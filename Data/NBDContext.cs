using Microsoft.EntityFrameworkCore;
using NBD_TractionFive.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NBD_TractionFive.Data
{
    public class NBDContext : DbContext
    {
        public NBDContext(DbContextOptions<NBDContext> options)
            : base(options)
        {

        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Bid> Bids { get; set; }
        //public DbSet<BidState> BidStates { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Labour> Labours { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<BidLabour> BidLabours { get; set; }
        public DbSet<BidMaterial> BidMaterials { get; set; }
        public DbSet<InvType> InvTypes { get; set; }
        public DbSet<inventoryVM> inventoryVMs { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Project>()
            //    .HasOne(p => p.Client)
            //    .WithMany(b => b.Projects)
            //    .HasForeignKey(p => p.cliID)
            //    .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<BidState>()
            //    .HasIndex(p => p.Bids)
            //    .IsUnique();

            modelBuilder.Entity<Client>()
                .HasMany<Project>(p => p.Projects)
                .WithOne(p => p.Client)
                .HasForeignKey(p => p.cliID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Project>()
                .HasMany<Bid>(p => p.Bids)
                .WithOne(p => p.Project)
                .HasForeignKey(p => p.projID)
                .OnDelete(DeleteBehavior.Restrict);


            //After inverse prop changes
            modelBuilder.Entity<Employee>()
                .HasMany<Bid>(e => e.SalesBids)
                .WithOne(e => e.SalesAssociate)
                .HasForeignKey(e => e.SalesID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Employee>()
                .HasMany<Bid>(e => e.DesignerBids)
                .WithOne(e => e.Designer)
                .HasForeignKey(e => e.DesignerID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Bid>()
                .HasMany<BidLabour>(b => b.BidLabours)
                .WithOne(b => b.Bid)
                .HasForeignKey(b => b.bidID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Bid>()
                .HasMany<BidMaterial>(b => b.BidMaterials)
                .WithOne(b => b.Bid)
                .HasForeignKey(b => b.bidID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Labour>()
                .HasMany<BidLabour>(b => b.BidLabours)
                .WithOne(b => b.Labour)
                .HasForeignKey(b => b.labID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Inventory>()
                .HasMany<BidMaterial>(b => b.BidMaterials)
                .WithOne(b => b.Inventory)
                .HasForeignKey(b => b.invID)
                .OnDelete(DeleteBehavior.Restrict);

            //After BidState was made enum
            //modelBuilder.Entity<BidState>()
            //    .HasMany<Bid>(bs => bs.Bids)
            //    .WithOne(bs => bs.BidState)
            //    .HasForeignKey(bs => bs.bidStateID)
            //    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<InvType>()
                .HasMany<Inventory>(i => i.Inventories)
                .WithOne(i => i.InvType)
                .HasForeignKey(i => i.itID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BidLabour>()
                .HasKey(b => new { b.bidID, b.labID });

            modelBuilder.Entity<BidMaterial>()
                .HasKey(b => new { b.bidID, b.invID });
        }
    }
}
