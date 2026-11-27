using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TRAMSAPI.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_trams_action_master",
                columns: table => new
                {
                    action_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    action_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_trams_action_master", x => x.action_id);
                });

            migrationBuilder.CreateTable(
                name: "tb_trams_data_audit",
                columns: table => new
                {
                    data_audit_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tenant_id = table.Column<long>(type: "bigint", nullable: true),
                    table_name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    record_id = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    action = table.Column<int>(type: "integer", nullable: false),
                    old_values = table.Column<string>(type: "text", nullable: true),
                    new_values = table.Column<string>(type: "text", nullable: true),
                    changed_columns = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    user_id = table.Column<long>(type: "bigint", nullable: true),
                    timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_trams_data_audit", x => x.data_audit_id);
                });

            migrationBuilder.CreateTable(
                name: "tb_trams_error_log",
                columns: table => new
                {
                    error_log_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tenant_id = table.Column<long>(type: "bigint", nullable: true),
                    user_id = table.Column<long>(type: "bigint", nullable: true),
                    request_path = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    http_method = table.Column<int>(type: "integer", nullable: true),
                    status_code = table.Column<int>(type: "integer", nullable: true),
                    error_message = table.Column<string>(type: "text", nullable: true),
                    exception_type = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    stack_trace = table.Column<string>(type: "text", nullable: true),
                    source = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_trams_error_log", x => x.error_log_id);
                });

            migrationBuilder.CreateTable(
                name: "tb_trams_expense_category",
                columns: table => new
                {
                    expense_category_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tenant_id = table.Column<long>(type: "bigint", nullable: false),
                    category_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_trams_expense_category", x => x.expense_category_id);
                });

            migrationBuilder.CreateTable(
                name: "tb_trams_fuel_type",
                columns: table => new
                {
                    fuel_type_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tenant_id = table.Column<long>(type: "bigint", nullable: false),
                    fuel_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_trams_fuel_type", x => x.fuel_type_id);
                });

            migrationBuilder.CreateTable(
                name: "tb_trams_implement_asset",
                columns: table => new
                {
                    implement_asset_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tenant_id = table.Column<long>(type: "bigint", nullable: false),
                    implement_master_id = table.Column<long>(type: "bigint", nullable: true),
                    implement_code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    ownership_type = table.Column<int>(type: "integer", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    image_url = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_trams_implement_asset", x => x.implement_asset_id);
                });

            migrationBuilder.CreateTable(
                name: "tb_trams_implementation_master",
                columns: table => new
                {
                    implementation_master_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tenant_id = table.Column<long>(type: "bigint", nullable: false),
                    vehicle_category_id = table.Column<long>(type: "bigint", nullable: true),
                    implementation_name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_trams_implementation_master", x => x.implementation_master_id);
                });

            migrationBuilder.CreateTable(
                name: "tb_trams_job_payment",
                columns: table => new
                {
                    job_payment_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tenant_id = table.Column<long>(type: "bigint", nullable: false),
                    job_key = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    collected_amount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    discount = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    settlement_status = table.Column<int>(type: "integer", nullable: true),
                    payment_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<long>(type: "bigint", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_by = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_trams_job_payment", x => x.job_payment_id);
                });

            migrationBuilder.CreateTable(
                name: "tb_trams_maintenance_type",
                columns: table => new
                {
                    maintenance_type_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tenant_id = table.Column<long>(type: "bigint", nullable: false),
                    maintenance_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_trams_maintenance_type", x => x.maintenance_type_id);
                });

            migrationBuilder.CreateTable(
                name: "tb_trams_menu_master",
                columns: table => new
                {
                    menu_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    menu_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    menu_key = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_trams_menu_master", x => x.menu_id);
                });

            migrationBuilder.CreateTable(
                name: "tb_trams_table_list",
                columns: table => new
                {
                    table_list_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    table_name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_trams_table_list", x => x.table_list_id);
                });

            migrationBuilder.CreateTable(
                name: "tb_trams_tenant_master",
                columns: table => new
                {
                    tenant_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    business_name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    tenant_code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    company_email = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    company_phone_no = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<long>(type: "bigint", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_by = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_trams_tenant_master", x => x.tenant_id);
                });

            migrationBuilder.CreateTable(
                name: "tb_trams_vehicle_brand",
                columns: table => new
                {
                    vehicle_brand_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tenant_id = table.Column<long>(type: "bigint", nullable: false),
                    brand_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_trams_vehicle_brand", x => x.vehicle_brand_id);
                });

            migrationBuilder.CreateTable(
                name: "tb_trams_vehicle_category",
                columns: table => new
                {
                    vehicle_category_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tenant_id = table.Column<long>(type: "bigint", nullable: false),
                    category_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_trams_vehicle_category", x => x.vehicle_category_id);
                });

            migrationBuilder.CreateTable(
                name: "tb_trams_menu_action_mapping",
                columns: table => new
                {
                    menu_action_mapping_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    menu_id = table.Column<long>(type: "bigint", nullable: false),
                    action_id = table.Column<long>(type: "bigint", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_trams_menu_action_mapping", x => x.menu_action_mapping_id);
                    table.ForeignKey(
                        name: "FK_tb_trams_menu_action_mapping_tb_trams_action_master_action_~",
                        column: x => x.action_id,
                        principalTable: "tb_trams_action_master",
                        principalColumn: "action_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_trams_menu_action_mapping_tb_trams_menu_master_menu_id",
                        column: x => x.menu_id,
                        principalTable: "tb_trams_menu_master",
                        principalColumn: "menu_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_trams_role",
                columns: table => new
                {
                    role_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tenant_id = table.Column<long>(type: "bigint", nullable: false),
                    role_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_trams_role", x => x.role_id);
                    table.ForeignKey(
                        name: "FK_tb_trams_role_tb_trams_tenant_master_tenant_id",
                        column: x => x.tenant_id,
                        principalTable: "tb_trams_tenant_master",
                        principalColumn: "tenant_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tb_trams_user",
                columns: table => new
                {
                    user_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tenant_id = table.Column<long>(type: "bigint", nullable: false),
                    name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    email = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    phone_no = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    alternate_phone_no = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    address = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    ProfileImage = table.Column<byte[]>(type: "bytea", nullable: true),
                    profile_image_mime = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    password_hash = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    is_system_user = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<long>(type: "bigint", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_by = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_trams_user", x => x.user_id);
                    table.ForeignKey(
                        name: "FK_tb_trams_user_tb_trams_tenant_master_tenant_id",
                        column: x => x.tenant_id,
                        principalTable: "tb_trams_tenant_master",
                        principalColumn: "tenant_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tb_trams_vehicle_type",
                columns: table => new
                {
                    vehicle_type_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tenant_id = table.Column<long>(type: "bigint", nullable: false),
                    vehicle_category_id = table.Column<long>(type: "bigint", nullable: false),
                    type_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_trams_vehicle_type", x => x.vehicle_type_id);
                    table.ForeignKey(
                        name: "FK_tb_trams_vehicle_type_tb_trams_vehicle_category_vehicle_cat~",
                        column: x => x.vehicle_category_id,
                        principalTable: "tb_trams_vehicle_category",
                        principalColumn: "vehicle_category_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tb_trams_access_permission_mapping",
                columns: table => new
                {
                    access_permission_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tenant_id = table.Column<long>(type: "bigint", nullable: false),
                    role_id = table.Column<long>(type: "bigint", nullable: true),
                    user_id = table.Column<long>(type: "bigint", nullable: true),
                    action_id = table.Column<long>(type: "bigint", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_trams_access_permission_mapping", x => x.access_permission_id);
                    table.ForeignKey(
                        name: "FK_tb_trams_access_permission_mapping_tb_trams_action_master_a~",
                        column: x => x.action_id,
                        principalTable: "tb_trams_action_master",
                        principalColumn: "action_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_trams_access_permission_mapping_tb_trams_role_role_id",
                        column: x => x.role_id,
                        principalTable: "tb_trams_role",
                        principalColumn: "role_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_trams_access_permission_mapping_tb_trams_tenant_master_t~",
                        column: x => x.tenant_id,
                        principalTable: "tb_trams_tenant_master",
                        principalColumn: "tenant_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tb_trams_access_permission_mapping_tb_trams_user_user_id",
                        column: x => x.user_id,
                        principalTable: "tb_trams_user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_trams_account_number",
                columns: table => new
                {
                    account_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tenant_id = table.Column<long>(type: "bigint", nullable: false),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    account_no = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    nonencrypted = table.Column<int>(type: "integer", nullable: false),
                    bank_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ifsc = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    branch_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    mobile_number = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_trams_account_number", x => x.account_id);
                    table.ForeignKey(
                        name: "FK_tb_trams_account_number_tb_trams_user_user_id",
                        column: x => x.user_id,
                        principalTable: "tb_trams_user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tb_trams_amount_tally",
                columns: table => new
                {
                    amount_tally_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tenant_id = table.Column<long>(type: "bigint", nullable: false),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    amount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    transaction_type = table.Column<int>(type: "integer", nullable: false),
                    incentive = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_trams_amount_tally", x => x.amount_tally_id);
                    table.ForeignKey(
                        name: "FK_tb_trams_amount_tally_tb_trams_user_user_id",
                        column: x => x.user_id,
                        principalTable: "tb_trams_user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tb_trams_driver_operator_detail",
                columns: table => new
                {
                    driver_operator_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tenant_id = table.Column<long>(type: "bigint", nullable: false),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    license_no = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    license_expiry_date = table.Column<DateOnly>(type: "date", nullable: true),
                    experience_years = table.Column<int>(type: "integer", nullable: true),
                    specialization = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_trams_driver_operator_detail", x => x.driver_operator_id);
                    table.ForeignKey(
                        name: "FK_tb_trams_driver_operator_detail_tb_trams_tenant_master_tena~",
                        column: x => x.tenant_id,
                        principalTable: "tb_trams_tenant_master",
                        principalColumn: "tenant_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tb_trams_driver_operator_detail_tb_trams_user_user_id",
                        column: x => x.user_id,
                        principalTable: "tb_trams_user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_trams_salary_config",
                columns: table => new
                {
                    salary_config_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    salary_amount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    salary_type = table.Column<int>(type: "integer", nullable: false),
                    tenant_id = table.Column<long>(type: "bigint", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_trams_salary_config", x => x.salary_config_id);
                    table.ForeignKey(
                        name: "FK_tb_trams_salary_config_tb_trams_user_user_id",
                        column: x => x.user_id,
                        principalTable: "tb_trams_user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tb_trams_user_login_history",
                columns: table => new
                {
                    login_history_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tenant_id = table.Column<long>(type: "bigint", nullable: true),
                    user_id = table.Column<long>(type: "bigint", nullable: true),
                    event_type = table.Column<int>(type: "integer", nullable: false),
                    event_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ip_address = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    device_info = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_trams_user_login_history", x => x.login_history_id);
                    table.ForeignKey(
                        name: "FK_tb_trams_user_login_history_tb_trams_tenant_master_tenant_id",
                        column: x => x.tenant_id,
                        principalTable: "tb_trams_tenant_master",
                        principalColumn: "tenant_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tb_trams_user_login_history_tb_trams_user_user_id",
                        column: x => x.user_id,
                        principalTable: "tb_trams_user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tb_trams_user_otp_verification",
                columns: table => new
                {
                    otp_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    otp_hash = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    purpose = table.Column<int>(type: "integer", nullable: false),
                    is_verified = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    attempt_count = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_trams_user_otp_verification", x => x.otp_id);
                    table.ForeignKey(
                        name: "FK_tb_trams_user_otp_verification_tb_trams_user_user_id",
                        column: x => x.user_id,
                        principalTable: "tb_trams_user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_trams_user_role_mapping",
                columns: table => new
                {
                    user_role_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tenant_id = table.Column<long>(type: "bigint", nullable: false),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    role_id = table.Column<long>(type: "bigint", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_trams_user_role_mapping", x => x.user_role_id);
                    table.ForeignKey(
                        name: "FK_tb_trams_user_role_mapping_tb_trams_role_role_id",
                        column: x => x.role_id,
                        principalTable: "tb_trams_role",
                        principalColumn: "role_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_trams_user_role_mapping_tb_trams_tenant_master_tenant_id",
                        column: x => x.tenant_id,
                        principalTable: "tb_trams_tenant_master",
                        principalColumn: "tenant_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tb_trams_user_role_mapping_tb_trams_user_user_id",
                        column: x => x.user_id,
                        principalTable: "tb_trams_user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_trams_vehicle",
                columns: table => new
                {
                    vehicle_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tenant_id = table.Column<long>(type: "bigint", nullable: false),
                    vehicle_type_id = table.Column<long>(type: "bigint", nullable: false),
                    vehicle_brand_id = table.Column<long>(type: "bigint", nullable: false),
                    fuel_type_id = table.Column<long>(type: "bigint", nullable: false),
                    vehicle_number = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    model_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    manufacturing_year = table.Column<int>(type: "integer", nullable: true),
                    purchase_date = table.Column<DateOnly>(type: "date", nullable: true),
                    purchase_price = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    current_kilometers = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    top_speed = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    is_implement = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    ownership_type = table.Column<int>(type: "integer", nullable: false),
                    vehicle_status = table.Column<int>(type: "integer", nullable: false),
                    image_url = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<long>(type: "bigint", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_by = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_trams_vehicle", x => x.vehicle_id);
                    table.ForeignKey(
                        name: "FK_tb_trams_vehicle_tb_trams_fuel_type_fuel_type_id",
                        column: x => x.fuel_type_id,
                        principalTable: "tb_trams_fuel_type",
                        principalColumn: "fuel_type_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tb_trams_vehicle_tb_trams_vehicle_brand_vehicle_brand_id",
                        column: x => x.vehicle_brand_id,
                        principalTable: "tb_trams_vehicle_brand",
                        principalColumn: "vehicle_brand_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tb_trams_vehicle_tb_trams_vehicle_type_vehicle_type_id",
                        column: x => x.vehicle_type_id,
                        principalTable: "tb_trams_vehicle_type",
                        principalColumn: "vehicle_type_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tb_trams_job",
                columns: table => new
                {
                    job_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tenant_id = table.Column<long>(type: "bigint", nullable: false),
                    vehicle_id = table.Column<long>(type: "bigint", nullable: false),
                    driver_id = table.Column<long>(type: "bigint", nullable: false),
                    customer_id = table.Column<long>(type: "bigint", nullable: false),
                    from = table.Column<int>(type: "integer", nullable: false),
                    to = table.Column<int>(type: "integer", nullable: true),
                    additional_details = table.Column<int>(type: "integer", nullable: true),
                    total_amount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    job_key = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<long>(type: "bigint", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_by = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_trams_job", x => x.job_id);
                    table.ForeignKey(
                        name: "FK_tb_trams_job_tb_trams_user_driver_id",
                        column: x => x.driver_id,
                        principalTable: "tb_trams_user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tb_trams_job_tb_trams_vehicle_vehicle_id",
                        column: x => x.vehicle_id,
                        principalTable: "tb_trams_vehicle",
                        principalColumn: "vehicle_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tb_trams_rate_card",
                columns: table => new
                {
                    rate_card_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tenant_id = table.Column<long>(type: "bigint", nullable: false),
                    vehicle_id = table.Column<long>(type: "bigint", nullable: false),
                    implement_asset_id = table.Column<long>(type: "bigint", nullable: true),
                    calculation_type_1 = table.Column<int>(type: "integer", nullable: false),
                    calculation_type_2 = table.Column<int>(type: "integer", nullable: true),
                    amount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    waiting_time_charge = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<long>(type: "bigint", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_by = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_trams_rate_card", x => x.rate_card_id);
                    table.ForeignKey(
                        name: "FK_tb_trams_rate_card_tb_trams_implement_asset_implement_asset~",
                        column: x => x.implement_asset_id,
                        principalTable: "tb_trams_implement_asset",
                        principalColumn: "implement_asset_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tb_trams_rate_card_tb_trams_vehicle_vehicle_id",
                        column: x => x.vehicle_id,
                        principalTable: "tb_trams_vehicle",
                        principalColumn: "vehicle_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tb_trams_vehicle_maintenance_record",
                columns: table => new
                {
                    vehicle_maintenance_record_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tenant_id = table.Column<long>(type: "bigint", nullable: false),
                    vehicle_id = table.Column<long>(type: "bigint", nullable: false),
                    maintenance_type_id = table.Column<long>(type: "bigint", nullable: false),
                    service_date = table.Column<DateOnly>(type: "date", nullable: false),
                    service_reading = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    service_performed_by = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    service_provider_name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    next_service_reading = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_trams_vehicle_maintenance_record", x => x.vehicle_maintenance_record_id);
                    table.ForeignKey(
                        name: "FK_tb_trams_vehicle_maintenance_record_tb_trams_maintenance_ty~",
                        column: x => x.maintenance_type_id,
                        principalTable: "tb_trams_maintenance_type",
                        principalColumn: "maintenance_type_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tb_trams_vehicle_maintenance_record_tb_trams_vehicle_vehicl~",
                        column: x => x.vehicle_id,
                        principalTable: "tb_trams_vehicle",
                        principalColumn: "vehicle_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tb_trams_additional_job",
                columns: table => new
                {
                    waiting_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tenant_id = table.Column<long>(type: "bigint", nullable: false),
                    job_id = table.Column<long>(type: "bigint", nullable: false),
                    waiting_start_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    waiting_end_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    amount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<long>(type: "bigint", nullable: true),
                    reason = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_trams_additional_job", x => x.waiting_id);
                    table.ForeignKey(
                        name: "FK_tb_trams_additional_job_tb_trams_job_job_id",
                        column: x => x.job_id,
                        principalTable: "tb_trams_job",
                        principalColumn: "job_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_trams_expense",
                columns: table => new
                {
                    expense_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tenant_id = table.Column<long>(type: "bigint", nullable: false),
                    expense_category_id = table.Column<long>(type: "bigint", nullable: false),
                    is_for_vehicle = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    vehicle_id = table.Column<long>(type: "bigint", nullable: true),
                    vehicle_maintenance_record_id = table.Column<long>(type: "bigint", nullable: true),
                    expense_date = table.Column<DateOnly>(type: "date", nullable: false),
                    title = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    amount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    payment_mode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    vendor_name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    bill_number = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_trams_expense", x => x.expense_id);
                    table.ForeignKey(
                        name: "FK_tb_trams_expense_tb_trams_expense_category_expense_category~",
                        column: x => x.expense_category_id,
                        principalTable: "tb_trams_expense_category",
                        principalColumn: "expense_category_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tb_trams_expense_tb_trams_vehicle_maintenance_record_vehicl~",
                        column: x => x.vehicle_maintenance_record_id,
                        principalTable: "tb_trams_vehicle_maintenance_record",
                        principalColumn: "vehicle_maintenance_record_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tb_trams_expense_tb_trams_vehicle_vehicle_id",
                        column: x => x.vehicle_id,
                        principalTable: "tb_trams_vehicle",
                        principalColumn: "vehicle_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "tb_trams_table_list",
                columns: new[] { "table_list_id", "created_at", "table_name" },
                values: new object[,]
                {
                    { 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "tb_trams_tenant_master" },
                    { 2L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "tb_trams_user" },
                    { 3L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "tb_trams_role" },
                    { 4L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "tb_trams_user_role_mapping" },
                    { 5L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "tb_trams_driver_operator_detail" },
                    { 6L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "tb_trams_menu_master" },
                    { 7L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "tb_trams_action_master" },
                    { 8L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "tb_trams_access_permission_mapping" },
                    { 9L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "tb_trams_user_login_history" },
                    { 10L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "tb_trams_user_otp_verification" },
                    { 11L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "tb_trams_menu_action_mapping" },
                    { 12L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "tb_trams_vehicle_category" },
                    { 13L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "tb_trams_vehicle_type" },
                    { 14L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "tb_trams_vehicle_brand" },
                    { 15L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "tb_trams_fuel_type" },
                    { 16L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "tb_trams_vehicle" },
                    { 17L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "tb_trams_implementation_master" },
                    { 18L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "tb_trams_implement_asset" },
                    { 19L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "tb_trams_maintenance_type" },
                    { 20L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "tb_trams_vehicle_maintenance_record" },
                    { 21L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "tb_trams_expense_category" },
                    { 22L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "tb_trams_expense" },
                    { 23L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "tb_trams_rate_card" },
                    { 24L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "tb_trams_job" },
                    { 25L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "tb_trams_additional_job" },
                    { 26L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "tb_trams_job_payment" },
                    { 27L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "tb_trams_account_number" },
                    { 28L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "tb_trams_amount_tally" },
                    { 29L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "tb_trams_salary_config" },
                    { 30L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "tb_trams_table_list" },
                    { 31L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "tb_trams_data_audit" },
                    { 32L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "tb_trams_error_log" }
                });

            migrationBuilder.InsertData(
                table: "tb_trams_tenant_master",
                columns: new[] { "tenant_id", "business_name", "company_email", "company_phone_no", "created_at", "created_by", "is_active", "tenant_code", "updated_at", "updated_by" },
                values: new object[] { 1L, "NavShift System", "admin123@gmail.com", "0000000000", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, "DEFAULT", null, null });

            migrationBuilder.InsertData(
                table: "tb_trams_role",
                columns: new[] { "role_id", "created_at", "created_by", "description", "is_active", "role_name", "tenant_id" },
                values: new object[] { 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Developer with system administrative privileges", true, "Developer", 1L });

            migrationBuilder.InsertData(
                table: "tb_trams_user",
                columns: new[] { "user_id", "address", "alternate_phone_no", "created_at", "created_by", "email", "is_active", "is_system_user", "name", "password_hash", "phone_no", "ProfileImage", "profile_image_mime", "tenant_id", "updated_at", "updated_by" },
                values: new object[] { 1L, null, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "admin123@gmail.com", true, true, "Admin", "$2a$11$e8xr3VpVy9w5RZZaH6N5quaU4ljjMCa2WVAgZR5.TNKY35yvnLKve", "0000000000", null, null, 1L, null, null });

            migrationBuilder.InsertData(
                table: "tb_trams_user_role_mapping",
                columns: new[] { "user_role_id", "created_at", "created_by", "is_active", "role_id", "tenant_id", "user_id" },
                values: new object[] { 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, 1L, 1L, 1L });

            migrationBuilder.CreateIndex(
                name: "IX_tb_trams_access_permission_mapping_action_id",
                table: "tb_trams_access_permission_mapping",
                column: "action_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_trams_access_permission_mapping_role_id",
                table: "tb_trams_access_permission_mapping",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_trams_access_permission_mapping_tenant_id",
                table: "tb_trams_access_permission_mapping",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_trams_access_permission_mapping_user_id",
                table: "tb_trams_access_permission_mapping",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_trams_account_number_user_id",
                table: "tb_trams_account_number",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_trams_additional_job_job_id",
                table: "tb_trams_additional_job",
                column: "job_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_trams_amount_tally_user_id",
                table: "tb_trams_amount_tally",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_trams_driver_operator_detail_tenant_id_user_id",
                table: "tb_trams_driver_operator_detail",
                columns: new[] { "tenant_id", "user_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_trams_driver_operator_detail_user_id",
                table: "tb_trams_driver_operator_detail",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_trams_expense_expense_category_id",
                table: "tb_trams_expense",
                column: "expense_category_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_trams_expense_vehicle_id",
                table: "tb_trams_expense",
                column: "vehicle_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_trams_expense_vehicle_maintenance_record_id",
                table: "tb_trams_expense",
                column: "vehicle_maintenance_record_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_trams_job_driver_id",
                table: "tb_trams_job",
                column: "driver_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_trams_job_vehicle_id",
                table: "tb_trams_job",
                column: "vehicle_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_trams_menu_action_mapping_action_id",
                table: "tb_trams_menu_action_mapping",
                column: "action_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_trams_menu_action_mapping_menu_id",
                table: "tb_trams_menu_action_mapping",
                column: "menu_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_trams_menu_master_menu_key",
                table: "tb_trams_menu_master",
                column: "menu_key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_trams_rate_card_implement_asset_id",
                table: "tb_trams_rate_card",
                column: "implement_asset_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_trams_rate_card_vehicle_id",
                table: "tb_trams_rate_card",
                column: "vehicle_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_trams_role_tenant_id_role_name",
                table: "tb_trams_role",
                columns: new[] { "tenant_id", "role_name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_trams_salary_config_user_id",
                table: "tb_trams_salary_config",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_trams_tenant_master_tenant_code",
                table: "tb_trams_tenant_master",
                column: "tenant_code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_trams_user_tenant_id",
                table: "tb_trams_user",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_trams_user_login_history_tenant_id",
                table: "tb_trams_user_login_history",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_trams_user_login_history_user_id",
                table: "tb_trams_user_login_history",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_trams_user_otp_verification_user_id",
                table: "tb_trams_user_otp_verification",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_trams_user_role_mapping_role_id",
                table: "tb_trams_user_role_mapping",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_trams_user_role_mapping_tenant_id",
                table: "tb_trams_user_role_mapping",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_trams_user_role_mapping_user_id",
                table: "tb_trams_user_role_mapping",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_trams_vehicle_fuel_type_id",
                table: "tb_trams_vehicle",
                column: "fuel_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_trams_vehicle_vehicle_brand_id",
                table: "tb_trams_vehicle",
                column: "vehicle_brand_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_trams_vehicle_vehicle_type_id",
                table: "tb_trams_vehicle",
                column: "vehicle_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_trams_vehicle_maintenance_record_maintenance_type_id",
                table: "tb_trams_vehicle_maintenance_record",
                column: "maintenance_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_trams_vehicle_maintenance_record_vehicle_id",
                table: "tb_trams_vehicle_maintenance_record",
                column: "vehicle_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_trams_vehicle_type_vehicle_category_id",
                table: "tb_trams_vehicle_type",
                column: "vehicle_category_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_trams_access_permission_mapping");

            migrationBuilder.DropTable(
                name: "tb_trams_account_number");

            migrationBuilder.DropTable(
                name: "tb_trams_additional_job");

            migrationBuilder.DropTable(
                name: "tb_trams_amount_tally");

            migrationBuilder.DropTable(
                name: "tb_trams_data_audit");

            migrationBuilder.DropTable(
                name: "tb_trams_driver_operator_detail");

            migrationBuilder.DropTable(
                name: "tb_trams_error_log");

            migrationBuilder.DropTable(
                name: "tb_trams_expense");

            migrationBuilder.DropTable(
                name: "tb_trams_implementation_master");

            migrationBuilder.DropTable(
                name: "tb_trams_job_payment");

            migrationBuilder.DropTable(
                name: "tb_trams_menu_action_mapping");

            migrationBuilder.DropTable(
                name: "tb_trams_rate_card");

            migrationBuilder.DropTable(
                name: "tb_trams_salary_config");

            migrationBuilder.DropTable(
                name: "tb_trams_table_list");

            migrationBuilder.DropTable(
                name: "tb_trams_user_login_history");

            migrationBuilder.DropTable(
                name: "tb_trams_user_otp_verification");

            migrationBuilder.DropTable(
                name: "tb_trams_user_role_mapping");

            migrationBuilder.DropTable(
                name: "tb_trams_job");

            migrationBuilder.DropTable(
                name: "tb_trams_expense_category");

            migrationBuilder.DropTable(
                name: "tb_trams_vehicle_maintenance_record");

            migrationBuilder.DropTable(
                name: "tb_trams_action_master");

            migrationBuilder.DropTable(
                name: "tb_trams_menu_master");

            migrationBuilder.DropTable(
                name: "tb_trams_implement_asset");

            migrationBuilder.DropTable(
                name: "tb_trams_role");

            migrationBuilder.DropTable(
                name: "tb_trams_user");

            migrationBuilder.DropTable(
                name: "tb_trams_maintenance_type");

            migrationBuilder.DropTable(
                name: "tb_trams_vehicle");

            migrationBuilder.DropTable(
                name: "tb_trams_tenant_master");

            migrationBuilder.DropTable(
                name: "tb_trams_fuel_type");

            migrationBuilder.DropTable(
                name: "tb_trams_vehicle_brand");

            migrationBuilder.DropTable(
                name: "tb_trams_vehicle_type");

            migrationBuilder.DropTable(
                name: "tb_trams_vehicle_category");
        }
    }
}
