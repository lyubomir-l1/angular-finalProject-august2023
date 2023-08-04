using Finances.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Finances.Data
{
    public class ApplicationDbContextInitialiser
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ILogger<ApplicationDbContextInitialiser> logger;

        public ApplicationDbContextInitialiser(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ILogger<ApplicationDbContextInitialiser> logger)
        {
            this.context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.logger = logger;
        }

        public async Task InitialiseAsync()
        {
            try
            {
                if (this.context.Database.IsSqlServer())
                {
                    await this.context.Database.MigrateAsync();
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "An error occurred while initialising the database.");
                throw;
            }
        }

        public async Task SeedAsync()
        {
            try
            {
                await TrySeedAsync();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }
        }

        public async Task TrySeedAsync()
        {
            if (context.IncomeCategories.Any())
            {
                return;
            }

            await SeedRolesAsync();
            await SeedUserAsync();
            await SeedCashflowTypesAsync();
            await SeedExpenseCategories();
            await SeedIncomeCategories();
            await SeedExpensesAsync();
            await SeedIncomesAsync();
        }

        private async Task SeedCashflowTypesAsync()
        {
            var cashflowTypes = new[]
            {
                new CashflowType { Description = "Gross Payment." },
                new CashflowType { Description = "Monthly Bonuses." },
                new CashflowType { Description = "Partner Gross Payment." },
                new CashflowType { Description = "Partner Monthly Bonuses." },
                new CashflowType { Description = "Business, Dividents, Rents and others." },
                new CashflowType { Description = "Pay to yourself first." },
                new CashflowType { Description = "Deductions from Gross Payments." },
                new CashflowType { Description = "Fixed monthly expenses." },
                new CashflowType { Description = "Unfixed monthly expenses." },
                new CashflowType { Description = "Remainder for investments." }
            };

            await context.CashflowTypes.AddRangeAsync(cashflowTypes);
            await context.SaveChangesAsync();
        }

        private async Task SeedIncomesAsync()
        {
            var user = await context.Users.FirstOrDefaultAsync();
            var category = await context.IncomeCategories.FirstOrDefaultAsync();

            var incomes = new[]
            {
                new Income { Merchant = "VSG", CategoryId = category.Id, UserId = user.Id, Date = DateTime.UtcNow, Total = 1156.60M, Note = "" },
                new Income { Merchant = "UniCredit", CategoryId = category.Id, UserId = user.Id, Date = DateTime.UtcNow, Total = 5.80M, Note = "" },
                new Income { Merchant = "University", CategoryId = category.Id, UserId = user.Id, Date = DateTime.UtcNow, Total = 220.60M, Note = "" }
            };

            await context.Incomes.AddRangeAsync(incomes);
            await context.SaveChangesAsync();
        }

        private async Task SeedExpensesAsync()
        {
            var user = await context.Users.FirstOrDefaultAsync();
            var category = await context.ExpenseCategories.FirstOrDefaultAsync();

            var expenses = new[]
            {
                new Expense { Merchant = "EnergoPro", CategoryId = category.Id, UserId = user.Id, Date = DateTime.UtcNow, Total = 23.60M, Note = "" },
                new Expense { Merchant = "Lukoul", CategoryId = category.Id, UserId = user.Id, Date = DateTime.UtcNow, Total = 55.30M, Note = "" },
                new Expense { Merchant = "Hipoland", CategoryId = category.Id, UserId = user.Id, Date = DateTime.UtcNow, Total = 152.80M, Note = "" }
            };

            await context.Expenses.AddRangeAsync(expenses);
            await context.SaveChangesAsync();
        }

        private async Task SeedIncomeCategories()
        {
            var user = await context.Users.FirstOrDefaultAsync();

            var incomeCategories = new[]
            {
                new IncomeCategory { Name = "Salary", TypeId = 1, UserId = user.Id },
                new IncomeCategory { Name = "Bonuses", TypeId = 2, UserId = user.Id },
                new IncomeCategory { Name = "Partner Salary", TypeId = 3, UserId = user.Id  },
                new IncomeCategory { Name = "Partner Bonuses", TypeId = 4, UserId = user.Id },
                new IncomeCategory { Name = "Business", TypeId = 5, UserId = user.Id },
                new IncomeCategory { Name = "Rents", TypeId = 5, UserId = user.Id },
                new IncomeCategory { Name = "Horariums", TypeId = 5, UserId = user.Id },
                new IncomeCategory { Name = "Dividents", TypeId = 5, UserId = user.Id },
                new IncomeCategory { Name = "Others", TypeId = 5, UserId = user.Id }
            };

            await context.IncomeCategories.AddRangeAsync(incomeCategories);
            await context.SaveChangesAsync();
        }

        private async Task SeedExpenseCategories()
        {
            var user = await context.Users.FirstOrDefaultAsync();

            var expenseCategories = new[]
            {
                new ExpenseCategory { Name = "Taxes", TypeId = 7, UserId = user.Id },
                new ExpenseCategory { Name = "Energy Bills", TypeId = 8, UserId = user.Id },
                new ExpenseCategory { Name = "Water Bills", TypeId = 8, UserId = user.Id },
                new ExpenseCategory { Name = "Phone Bills", TypeId = 8, UserId = user.Id },
                new ExpenseCategory { Name = "Home Bills", TypeId = 8, UserId = user.Id },
                new ExpenseCategory { Name = "I-net/TV Bills", TypeId = 8, UserId = user.Id },
                new ExpenseCategory { Name = "Car Maintenance", TypeId = 8, UserId = user.Id },
                new ExpenseCategory { Name = "Rents", TypeId = 8, UserId = user.Id },
                new ExpenseCategory { Name = "Food", TypeId = 9, UserId = user.Id },
                new ExpenseCategory { Name = "Childern", TypeId = 9, UserId = user.Id },
                new ExpenseCategory { Name = "Home Maintenance", TypeId = 9, UserId = user.Id },
                new ExpenseCategory { Name = "Cosmetics", TypeId = 9, UserId = user.Id  },
                new ExpenseCategory { Name = "Clothes and Shoes", TypeId = 9, UserId = user.Id },
                new ExpenseCategory { Name = "Outlook", TypeId = 8, UserId = user.Id },
                new ExpenseCategory { Name = "Insurances", TypeId = 8, UserId = user.Id },
                new ExpenseCategory { Name = "Business", TypeId = 9, UserId = user.Id },
                new ExpenseCategory { Name = "Gifts", TypeId = 9, UserId = user.Id },
                new ExpenseCategory { Name = "Fun", TypeId = 9, UserId = user.Id },
                new ExpenseCategory { Name = "Rest", TypeId = 9, UserId = user.Id },
                new ExpenseCategory { Name = "Sport", TypeId = 9, UserId = user.Id },
                new ExpenseCategory { Name = "Others", TypeId = 9, UserId = user.Id }
            };

            await context.ExpenseCategories.AddRangeAsync(expenseCategories);
            await context.SaveChangesAsync();
        }

        private async Task SeedRolesAsync()
        {
            await roleManager.CreateAsync(new IdentityRole("Administrator"));
            await roleManager.CreateAsync(new IdentityRole("User"));

            await context.SaveChangesAsync();
        }

        private async Task SeedUserAsync()
        {
            if (userManager.FindByNameAsync("admin@admin.bg").Result == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = "admin@admin.bg";
                user.Email = "admin@admin.bg";

                await userManager.CreateAsync(user, "OfisaBatko4321!");
            }
        }
    }
}
