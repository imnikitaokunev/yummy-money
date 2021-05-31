using System;
using Bogus;
using CostAccounting.Core.Entities.Core;
using CostAccounting.Data.EntityFramework;
using CostAccounting.Data.EntityFramework.Repositories.Core;
using Microsoft.EntityFrameworkCore;

namespace CostAccounting.DataGeneration
{
    public class Program
    {
        public static void Main(string[] args)
        {
            #region Data

            // TODO: Use unit of work pattern;

            const string connectionString =
                "data source=(local);Initial Catalog=CostAccounting;Integrated Security=True;";
            const string connectionStringTesting =
                "Server=tcp:yummymoney.database.windows.net,1433;Initial Catalog=yummymoneytesting;Persist Security Info=False;User ID=nikitosinos1;Password=13Nikitos;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            const string connectionStringStaging =
                "Server=tcp:yummymoney.database.windows.net,1433;Initial Catalog=yummymoneystaging;Persist Security Info=False;User ID=nikitosinos1;Password=13Nikitos;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            var options = new DbContextOptionsBuilder<CostAccountingContext>();
            options.UseSqlServer(connectionString);

            var context = new CostAccountingContext(options.Options);

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

            void CreateCategories()
            {
                var categoryFaker = new Faker<Category>()
                    .RuleFor(x => x.Name, x => x.Commerce.ProductName())
                    .RuleFor(x => x.Description, x => x.Commerce.ProductDescription());

                var categories = categoryFaker.GenerateLazy(count);

                foreach (var category in categories)
                {
                    category.Description = category.Description.Length > Category.DescriptionLength
                        ? category.Description.Substring(0, Category.DescriptionLength)
                        : category.Description;

                    categoryRepository.Create(category);
                }

                categoryRepository.Save();
            }

            #endregion

            #region Expenses

            void CreateExpenses()
            {
                var categoryFaker = new Faker<Category>()
                    .RuleFor(x => x.Name, x => x.Commerce.ProductName())
                    .RuleFor(x => x.Description, x => x.Commerce.ProductDescription());

                var expenseFaker = new Faker<Expense>()
                    .RuleFor(x => x.Category, x => categoryFaker)
                    .RuleFor(x => x.Amount, x => x.Finance.Amount(1, 2048))
                    .RuleFor(x => x.Date, x => x.Date.Between(DateTime.Now.AddMonths(-1), DateTime.Now.AddMonths(1)))
                    .RuleFor(x => x.UserId, x => new Guid("d8d3b441-b5b8-eb11-8777-c83dd4f6a363"))
                    .RuleFor(x => x.Description, x => x.Commerce.ProductDescription());

                var expenses = expenseFaker.GenerateLazy(count);

                foreach (var expense in expenses)
                {
                    expense.Category.Description = expense.Category.Description.Length > Category.DescriptionLength
                        ? expense.Category.Description.Substring(0, Category.DescriptionLength)
                        : expense.Category.Description;

                    expense.Description = expense.Description.Length > Expense.DescriptionLength
                        ? expense.Description.Substring(0, Expense.DescriptionLength)
                        : expense.Description;

                    categoryRepository.Create(expense.Category);
                    expenseRepository.Create(expense);
                }

                categoryRepository.Save();
                expenseRepository.Save();
            }

            #endregion

            #region Incomes

            void CreateIncomes()
            {
                var categoryFaker = new Faker<Category>()
                    .RuleFor(x => x.Name, x => x.Commerce.ProductName())
                    .RuleFor(x => x.Description, x => x.Commerce.ProductDescription());

                var incomeFaker = new Faker<Income>()
                    .RuleFor(x => x.Category, x => categoryFaker)
                    .RuleFor(x => x.Amount, x => x.Finance.Amount(1, 2048))
                    .RuleFor(x => x.Date, x => x.Date.Between(DateTime.Now.AddMonths(-1), DateTime.Now.AddMonths(1)))
                    .RuleFor(x => x.UserId, x => new Guid("d8d3b441-b5b8-eb11-8777-c83dd4f6a363"))
                    .RuleFor(x => x.Description, x => x.Commerce.ProductDescription());

                var incomes = incomeFaker.GenerateLazy(count);

                foreach (var income in incomes)
                {
                    income.Category.Description = income.Category.Description.Length > Category.DescriptionLength
                        ? income.Category.Description.Substring(0, Category.DescriptionLength)
                        : income.Category.Description;

                    income.Description = income.Description.Length > Income.DescriptionLength
                        ? income.Description.Substring(0, Income.DescriptionLength)
                        : income.Description;

                    categoryRepository.Create(income.Category);
                    incomeRepository.Create(income);
                }

                categoryRepository.Save();
                incomeRepository.Save();
            }

            #endregion
        }
    }
}