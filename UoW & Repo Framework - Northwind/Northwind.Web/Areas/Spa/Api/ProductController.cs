﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using Microsoft.Data.OData;
using Northwind.Entity.Models;
using Northwind.Web.Areas.Spa.Extensions;
using Repository;

namespace Northwind.Web.Areas.Spa.Api
{
    [ODataNullValue]
    public class ProductController : AsyncEntitySetController<Product, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }

        protected override int GetKey(Product entity)
        {
            return entity.ProductID;
        }

[Queryable]
public override async Task<IEnumerable<Product>> Get()
{
    return await _unitOfWork.Repository<Product>().Query().GetAsync();
}

        [Queryable]
        public override async Task<HttpResponseMessage> Get([FromODataUri] int key)
        {
            var query = _unitOfWork.Repository<Product>().Query().Filter(x => x.ProductID == key).Get();
            return Request.CreateResponse(HttpStatusCode.OK, query);
        }

        ///// <summary>
        ///// Retrieve an entity by key from the entity set.
        ///// </summary>
        ///// <param name="key">The entity key of the entity to retrieve.</param>
        ///// <returns>A Task that contains the retrieved entity when it completes, or null if an entity with the specified entity key cannot be found in the entity set.</returns>
        [Queryable]
        protected override async Task<Product> GetEntityByKeyAsync(int key)
        {
            return await _unitOfWork.Repository<Product>().FindAsync(key);
        }

        protected override async Task<Product> CreateEntityAsync(Product entity)
        {
            if (entity == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            
            _unitOfWork.Repository<Product>().Insert(entity);
            await _unitOfWork.SaveAsync();
            return entity;
        }

        protected override async Task<Product> UpdateEntityAsync(int key, Product update)
        {
            if (update == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            
            if (key != update.ProductID)
                throw new HttpResponseException(Request.CreateODataErrorResponse(HttpStatusCode.BadRequest, new ODataError { Message = "The supplied key and the Product being updated do not match." }));

            try
            {
                update.ObjectState = ObjectState.Modified;
                _unitOfWork.Repository<Product>().Update(update);
                var x = await _unitOfWork.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            return update;
        }

        // PATCH <controller>(key)
        /// <summary>
        /// Apply a partial update to an existing entity in the entity set.
        /// </summary>
        /// <param name="key">The entity key of the entity to update.</param>
        /// <param name="patch">The patch representing the partial update.</param>
        /// <returns>A Task that contains the updated entity when it completes.</returns>
        protected override async Task<Product> PatchEntityAsync(int key, Delta<Product> patch)
        {
            if (patch == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            if (key != patch.GetEntity().ProductID)
                throw Request.EntityNotFound();

            var entity = await _unitOfWork.Repository<Product>().FindAsync(key);

            if (entity == null)
                throw Request.EntityNotFound();

            try
            {
                patch.Patch(entity);
                await _unitOfWork.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new HttpResponseException(HttpStatusCode.Conflict);
            }
            return entity;
        }

        public override async Task Delete([FromODataUri] int key)
        {
            var entity = await _unitOfWork.Repository<Product>().FindAsync(key);

            if (entity == null)
                throw Request.EntityNotFound();

            _unitOfWork.Repository<Product>().Delete(entity);

            try
            {
                await _unitOfWork.SaveAsync();
            }
            catch (Exception e)
            { 
                throw new HttpResponseException(
                    new HttpResponseMessage(HttpStatusCode.Conflict)
                    {
                        StatusCode = HttpStatusCode.Conflict, 
                        Content = new StringContent(e.Message), 
                        ReasonPhrase = e.InnerException.InnerException.Message
                    });
            }
        }

        #region Links
        // Create a relation from Product to Category or Supplier, by creating a $link entity.
        // POST <controller>(key)/$links/Category
        // POST <controller>(key)/$links/Supplier
        /// <summary>
        /// Handle POST and PUT requests that attempt to create a link between two entities.
        /// </summary>
        /// <param name="key">The key of the entity with the navigation property.</param>
        /// <param name="navigationProperty">The name of the navigation property.</param>
        /// <param name="link">The URI of the entity to link.</param>
        /// <returns>A Task that completes when the link has been successfully created.</returns>
        [AcceptVerbs("POST", "PUT")]
        public override async Task CreateLink([FromODataUri] int key, string navigationProperty, [FromBody] Uri link)
        {
            var entity = await _unitOfWork.Repository<Product>().FindAsync(key);

            if (entity == null)
                throw Request.EntityNotFound();
            
            switch (navigationProperty)
            {
                case "Category":
                    var categoryKey = Request.GetKeyValue<int>(link);
                    var category = await _unitOfWork.Repository<Category>().FindAsync(categoryKey);

                    if (category == null)
                        throw Request.EntityNotFound();
                    
                        entity.Category = category;
                    break;

                case "Supplier":
                    var supplierKey = Request.GetKeyValue<int>(link);
                    var supplier = await _unitOfWork.Repository<Supplier>().FindAsync(supplierKey);

                    if (supplier == null)
                        throw Request.EntityNotFound();
                    
                        entity.Supplier = supplier;
                    break;

                default:
                    await base.CreateLink(key, navigationProperty, link);
                    break;
            }
            await _unitOfWork.SaveAsync();
        }

        // Remove a relation, by deleting a $link entity
        // DELETE <controller>(key)/$links/Category
        // DELETE <controller>(key)/$links/Supplier
        /// <summary>
        /// Handle DELETE requests that attempt to break a relationship between two entities.
        /// </summary>
        /// <param name="key">The key of the entity with the navigation property.</param>
        /// <param name="relatedKey">The key of the related entity.</param>
        /// <param name="navigationProperty">The name of the navigation property.</param>
        /// <returns>Task.</returns>
        public override async Task DeleteLink([FromODataUri] int key, string relatedKey, string navigationProperty)
        {
            var entity = await _unitOfWork.Repository<Product>().FindAsync(key);

            if (entity == null)
                throw Request.EntityNotFound();

            switch (navigationProperty)
            {
                case "Category":
                    entity.Category = null;
                    break;

                case "Supplier":
                    entity.Supplier = null;
                    break;

                default:
                    await base.DeleteLink(key, relatedKey, navigationProperty);
                    break;
            }

            await _unitOfWork.SaveAsync();
        }

        // Remove a relation, by deleting a $link entity
        // DELETE <controller>(key)/$links/Category
        // DELETE <controller>(key)/$links/Supplier
        /// <summary>
        /// Handle DELETE requests that attempt to break a relationship between two entities.
        /// </summary>
        /// <param name="key">The key of the entity with the navigation property.</param>
        /// <param name="navigationProperty">The name of the navigation property.</param>
        /// <param name="link">The URI of the entity to remove from the navigation property.</param>
        /// <returns>Task.</returns>
        public override async Task DeleteLink([FromODataUri] int key, string navigationProperty, [FromBody] Uri link)
        {
            var entity = await _unitOfWork.Repository<Product>().FindAsync(key);

            if (entity == null)
                throw Request.EntityNotFound();

            switch (navigationProperty)
            {
                case "Category":
                    entity.Category = null;
                    break;

                case "Supplier":
                    entity.Supplier = null;
                    break;
                     
                default:
                    await base.DeleteLink(key, navigationProperty, link);
                    break;
            }

            await _unitOfWork.SaveAsync();
        }
        #endregion Links

        public override async Task<HttpResponseMessage> HandleUnmappedRequest(ODataPath odataPath)
        {
            //TODO: add logic and proper return values
            return Request.CreateResponse(HttpStatusCode.NoContent, odataPath);
        }

        #region Navigation Properties
        public async Task<Category> GetCategory(int key)
        {
            var entity = await _unitOfWork.Repository<Product>().FindAsync(key);

            if (entity == null)
                throw Request.EntityNotFound();
            
            return entity.Category;
        }

        public async Task<Supplier> GetSupplier(int key)
        {
            var entity = await _unitOfWork.Repository<Product>().FindAsync(key);

            if (entity == null)
                throw Request.EntityNotFound();
            
            return entity.Supplier;
        }
        #endregion Navigation Properties
    }
}