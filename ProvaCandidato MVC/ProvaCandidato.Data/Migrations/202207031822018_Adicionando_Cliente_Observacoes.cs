namespace ProvaCandidato.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Adicionando_Cliente_Observacoes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClienteObservacao",
                c => new
                    {
                        codigo = c.Int(nullable: false, identity: true),
                        cliente_codigo = c.Int(nullable: false),
                        observacao = c.String(nullable: false, maxLength: 500),
                    })
                .PrimaryKey(t => t.codigo)
                .ForeignKey("dbo.Cliente", t => t.cliente_codigo, cascadeDelete: true)
                .Index(t => t.cliente_codigo);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ClienteObservacao", "cliente_codigo", "dbo.Cliente");
            DropIndex("dbo.ClienteObservacao", new[] { "cliente_codigo" });
            DropTable("dbo.ClienteObservacao");
        }
    }
}
