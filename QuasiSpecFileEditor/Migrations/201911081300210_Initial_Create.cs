namespace QuasiSpecFileEditor.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial_Create : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admin",
                c => new
                    {
                        AdminID = c.Int(nullable: false, identity: true),
                        SAMAccountName = c.String(),
                    })
                .PrimaryKey(t => t.AdminID);
            
            CreateTable(
                "dbo.AlarmDescription",
                c => new
                    {
                        AlarmDescriptionID = c.Int(nullable: false, identity: true),
                        ModelID = c.Int(nullable: false),
                        AlarmID = c.Int(nullable: false),
                        Priority = c.Int(),
                        Criteria = c.Int(),
                        Hold = c.String(),
                        ParameterName = c.String(),
                        TargetLimit = c.String(),
                        LastModifyDate = c.DateTime(),
                        LastModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.AlarmDescriptionID)
                .ForeignKey("dbo.Alarm", t => t.AlarmID, cascadeDelete: true)
                .ForeignKey("dbo.Model", t => t.ModelID, cascadeDelete: true)
                .Index(t => t.ModelID)
                .Index(t => t.AlarmID);
            
            CreateTable(
                "dbo.Alarm",
                c => new
                    {
                        AlarmID = c.Int(nullable: false, identity: true),
                        AlarmName = c.String(),
                        Comment = c.String(),
                    })
                .PrimaryKey(t => t.AlarmID);
            
            CreateTable(
                "dbo.Model",
                c => new
                    {
                        ModelID = c.Int(nullable: false, identity: true),
                        CodeName = c.String(),
                    })
                .PrimaryKey(t => t.ModelID);
            
            CreateTable(
                "dbo.Group",
                c => new
                    {
                        GroupID = c.Int(nullable: false, identity: true),
                        GroupName = c.String(),
                        ModelID = c.Int(nullable: false),
                        LastModifyDate = c.DateTime(),
                        LastModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.GroupID)
                .ForeignKey("dbo.Model", t => t.ModelID, cascadeDelete: true)
                .Index(t => t.ModelID);
            
            CreateTable(
                "dbo.Parameter",
                c => new
                    {
                        ParameterID = c.Int(nullable: false, identity: true),
                        FieldName = c.String(),
                        UpperLimit = c.Double(),
                        UpperLimitDefect = c.String(),
                        LowerLimit = c.Double(),
                        LowerLimitDefect = c.String(),
                        NegFactor = c.String(),
                        NegFactorDefect = c.String(),
                        OpReqUpload = c.String(),
                        ParamTest = c.String(),
                        CSVHeaderName = c.String(),
                        LastModifyDate = c.DateTime(),
                        LastModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.ParameterID);
            
            CreateTable(
                "dbo.Part",
                c => new
                    {
                        PartID = c.Int(nullable: false, identity: true),
                        PN = c.String(),
                        ModelID = c.Int(nullable: false),
                        GroupID = c.Int(),
                    })
                .PrimaryKey(t => t.PartID)
                .ForeignKey("dbo.Group", t => t.GroupID)
                .ForeignKey("dbo.Model", t => t.ModelID, cascadeDelete: true)
                .Index(t => t.ModelID)
                .Index(t => t.GroupID);
            
            CreateTable(
                "dbo.ParameterGroup",
                c => new
                    {
                        Parameter_ParameterID = c.Int(nullable: false),
                        Group_GroupID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Parameter_ParameterID, t.Group_GroupID })
                .ForeignKey("dbo.Parameter", t => t.Parameter_ParameterID, cascadeDelete: true)
                .ForeignKey("dbo.Group", t => t.Group_GroupID, cascadeDelete: true)
                .Index(t => t.Parameter_ParameterID)
                .Index(t => t.Group_GroupID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Part", "ModelID", "dbo.Model");
            DropForeignKey("dbo.Part", "GroupID", "dbo.Group");
            DropForeignKey("dbo.ParameterGroup", "Group_GroupID", "dbo.Group");
            DropForeignKey("dbo.ParameterGroup", "Parameter_ParameterID", "dbo.Parameter");
            DropForeignKey("dbo.Group", "ModelID", "dbo.Model");
            DropForeignKey("dbo.AlarmDescription", "ModelID", "dbo.Model");
            DropForeignKey("dbo.AlarmDescription", "AlarmID", "dbo.Alarm");
            DropIndex("dbo.ParameterGroup", new[] { "Group_GroupID" });
            DropIndex("dbo.ParameterGroup", new[] { "Parameter_ParameterID" });
            DropIndex("dbo.Part", new[] { "GroupID" });
            DropIndex("dbo.Part", new[] { "ModelID" });
            DropIndex("dbo.Group", new[] { "ModelID" });
            DropIndex("dbo.AlarmDescription", new[] { "AlarmID" });
            DropIndex("dbo.AlarmDescription", new[] { "ModelID" });
            DropTable("dbo.ParameterGroup");
            DropTable("dbo.Part");
            DropTable("dbo.Parameter");
            DropTable("dbo.Group");
            DropTable("dbo.Model");
            DropTable("dbo.Alarm");
            DropTable("dbo.AlarmDescription");
            DropTable("dbo.Admin");
        }
    }
}
