﻿using FurnitureShop.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureShop.DAL
{
    public class FurnitureShopContext : DbContext
    {
        public FurnitureShopContext(DbContextOptions<FurnitureShopContext> options) : base(options)
        {
        }

        protected FurnitureShopContext()
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<AccountRole> AccountRoles { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Furniture> Furnitures { get; set;}
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderLine> OrderLines { get; set; }

        public virtual DbSet<Blog> Blogs { get; set; }



    }
}
