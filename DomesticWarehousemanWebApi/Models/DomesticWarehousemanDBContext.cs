using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DomesticWarehousemanWebApi.Models
{
    public partial class DomesticWarehousemanDBContext : DbContext
    {
        public DomesticWarehousemanDBContext()
        {
        }

        public DomesticWarehousemanDBContext(DbContextOptions<DomesticWarehousemanDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ArchivedItem> ArchivedItems { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<EssentialList> EssentialLists { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Provider> Providers { get; set; }
        public virtual DbSet<Resource> Resources { get; set; }
        public virtual DbSet<ShoppingList> ShoppingLists { get; set; }
        public virtual DbSet<ShoppingListEntry> ShoppingListEntries { get; set; }
        public virtual DbSet<Storage> Storages { get; set; }
        public virtual DbSet<StorageMember> StorageMembers { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=DomesticWarehousemanDBDevelopment");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<ArchivedItem>(entity =>
            {
                entity.ToTable("ArchivedItem");

                entity.HasIndex(e => e.IdResource, "PK_ArchivedItem_Resource");

                entity.HasIndex(e => e.IdStorage, "PK_ArchivedItem_Storage");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.ArchivedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("archivedOn");

                entity.Property(e => e.ArchivizationReason)
                    .HasMaxLength(100)
                    .HasColumnName("archivizationReason");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("createdOn");

                entity.Property(e => e.ExpiresOn)
                    .HasColumnType("datetime")
                    .HasColumnName("expiresOn");

                entity.Property(e => e.IdResource).HasColumnName("idResource");

                entity.Property(e => e.IdStorage).HasColumnName("idStorage");

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

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("createdOn");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<EssentialList>(entity =>
            {
                entity.ToTable("EssentialList");

                entity.HasIndex(e => e.IdResource, "FK_EssentialList_Resource");

                entity.HasIndex(e => e.IdStorage, "FK_EssentialList_Storage");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("createdOn");

                entity.Property(e => e.IdResource).HasColumnName("idResource");

                entity.Property(e => e.IdStorage).HasColumnName("idStorage");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("updatedOn");

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
                entity.ToTable("Item");

                entity.HasIndex(e => e.IdResource, "FK_Item_Resource");

                entity.HasIndex(e => e.IdStorage, "FK_Item_Storage");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("createdOn");

                entity.Property(e => e.ExpiresOn)
                    .HasColumnType("datetime")
                    .HasColumnName("expiresOn");

                entity.Property(e => e.IdResource).HasColumnName("idResource");

                entity.Property(e => e.IdStorage).HasColumnName("idStorage");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("updatedOn");

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

            modelBuilder.Entity<Provider>(entity =>
            {
                entity.ToTable("Provider");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("createdOn");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Resource>(entity =>
            {
                entity.ToTable("Resource");

                entity.HasIndex(e => e.IdCategory, "FK_Resource_Category");

                entity.HasIndex(e => e.IdProvider, "FK_Resource_Provider");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("createdOn");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .HasColumnName("description");

                entity.Property(e => e.IdCategory).HasColumnName("idCategory");

                entity.Property(e => e.IdProvider).HasColumnName("idProvider");

                entity.Property(e => e.Image)
                    .IsRequired()
                    .HasColumnName("image");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("name");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("updatedOn");

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
            });

            modelBuilder.Entity<ShoppingList>(entity =>
            {
                entity.ToTable("ShoppingList");

                entity.HasIndex(e => e.IdCategory, "FK_ShoppingList_Category");

                entity.HasIndex(e => e.IdStorage, "FK_ShoppingList_Storage");

                entity.HasIndex(e => e.IdUserCreator, "FK_ShoppingList_User1");

                entity.HasIndex(e => e.IdUserOwner, "FK_ShoppingList_User2");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("createdOn");

                entity.Property(e => e.IdCategory).HasColumnName("idCategory");

                entity.Property(e => e.IdStorage).HasColumnName("idStorage");

                entity.Property(e => e.IdUserCreator).HasColumnName("idUserCreator");

                entity.Property(e => e.IdUserOwner).HasColumnName("idUserOwner");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("updatedOn");

                entity.HasOne(d => d.IdCategoryNavigation)
                    .WithMany(p => p.ShoppingLists)
                    .HasForeignKey(d => d.IdCategory)
                    .HasConstraintName("FK_ShoppingList_Category");

                entity.HasOne(d => d.IdStorageNavigation)
                    .WithMany(p => p.ShoppingLists)
                    .HasForeignKey(d => d.IdStorage)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShoppingList_Storage");

                entity.HasOne(d => d.IdUserCreatorNavigation)
                    .WithMany(p => p.ShoppingListIdUserCreatorNavigations)
                    .HasForeignKey(d => d.IdUserCreator)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShoppingList_User1");

                entity.HasOne(d => d.IdUserOwnerNavigation)
                    .WithMany(p => p.ShoppingListIdUserOwnerNavigations)
                    .HasForeignKey(d => d.IdUserOwner)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShoppingList_User2");
            });

            modelBuilder.Entity<ShoppingListEntry>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ShoppingListEntry");

                entity.HasIndex(e => e.IdResource, "FK_ShoppingListEntry_Resource");

                entity.HasIndex(e => e.IdShoppingList, "FK_ShoppingListEntry_ShoppingList");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.Checked).HasColumnName("checked");

                entity.Property(e => e.IdResource).HasColumnName("idResource");

                entity.Property(e => e.IdShoppingList).HasColumnName("idShoppingList");

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

            modelBuilder.Entity<Storage>(entity =>
            {
                entity.ToTable("Storage");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("createdOn");

                entity.Property(e => e.DisplayName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("displayName");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("updatedOn");
            });

            modelBuilder.Entity<StorageMember>(entity =>
            {
                entity.ToTable("StorageMember");

                entity.HasIndex(e => e.IdStorage, "FK_StorageMember_Storage");

                entity.HasIndex(e => e.IdUser, "FK_StorageMember_User");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("createdOn");

                entity.Property(e => e.IdStorage).HasColumnName("idStorage");

                entity.Property(e => e.IdUser).HasColumnName("idUser");

                entity.Property(e => e.Role).HasColumnName("role");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("updatedOn");

                entity.HasOne(d => d.IdStorageNavigation)
                    .WithMany(p => p.StorageMembers)
                    .HasForeignKey(d => d.IdStorage)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StorageMember_Storage");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.StorageMembers)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StorageMember_User");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("createdOn");

                entity.Property(e => e.DisplayName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("displayName");

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("login");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("password")
                    .IsFixedLength(true);

                entity.Property(e => e.UpdatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("updatedOn");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
