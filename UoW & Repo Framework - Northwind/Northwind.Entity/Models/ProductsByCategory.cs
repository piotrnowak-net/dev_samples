#region

using System;
using Repository;

#endregion

namespace Northwind.Entity.Models
{
    public partial class ProductsByCategory : EntityBase
    {
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
        public string QuantityPerUnit { get; set; }
        public Nullable<short> UnitsInStock { get; set; }
        public bool Discontinued { get; set; }
    }
}