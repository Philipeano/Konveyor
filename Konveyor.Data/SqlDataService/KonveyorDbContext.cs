using Konveyor.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Konveyor.Data.SqlDataService
{
    public partial class KonveyorDbContext : DbContext
    {
        public KonveyorDbContext(DbContextOptions<KonveyorDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CostParameters> CostParameters { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<NigerianStates> NigerianStates { get; set; }
        public virtual DbSet<Offices> Offices { get; set; }
        public virtual DbSet<OrderStatus> OrderStatus { get; set; }
        public virtual DbSet<OrderUpdates> OrderUpdates { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<PackageStatus> PackageStatus { get; set; }
        public virtual DbSet<PackageTypes> PackageTypes { get; set; }
        public virtual DbSet<PackageUpdates> PackageUpdates { get; set; }
        public virtual DbSet<Packages> Packages { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost;Initial Catalog = KonveyorDB;Integrated Security = true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CostParameters>(entity =>
            {
                entity.HasKey(e => e.ParameterId);

                entity.Property(e => e.Description).HasMaxLength(300);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Multiplier).HasDefaultValueSql("((1))");

                entity.Property(e => e.Operand)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasDefaultValueSql("('Add')");

                entity.Property(e => e.ParameterName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Scope)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasDefaultValueSql("('Order')");

                entity.Property(e => e.Value)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<Customers>(entity =>
            {
                entity.HasKey(e => e.CustomerId);

                entity.HasIndex(e => e.CustomerCode)
                    .HasName("IX_Customers")
                    .IsUnique();

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_Customers_1")
                    .IsUnique();

                entity.Property(e => e.ContactAddress).HasMaxLength(200);

                entity.Property(e => e.CustomerCode)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.LastUpdated).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PreferredName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Status).HasMaxLength(20);

                entity.HasOne(d => d.User)
                    .WithOne(p => p.Customers)
                    .HasForeignKey<Customers>(d => d.UserId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Customers_Users");
            });

            modelBuilder.Entity<Employees>(entity =>
            {
                entity.HasKey(e => e.EmployeeId);

                entity.HasIndex(e => e.EmployeeCode)
                    .HasName("IX_Employees")
                    .IsUnique();

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_Employees_1")
                    .IsUnique();

                entity.Property(e => e.Designation)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.EmployeeCode)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.LastUpdated).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.RoleId).HasDefaultValueSql("((4))");

                entity.Property(e => e.Status).HasMaxLength(20);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employees_Roles");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.Employees)
                    .HasForeignKey<Employees>(d => d.UserId)
                    .HasConstraintName("FK_Employees_Users");
            });

            modelBuilder.Entity<NigerianStates>(entity =>
            {
                entity.HasKey(e => e.StateId);

                entity.HasIndex(e => e.Abbreviation)
                    .HasName("IX_NigerianStates_1")
                    .IsUnique();

                entity.HasIndex(e => e.State)
                    .HasName("IX_NigerianStates")
                    .IsUnique();

                entity.Property(e => e.Abbreviation)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsFixedLength();

                entity.Property(e => e.Capital)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Offices>(entity =>
            {
                entity.HasKey(e => e.OfficeId);

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.EmailAddress).HasMaxLength(150);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.OfficeName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.PhoneNumber).HasMaxLength(20);

                entity.HasOne(d => d.State)
                    .WithMany(p => p.Offices)
                    .HasForeignKey(d => d.StateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Offices_NigerianStates");
            });

            modelBuilder.Entity<OrderStatus>(entity =>
            {
                entity.HasIndex(e => e.OrderStatus1)
                    .HasName("IX_OrderStatus")
                    .IsUnique();

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.OrderStatus1)
                    .IsRequired()
                    .HasColumnName("OrderStatus")
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<OrderUpdates>(entity =>
            {
                entity.HasKey(e => e.EntryId);

                entity.Property(e => e.EntryDate).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.NewOrderStatus)
                    .WithMany(p => p.OrderUpdates)
                    .HasForeignKey(d => d.NewOrderStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderUpdates_OrderStatus");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderUpdates)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderUpdates_Orders");

                entity.HasOne(d => d.ProcessedByNavigation)
                    .WithMany(p => p.OrderUpdates)
                    .HasForeignKey(d => d.ProcessedBy)
                    .HasConstraintName("FK_OrderUpdates_Employees");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PK_Transactions");

                entity.HasIndex(e => e.TrackingCode)
                    .HasName("IX_Orders")
                    .IsUnique();

                entity.Property(e => e.ExpectedNumOfDays).HasDefaultValueSql("((3))");

                entity.Property(e => e.RecipientAddress).HasMaxLength(300);

                entity.Property(e => e.RecipientName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.RecipientPhone)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Remarks)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.TotalCost).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TrackingCode).HasMaxLength(50);

                entity.HasOne(d => d.DestinationOffice)
                    .WithMany(p => p.OrdersDestinationOffice)
                    .HasForeignKey(d => d.DestinationOfficeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_Offices1");

                entity.HasOne(d => d.OriginOffice)
                    .WithMany(p => p.OrdersOriginOffice)
                    .HasForeignKey(d => d.OriginOfficeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_Offices");

                entity.HasOne(d => d.Sender)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.SenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_Customers");
            });

            modelBuilder.Entity<PackageStatus>(entity =>
            {
                entity.HasIndex(e => e.PackageStatus1)
                    .HasName("IX_PackageStatus")
                    .IsUnique();

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.PackageStatus1)
                    .IsRequired()
                    .HasColumnName("PackageStatus")
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<PackageTypes>(entity =>
            {
                entity.HasKey(e => e.TypeId);

                entity.HasIndex(e => e.TypeName)
                    .HasName("IX_PackageTypes")
                    .IsUnique();

                entity.Property(e => e.Description).HasMaxLength(300);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.TypeName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<PackageUpdates>(entity =>
            {
                entity.HasKey(e => e.EntryId);

                entity.Property(e => e.EntryDate).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.LoggedByNavigation)
                    .WithMany(p => p.PackageUpdates)
                    .HasForeignKey(d => d.LoggedBy)
                    .HasConstraintName("FK_PackageUpdates_Employees");

                entity.HasOne(d => d.NewPackageStatus)
                    .WithMany(p => p.PackageUpdates)
                    .HasForeignKey(d => d.NewPackageStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PackageUpdates_PackageStatus");

                entity.HasOne(d => d.Package)
                    .WithMany(p => p.PackageUpdates)
                    .HasForeignKey(d => d.PackageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PackageUpdates_Packages");
            });

            modelBuilder.Entity<Packages>(entity =>
            {
                entity.HasKey(e => e.PackageId);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Packages)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_Packages_Orders");

                entity.HasOne(d => d.PackageType)
                    .WithMany(p => p.Packages)
                    .HasForeignKey(d => d.PackageTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Packages_PackageTypes");
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.HasKey(e => e.RoleId);

                entity.HasIndex(e => e.RoleName)
                    .HasName("IX_Roles")
                    .IsUnique();

                entity.Property(e => e.Description).HasMaxLength(300);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.HasIndex(e => e.EmailAddress)
                    .HasName("IX_Users")
                    .IsUnique();

                entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EmailAddress)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Gender).HasMaxLength(20);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
