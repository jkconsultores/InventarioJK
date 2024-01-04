using Microsoft.EntityFrameworkCore;
using Model_Inventario.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio_Inventario
{
    public class SqlDbContext : DbContext
    {
        private readonly string _connectionString;

        public SqlDbContext() { _connectionString = null; }
        public SqlDbContext(string connectionString)
        {
            this._connectionString = connectionString;
        }
        public SqlDbContext(DbContextOptions<SqlDbContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_connectionString != null)
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }
        }
        public DbSet<Empresa> EMPRESA { get; set; }
        public DbSet<DocumentosAValidar> T_DocumentosAValidar { get; set; }
        public DbSet<SunatData> SunatData { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DocumentosAValidar>().Property(d => d.MontoTotal)
            .HasColumnType("decimal(18, 2)");
            modelBuilder.Entity<DocumentosAValidar>()
                .HasKey(e => new { e.tipoDocumentoRemision, e.numeroDocumentoRemision, e.serieNumero, e.tipoDocumento });
            modelBuilder.Entity<SunatData>()
                .HasKey(e => new { e.RucEmsior });
        }

    }
}
