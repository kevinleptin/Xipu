namespace Xipu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeUniqueIX : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Chefs", new[] { "PhoneNumber" });
            CreateIndex("dbo.Chefs", new[] { "PhoneNumber", "Entrypoint" }, unique: true, name: "IX_PhoneEntrypoint");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Chefs", "IX_PhoneEntrypoint");
            CreateIndex("dbo.Chefs", "PhoneNumber", unique: true);
        }
    }
}
