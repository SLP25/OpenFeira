﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp1.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Bid> Bids { get; set; }

    public virtual DbSet<Buyer> Buyers { get; set; }

    public virtual DbSet<Market> Markets { get; set; }

    public virtual DbSet<Organizer> Organizers { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductDelivery> ProductDeliveries { get; set; }

    public virtual DbSet<ProductPhoto> ProductPhotos { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    public virtual DbSet<Seller> Sellers { get; set; }

    public virtual DbSet<Stand> Stands { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserToken> UserTokens { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bid>(entity =>
        {
            entity.ToTable("Bid");

            entity.Property(e => e.BidId)
                .ValueGeneratedNever()
                .HasColumnName("bid_id");
            entity.Property(e => e.BidAmount).HasColumnName("bid_amount");
            entity.Property(e => e.BidProduct).HasColumnName("bid_product");
            entity.Property(e => e.BidStand).HasColumnName("bid_stand");
            entity.Property(e => e.BidTimestamp)
                .HasColumnType("datetime")
                .HasColumnName("bid_timestamp");
            entity.Property(e => e.BuyerId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("buyer_id");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(2, 0)")
                .HasColumnName("price");
            entity.Property(e => e.SaleId).HasColumnName("sale_id");

            entity.HasOne(d => d.BidProductNavigation).WithMany(p => p.Bids)
                .HasForeignKey(d => d.BidProduct)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Bid_Product");

            entity.HasOne(d => d.BidStandNavigation).WithMany(p => p.Bids)
                .HasForeignKey(d => d.BidStand)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Bid_Stand");

            entity.HasOne(d => d.Buyer).WithMany(p => p.Bids)
                .HasForeignKey(d => d.BuyerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Bid_Buyer");

            entity.HasOne(d => d.Sale).WithMany(p => p.Bids)
                .HasForeignKey(d => d.SaleId)
                .HasConstraintName("FK_Bid_Sale");
        });

        modelBuilder.Entity<Buyer>(entity =>
        {
            entity.HasKey(e => e.UserEmail);

            entity.ToTable("Buyer");

            entity.Property(e => e.UserEmail)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("user_email");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");

            entity.HasOne(d => d.UserEmailNavigation).WithOne(p => p.Buyer)
                .HasForeignKey<Buyer>(d => d.UserEmail)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Buyer_User");
        });

        modelBuilder.Entity<Market>(entity =>
        {
            entity.ToTable("Market");

            entity.Property(e => e.MarketId)
                .ValueGeneratedNever()
                .HasColumnName("market_id");
            entity.Property(e => e.EndingTime)
                .HasColumnType("datetime")
                .HasColumnName("ending_time");
            entity.Property(e => e.MarketDescription)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("market_description");
            entity.Property(e => e.MarketLocation)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("market_location");
            entity.Property(e => e.MarketName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("market_name");
            entity.Property(e => e.MarketPhotoPath)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("market_photo_path");
            entity.Property(e => e.OrganizerId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("organizer_id");
            entity.Property(e => e.StartingTime)
                .HasColumnType("datetime")
                .HasColumnName("starting_time");
            entity.Property(e => e.TotalStands).HasColumnName("total_stands");

            entity.HasOne(d => d.Organizer).WithMany(p => p.Markets)
                .HasForeignKey(d => d.OrganizerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Market_Organizer");
        });

        modelBuilder.Entity<Organizer>(entity =>
        {
            entity.HasKey(e => e.UserEmail);

            entity.ToTable("Organizer");

            entity.Property(e => e.UserEmail)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("user_email");
            entity.Property(e => e.OrganizerName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("organizer_name");

            entity.HasOne(d => d.UserEmailNavigation).WithOne(p => p.Organizer)
                .HasForeignKey<Organizer>(d => d.UserEmail)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Organizer_User");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Product");

            entity.Property(e => e.ProductId)
                .ValueGeneratedNever()
                .HasColumnName("product_id");
            entity.Property(e => e.ProductBasePrice)
                .HasColumnType("decimal(2, 0)")
                .HasColumnName("product_base_price");
            entity.Property(e => e.ProductDescription)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("product_description");
            entity.Property(e => e.ProductName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("product_name");
            entity.Property(e => e.ProductStock).HasColumnName("product_stock");
        });

        modelBuilder.Entity<ProductDelivery>(entity =>
        {
            entity.HasKey(e => e.DeliveryId);

            entity.ToTable("ProductDelivery");

            entity.Property(e => e.DeliveryId)
                .ValueGeneratedNever()
                .HasColumnName("delivery_id");
            entity.Property(e => e.DeliveryAmount).HasColumnName("delivery_amount");
            entity.Property(e => e.DeliveryTimestamp)
                .HasColumnType("datetime")
                .HasColumnName("delivery_timestamp");
            entity.Property(e => e.ProductId).HasColumnName("product_id");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductDeliveries)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductDelivery_Product");
        });

        modelBuilder.Entity<ProductPhoto>(entity =>
        {
            entity.HasKey(e => new { e.ProductId, e.ProductPhotoPath });

            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.ProductPhotoPath)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("product_photo_path");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductPhotos)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductPhotos_Product");
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.ToTable("Sale");

            entity.Property(e => e.SaleId)
                .ValueGeneratedNever()
                .HasColumnName("sale_id");
            entity.Property(e => e.BidId).HasColumnName("bid_id");
            entity.Property(e => e.SaleTimestamp)
                .HasColumnType("datetime")
                .HasColumnName("sale_timestamp");
            entity.Property(e => e.SellerId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("seller_id");

            entity.HasOne(d => d.Bid).WithMany(p => p.Sales)
                .HasForeignKey(d => d.BidId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sale_Bid");

            entity.HasOne(d => d.Seller).WithMany(p => p.Sales)
                .HasForeignKey(d => d.SellerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sale_Seller");
        });

        modelBuilder.Entity<Seller>(entity =>
        {
            entity.HasKey(e => e.UserEmail);

            entity.ToTable("Seller");

            entity.Property(e => e.UserEmail)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("user_email");
            entity.Property(e => e.CompanyName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("company_name");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(13)
                .IsUnicode(false)
                .HasColumnName("phone_number");
            entity.Property(e => e.Website)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("website");

            entity.HasOne(d => d.UserEmailNavigation).WithOne(p => p.Seller)
                .HasForeignKey<Seller>(d => d.UserEmail)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Seller_User");
        });

        modelBuilder.Entity<Stand>(entity =>
        {
            entity.ToTable("Stand");

            entity.Property(e => e.StandId)
                .ValueGeneratedNever()
                .HasColumnName("stand_id");
            entity.Property(e => e.MarketId).HasColumnName("market_id");
            entity.Property(e => e.SellerId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("seller_id");
            entity.Property(e => e.StandPhotoPath)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("stand_photo_path");

            entity.HasOne(d => d.Market).WithMany(p => p.Stands)
                .HasForeignKey(d => d.MarketId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Stand_Market");

            entity.HasOne(d => d.Seller).WithMany(p => p.Stands)
                .HasForeignKey(d => d.SellerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Stand_Seller");

            entity.HasMany(d => d.Products).WithMany(p => p.Stands)
                .UsingEntity<Dictionary<string, object>>(
                    "ProductInStand",
                    r => r.HasOne<Product>().WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_ProductInStand_Product"),
                    l => l.HasOne<Stand>().WithMany()
                        .HasForeignKey("StandId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_ProductInStand_Stand"),
                    j =>
                    {
                        j.HasKey("StandId", "ProductId");
                        j.ToTable("ProductInStand");
                    });
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserEmail);

            entity.ToTable("User");

            entity.Property(e => e.UserEmail)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("user_email");
            entity.Property(e => e.Nif).HasColumnName("nif");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("password_hash");
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("role");
        });

        modelBuilder.Entity<UserToken>(entity =>
        {
            entity.HasKey(e => e.UserEmail);

            entity.ToTable("UserToken");

            entity.Property(e => e.UserEmail)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("user_email");
            entity.Property(e => e.ExpiryDate)
                .HasColumnType("datetime")
                .HasColumnName("expiry_date");
            entity.Property(e => e.Token).HasColumnName("token");

            entity.HasOne(d => d.UserEmailNavigation).WithOne(p => p.UserToken)
                .HasForeignKey<UserToken>(d => d.UserEmail)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserToken_User");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
