namespace proje_obs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test_test1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DersTarihlers", "dersSecmeBaslangic", c => c.DateTime(nullable: false));
            AddColumn("dbo.DersTarihlers", "dersSecmeBitis", c => c.DateTime(nullable: false));
            DropColumn("dbo.DersTarihlers", "dersEklemeBaslangic");
            DropColumn("dbo.DersTarihlers", "dersEklemeBitis");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DersTarihlers", "dersEklemeBitis", c => c.DateTime(nullable: false));
            AddColumn("dbo.DersTarihlers", "dersEklemeBaslangic", c => c.DateTime(nullable: false));
            DropColumn("dbo.DersTarihlers", "dersSecmeBitis");
            DropColumn("dbo.DersTarihlers", "dersSecmeBaslangic");
        }
    }
}
