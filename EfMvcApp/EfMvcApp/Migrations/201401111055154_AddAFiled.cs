namespace EfMvcApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAFiled : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Student", "a", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Student", "a");
        }
    }
}
