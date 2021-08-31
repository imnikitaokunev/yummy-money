using System;
using Bogus;
using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DataGeneration
{
    public class Program
    {
        public static void Main(string[] args)
        {
            #region Data

            // TODO: Use unit of work pattern;

            const string connectionString =
                "data source=(local);Initial Catalog=CostAccounting;Integrated Security=True;";
            //const string connectionString =
            //    "Server=tcp:yummymoney.database.windows.net,1433;Initial Catalog=yummymoneytesting;Persist Security Info=False;User ID=nikitosinos1;Password=13Nikitos;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            //const string connectionString =
            //    "Server=tcp:yummymoney.database.windows.net,1433;Initial Catalog=yummymoneystaging;Persist Security Info=False;User ID=nikitosinos1;Password=13Nikitos;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            var options = new DbContextOptionsBuilder<ApplicationDbContext>();
            options.UseSqlServer(connectionString);

            var context = new ApplicationDbContext(options.Options);

            var categoryRepository = new CategoryRepository(context);
            var expenseRepository = new ExpenseRepository(context);
            var incomeRepository = new IncomeRepository(context);

            #endregion

            Console.WriteLine("[0] Categories");
            Console.WriteLine("[1] Expenses");
            Console.WriteLine("[2] Incomes");

            var parsedCode = int.TryParse(Console.ReadLine(), out var code);

            if (!parsedCode)
            {
                Console.WriteLine("Bad input.");
                return;
            }

            Console.Write("Objects count: ");

            var parsedCount = int.TryParse(Console.ReadLine(), out var count);

            switch (code)
            {
                case 0:
                    CreateCategories();
                    break;
                case 1:
                    CreateExpenses();
                    break;
                case 2:
                    CreateIncomes();
                    break;
            }

            Console.WriteLine($"Created {count} entities.");
            Console.ReadLine();

            #region Categories

            async void CreateCategories()
            {
                var categoryFaker = new Faker<Category>()
                    .RuleFor(x => x.Name, x => x.Commerce.ProductName())
                    .RuleFor(x => x.Description, x => x.Commerce.ProductDescription())
                    .RuleFor(x => x.UserId, new Guid("f2dce61f-828b-4310-0fd0-08d949626d84"));

                var categories = categoryFaker.GenerateLazy(count);

                foreach (var category in categories)
                {
                    category.Description = category.Description.Length > 128
                        ? category.Description.Substring(0, 128)
                        : category.Description;

                    await categoryRepository.AddAsync(category);
                }
            }

            #endregion

            #region Expenses

            async void CreateExpenses()
            {
                var categoryFaker = new Faker<Category>()
                    .RuleFor(x => x.Name, x => x.Commerce.ProductName())
                    .RuleFor(x => x.Description, x => x.Commerce.ProductDescription())
                    .RuleFor(x => x.UserId, new Guid("f2dce61f-828b-4310-0fd0-08d949626d84"));

                var expenseFaker = new Faker<Expense>()
                    .RuleFor(x => x.Category, x => categoryFaker)
                    .RuleFor(x => x.Amount, x => x.Finance.Amount(1, 2048))
                    .RuleFor(x => x.Date, x => x.Date.Between(DateTime.Now.AddMonths(-1), DateTime.Now.AddMonths(1)))
                    .RuleFor(x => x.UserId, x => new Guid("f2dce61f-828b-4310-0fd0-08d949626d84"))
                    .RuleFor(x => x.Description, x => x.Commerce.ProductDescription());

                var expenses = expenseFaker.GenerateLazy(count);

                foreach (var expense in expenses)
                {
                    expense.Category.Description = expense.Category.Description.Length > 128
                        ? expense.Category.Description.Substring(0, 128)
                        : expense.Category.Description;

                    expense.Description = expense.Description.Length > 128
                        ? expense.Description.Substring(0, 128)
                        : expense.Description;

                    await categoryRepository.AddAsync(expense.Category);
                    await expenseRepository.AddAsync(expense);
                }
            }

            #endregion

            #region Incomes

            async void CreateIncomes()
            {
                var categoryFaker = new Faker<Category>()
                    .RuleFor(x => x.Name, x => x.Commerce.ProductName())
                    .RuleFor(x => x.Description, x => x.Commerce.ProductDescription())
                    .RuleFor(x => x.UserId, new Guid("f2dce61f-828b-4310-0fd0-08d949626d84"));

                var incomeFaker = new Faker<Income>()
                    .RuleFor(x => x.Category, x => categoryFaker)
                    .RuleFor(x => x.Amount, x => x.Finance.Amount(1, 2048))
                    .RuleFor(x => x.Date, x => x.Date.Between(DateTime.Now.AddMonths(-1), DateTime.Now.AddMonths(1)))
                    .RuleFor(x => x.UserId, x => new Guid("f2dce61f-828b-4310-0fd0-08d949626d84"))
                    .RuleFor(x => x.Description, x => x.Commerce.ProductDescription());

                var incomes = incomeFaker.GenerateLazy(count);

                foreach (var income in incomes)
                {
                    income.Category.Description = income.Category.Description.Length > 128
                        ? income.Category.Description.Substring(0, 128)
                        : income.Category.Description;

                    income.Description = income.Description.Length > 128
                        ? income.Description.Substring(0, 128)
                        : income.Description;

                    await categoryRepository.AddAsync(income.Category);
                    await incomeRepository.AddAsync(income);
                }
            }

            #endregion
        }
    }
}