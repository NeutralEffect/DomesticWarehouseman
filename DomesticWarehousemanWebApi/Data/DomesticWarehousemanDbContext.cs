using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DomesticWarehousemanWebApi.Data
{
    public partial class DomesticWarehousemanDbContext : DbContext
    {
        public DomesticWarehousemanDbContext()
        {
        }

        public DomesticWarehousemanDbContext(DbContextOptions<DomesticWarehousemanDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<ArchivedItem> ArchivedItems { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<EssentialList> EssentialLists { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Provider> Providers { get; set; }
        public virtual DbSet<Resource> Resources { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<ShoppingList> ShoppingLists { get; set; }
        public virtual DbSet<ShoppingListComment> ShoppingListComments { get; set; }
        public virtual DbSet<ShoppingListEntry> ShoppingListEntries { get; set; }
        public virtual DbSet<Storage> Storages { get; set; }
        public virtual DbSet<StorageMember> StorageMembers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=DomesticWarehousemanDbDevelopment");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<ArchivedItem>(entity =>
            {
                entity.HasOne(d => d.IdResourceNavigation)
                    .WithMany(p => p.ArchivedItems)
                    .HasForeignKey(d => d.IdResource)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PK_ArchivedItem_Resource");

                entity.HasOne(d => d.IdStorageNavigation)
                    .WithMany(p => p.ArchivedItems)
                    .HasForeignKey(d => d.IdStorage)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PK_ArchivedItem_Storage");
            });

            modelBuilder.Entity<EssentialList>(entity =>
            {
                entity.HasOne(d => d.IdResourceNavigation)
                    .WithMany(p => p.EssentialLists)
                    .HasForeignKey(d => d.IdResource)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EssentialList_Resource");

                entity.HasOne(d => d.IdStorageNavigation)
                    .WithMany(p => p.EssentialLists)
                    .HasForeignKey(d => d.IdStorage)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EssentialList_Storage");
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.HasOne(d => d.IdResourceNavigation)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.IdResource)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Item_Resource");

                entity.HasOne(d => d.IdStorageNavigation)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.IdStorage)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Item_Storage");
            });

            modelBuilder.Entity<Resource>(entity =>
            {
                entity.HasOne(d => d.IdCategoryNavigation)
                    .WithMany(p => p.Resources)
                    .HasForeignKey(d => d.IdCategory)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Resource_Category");

                entity.HasOne(d => d.IdProviderNavigation)
                    .WithMany(p => p.Resources)
                    .HasForeignKey(d => d.IdProvider)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Resource_Provider");

                entity.HasOne(d => d.IdStorageNavigation)
                    .WithMany(p => p.Resources)
                    .HasForeignKey(d => d.IdStorage)
                    .HasConstraintName("FK_Resource_Storage");
            });

            modelBuilder.Entity<ShoppingList>(entity =>
            {
                entity.HasOne(d => d.IdAccountCreatorNavigation)
                    .WithMany(p => p.ShoppingListIdAccountCreatorNavigations)
                    .HasForeignKey(d => d.IdAccountCreator)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShoppingList_Account1");

                entity.HasOne(d => d.IdAccountOwnerNavigation)
                    .WithMany(p => p.ShoppingListIdAccountOwnerNavigations)
                    .HasForeignKey(d => d.IdAccountOwner)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShoppingList_Account2");

                entity.HasOne(d => d.IdCategoryNavigation)
                    .WithMany(p => p.ShoppingLists)
                    .HasForeignKey(d => d.IdCategory)
                    .HasConstraintName("FK_ShoppingList_Category");

                entity.HasOne(d => d.IdStorageNavigation)
                    .WithMany(p => p.ShoppingLists)
                    .HasForeignKey(d => d.IdStorage)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShoppingList_Storage");
            });

            modelBuilder.Entity<ShoppingListComment>(entity =>
            {
                entity.HasOne(d => d.IdAccountNavigation)
                    .WithMany(p => p.ShoppingListComments)
                    .HasForeignKey(d => d.IdAccount)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShoppingListComment_Account");

                entity.HasOne(d => d.IdShoppingListNavigation)
                    .WithMany(p => p.ShoppingListComments)
                    .HasForeignKey(d => d.IdShoppingList)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShoppingListComment_ShoppingList");
            });

            modelBuilder.Entity<ShoppingListEntry>(entity =>
            {
                entity.HasOne(d => d.IdResourceNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdResource)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShoppingListEntry_Resource");

                entity.HasOne(d => d.IdShoppingListNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdShoppingList)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShoppingListEntry_ShoppingList");
            });

            modelBuilder.Entity<StorageMember>(entity =>
            {
                entity.HasOne(d => d.IdAccountNavigation)
                    .WithMany(p => p.StorageMembers)
                    .HasForeignKey(d => d.IdAccount)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StorageMember_Account");

                entity.HasOne(d => d.IdRoleNavigation)
                    .WithMany(p => p.StorageMembers)
                    .HasForeignKey(d => d.IdRole)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StorageMember_Role");

                entity.HasOne(d => d.IdStorageNavigation)
                    .WithMany(p => p.StorageMembers)
                    .HasForeignKey(d => d.IdStorage)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StorageMember_Storage");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
