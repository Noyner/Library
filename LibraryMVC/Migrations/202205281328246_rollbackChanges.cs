namespace LibraryMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rollbackChanges : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Books", "FilePath");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Books", "FilePath", c => c.String());
        }
    }
}
