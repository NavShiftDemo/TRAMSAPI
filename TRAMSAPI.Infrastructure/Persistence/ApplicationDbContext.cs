using Microsoft.EntityFrameworkCore;
using TRAMSAPI.Domain.Entities;
using TRAMSAPI.Infrastructure.Persistence.Seeds;

namespace TRAMSAPI.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // Multi-Tenancy & Identity
    public DbSet<TenantMaster> TenantMasters => Set<TenantMaster>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<UserRoleMapping> UserRoleMappings => Set<UserRoleMapping>();
    public DbSet<DriverOperatorDetail> DriverOperatorDetails => Set<DriverOperatorDetail>();
    public DbSet<MenuMaster> MenuMasters => Set<MenuMaster>();
    public DbSet<ActionMaster> ActionMasters => Set<ActionMaster>();
    public DbSet<AccessPermissionMapping> AccessPermissionMappings => Set<AccessPermissionMapping>();
    public DbSet<UserLoginHistory> UserLoginHistories => Set<UserLoginHistory>();
    public DbSet<UserOtpVerification> UserOtpVerifications => Set<UserOtpVerification>();
    public DbSet<MenuActionMapping> MenuActionMappings => Set<MenuActionMapping>();

    // Vehicle Management & Maintenance
    public DbSet<VehicleCategory> VehicleCategories => Set<VehicleCategory>();
    public DbSet<VehicleType> VehicleTypes => Set<VehicleType>();
    public DbSet<VehicleBrand> VehicleBrands => Set<VehicleBrand>();
    public DbSet<FuelType> FuelTypes => Set<FuelType>();
    public DbSet<Vehicle> Vehicles => Set<Vehicle>();
    public DbSet<ImplementationMaster> ImplementationMasters => Set<ImplementationMaster>();
    public DbSet<ImplementAsset> ImplementAssets => Set<ImplementAsset>();
    public DbSet<MaintenanceType> MaintenanceTypes => Set<MaintenanceType>();
    public DbSet<VehicleMaintenanceRecord> VehicleMaintenanceRecords => Set<VehicleMaintenanceRecord>();
    public DbSet<ExpenseCategory> ExpenseCategories => Set<ExpenseCategory>();
    public DbSet<Expense> Expenses => Set<Expense>();

    // Job Management & Financials
    public DbSet<RateCard> RateCards => Set<RateCard>();
    public DbSet<Job> Jobs => Set<Job>();
    public DbSet<AdditionalJob> AdditionalJobs => Set<AdditionalJob>();
    public DbSet<JobPayment> JobPayments => Set<JobPayment>();
    public DbSet<AccountNumber> AccountNumbers => Set<AccountNumber>();
    public DbSet<AmountTally> AmountTallies => Set<AmountTally>();
    public DbSet<SalaryConfig> SalaryConfigs => Set<SalaryConfig>();

    // System & Auditing
    public DbSet<TableList> TablesList => Set<TableList>();
    public DbSet<DataAudit> DataAudits => Set<DataAudit>();
    public DbSet<ErrorLog> ErrorLogs => Set<ErrorLog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply all individual entity configuration files from this assembly
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        // Seed data for Table Master (TableList) from separate seed data file
        TableListSeedData.Seed(modelBuilder);

        // Seed initial Tenant, Developer Role, Admin User and UserRoleMapping
        UserSeedData.Seed(modelBuilder);
    }
}
