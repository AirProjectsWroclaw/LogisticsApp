namespace LogisticsProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cities", "Disabled", c => c.Boolean(nullable: false));
            AddColumn("dbo.Cities", "Group_Disabled", c => c.Boolean(nullable: false));
            AddColumn("dbo.Cities", "Group_Name", c => c.String());
            AddColumn("dbo.Cities", "Selected", c => c.Boolean(nullable: false));
            AddColumn("dbo.Cities", "Text", c => c.String());
            AddColumn("dbo.Cities", "Value", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cities", "Value");
            DropColumn("dbo.Cities", "Text");
            DropColumn("dbo.Cities", "Selected");
            DropColumn("dbo.Cities", "Group_Name");
            DropColumn("dbo.Cities", "Group_Disabled");
            DropColumn("dbo.Cities", "Disabled");
        }
    }
}
