namespace LibraryMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Books", "SerialNumber", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Books", "SerialNumber", c => c.String(nullable: false));
        }
    }
}
