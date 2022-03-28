﻿#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Catalog.Models;

namespace Catalog.Data
{
    public class CatalogContext : DbContext
    {
        public CatalogContext (DbContextOptions<CatalogContext> options)
            : base(options)
        {
        }

        public DbSet<Catalog.Models.Product> Product { get; set; }

        public DbSet<Catalog.Models.Brand> Brand { get; set; }

        public DbSet<Catalog.Models.ProductType> ProductType { get; set; }
    }
}
