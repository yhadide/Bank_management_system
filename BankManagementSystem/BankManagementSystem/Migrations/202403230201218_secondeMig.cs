namespace BankManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class secondeMig : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "Password", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Employees", "Password", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Customers", "Email", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "Email", c => c.String(maxLength: 100));
            DropColumn("dbo.Employees", "Password");
            DropColumn("dbo.Customers", "Password");
        }
    }
}
