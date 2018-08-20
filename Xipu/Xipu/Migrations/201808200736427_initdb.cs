namespace Xipu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initdb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Chefs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreateOn = c.DateTime(nullable: false),
                        ClientIp = c.String(maxLength: 64),
                        UserName = c.String(maxLength: 64),
                        PhoneNumber = c.String(maxLength: 20),
                        Province = c.String(maxLength: 20),
                        Cuisine = c.String(maxLength: 20),
                        Entrypoint = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.PhoneNumber, unique: true);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Chefs", new[] { "PhoneNumber" });
            DropTable("dbo.Chefs");
        }
    }
}
