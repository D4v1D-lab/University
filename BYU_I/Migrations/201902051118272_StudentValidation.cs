namespace BYU_I.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StudentValidation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Student", "FirstName", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Student", "FirstName", c => c.String(maxLength: 50));
        }
    }
}
