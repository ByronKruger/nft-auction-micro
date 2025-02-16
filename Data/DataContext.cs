using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NftApi.Entities;

namespace NftApi.Data;

public class DataContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>,
    RoleUser, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
{

    public DbSet<Nft> Nfts { get; set; }
    public DbSet<Auction> Auctions { get; set; }
    public DbSet<Bid> Bids { get; set; }
    public DbSet<RoleUser> UserAndRoles { get; set; }

    public DataContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Role>()
            .HasMany(e => e.UserRoles)
            .WithOne(e => e.Role)
            .HasForeignKey(e => e.RoleId)
            .IsRequired();

        builder.Entity<User>()
            .HasMany(e => e.UserRoles)
            .WithOne(e => e.User)
            .HasForeignKey(e => e.UserId)
            .IsRequired();

        builder.Entity<User>()
            .HasMany(u => u.Nfts)
            .WithOne(u => u.User)
            .HasForeignKey(n => n.UserId);
            // .IsRequired();

        // builder.Entity<Auction>()
        //     .HasOne(u => u.Charity)
        //     .WithOne(u => u.Auction)
        //     .HasForeignKey<User>(a => a.AuctionId);
            // .HasForeignKey(a => a.AuctionId)
            // .IsRequired();

        builder.Entity<Auction>()
            .HasMany(u => u.Users)
            .WithOne(u => u.Auction)
            .HasForeignKey(n => n.AuctionId);
            // .IsRequired();
            // .OnDelete(DeleteBehavior.Cascade);


        // builder.Entity<Nft>()
        //     .HasMany(u => u.Nfts)
        //     .WithOne(u => u.User)
        //     .HasForeignKey(n => n.UserId)
        //     .IsRequired();

// ==================================================
        builder.Entity<Auction>()
            .HasMany(e => e.Bids)
            .WithOne(e => e.Auction)
            .HasForeignKey(e => e.AuctionId)
            .IsRequired();
            // .OnDelete(DeleteBehavior.Cascade);
        
        // builder.Entity<Bid>()
        //     // .WithOne(e => e.Bid)
        //     .HasOne(e => e.Auction)
        //     .WithMany(e => e.Bids)
        //     .HasForeignKey(e => e.AuctionId)
        //     .IsRequired();
// ==================================================
// ==================================================
        // builder.Entity<Bid>()
        //     .HasOne(e => e.User)
        //     .WithMany(e => e.Bids)
        //     .HasForeignKey(e => e.UserId)
        //     .IsRequired();

        builder.Entity<User>()
            .HasMany(e => e.Bids)
            .WithOne(e => e.User)
            .HasForeignKey(e => e.UserId)
            .IsRequired();
// ==================================================

        // builder.Entity<Auction>()
        //     .HasMany(e => e.Bids)
        //     .WithOne(e => e.Auction)
        //     .HasForeignKey(e => e.AuctionId)
        //     .IsRequired();


    }
}