using Microsoft.EntityFrameworkCore;
using TRAMSAPI.Domain.Entities;

namespace TRAMSAPI.Infrastructure.Persistence.Seeds;

public static class TableListSeedData
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        var seedDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        modelBuilder.Entity<TableList>().HasData(
            new TableList { TableListId = 1, TableName = "tb_trams_tenant_master", CreatedAt = seedDate },
            new TableList { TableListId = 2, TableName = "tb_trams_user", CreatedAt = seedDate },
            new TableList { TableListId = 3, TableName = "tb_trams_role", CreatedAt = seedDate },
            new TableList { TableListId = 4, TableName = "tb_trams_user_role_mapping", CreatedAt = seedDate },
            new TableList { TableListId = 5, TableName = "tb_trams_driver_operator_detail", CreatedAt = seedDate },
            new TableList { TableListId = 6, TableName = "tb_trams_menu_master", CreatedAt = seedDate },
            new TableList { TableListId = 7, TableName = "tb_trams_action_master", CreatedAt = seedDate },
            new TableList { TableListId = 8, TableName = "tb_trams_access_permission_mapping", CreatedAt = seedDate },
            new TableList { TableListId = 9, TableName = "tb_trams_user_login_history", CreatedAt = seedDate },
            new TableList { TableListId = 10, TableName = "tb_trams_user_otp_verification", CreatedAt = seedDate },
            new TableList { TableListId = 11, TableName = "tb_trams_menu_action_mapping", CreatedAt = seedDate },
            new TableList { TableListId = 12, TableName = "tb_trams_vehicle_category", CreatedAt = seedDate },
            new TableList { TableListId = 13, TableName = "tb_trams_vehicle_type", CreatedAt = seedDate },
            new TableList { TableListId = 14, TableName = "tb_trams_vehicle_brand", CreatedAt = seedDate },
            new TableList { TableListId = 15, TableName = "tb_trams_fuel_type", CreatedAt = seedDate },
            new TableList { TableListId = 16, TableName = "tb_trams_vehicle", CreatedAt = seedDate },
            new TableList { TableListId = 17, TableName = "tb_trams_implementation_master", CreatedAt = seedDate },
            new TableList { TableListId = 18, TableName = "tb_trams_implement_asset", CreatedAt = seedDate },
            new TableList { TableListId = 19, TableName = "tb_trams_maintenance_type", CreatedAt = seedDate },
            new TableList { TableListId = 20, TableName = "tb_trams_vehicle_maintenance_record", CreatedAt = seedDate },
            new TableList { TableListId = 21, TableName = "tb_trams_expense_category", CreatedAt = seedDate },
            new TableList { TableListId = 22, TableName = "tb_trams_expense", CreatedAt = seedDate },
            new TableList { TableListId = 23, TableName = "tb_trams_rate_card", CreatedAt = seedDate },
            new TableList { TableListId = 24, TableName = "tb_trams_job", CreatedAt = seedDate },
            new TableList { TableListId = 25, TableName = "tb_trams_additional_job", CreatedAt = seedDate },
            new TableList { TableListId = 26, TableName = "tb_trams_job_payment", CreatedAt = seedDate },
            new TableList { TableListId = 27, TableName = "tb_trams_account_number", CreatedAt = seedDate },
            new TableList { TableListId = 28, TableName = "tb_trams_amount_tally", CreatedAt = seedDate },
            new TableList { TableListId = 29, TableName = "tb_trams_salary_config", CreatedAt = seedDate },
            new TableList { TableListId = 30, TableName = "tb_trams_table_list", CreatedAt = seedDate },
            new TableList { TableListId = 31, TableName = "tb_trams_data_audit", CreatedAt = seedDate },
            new TableList { TableListId = 32, TableName = "tb_trams_error_log", CreatedAt = seedDate }
        );
    }
}
