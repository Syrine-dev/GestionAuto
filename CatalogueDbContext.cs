using Microsoft.EntityFrameworkCore;
using GestionAutoEcole.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionAutoEcole.Service
{
    public class CatalogueDbContext: DbContext
    {
        public DbSet<Client> clients { set; get; }
        public object Clients { get; internal set; }
        public DbSet<Moniteur> Moniteurs { set; get; }

        public DbSet<Paiement> Paiements { set; get; }

        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=Cat_DB_8;Trusted_Connection=True");
        }
    }
}
