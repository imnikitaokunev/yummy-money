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

            const string connectionString = "data source=(local);Initial Catalog=CostAccounting;Integrated Security=True;";

            var options = new DbContextOptionsBuilder<ApplicationDbContext>();
            options.UseSqlServer(connectionString);

            var context = new ApplicationDbContext(options.Options, null);

            var categoryRepository = new CategoryRepository(context);
            var transactionRepository = new TransactionRepository(context);

            #endregion

            Console.WriteLine("[0] Categories");
            Console.WriteLine("[1] Transactions");

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
                    CreateTransactions();
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
                    .RuleFor(x => x.UserId, new Guid("F2DCE61F-828B-4310-0FD0-08D949626D84"));

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

            async void CreateTransactions()
            {
                var categoryFaker = new Faker<Category>()
                    .RuleFor(x => x.Name, x => x.Commerce.ProductName())
                    .RuleFor(x => x.Description, x => x.Commerce.ProductDescription())
                    .RuleFor(x => x.UserId, new Guid("F2DCE61F-828B-4310-0FD0-08D949626D84"));

                var transactionFaker = new Faker<Transaction>()
                    .RuleFor(x => x.Category, x => categoryFaker)
                    .RuleFor(x => x.IsIncome, x => x.Random.Bool())
                    .RuleFor(x => x.Amount, x => x.Finance.Amount(1, 2048))
                    .RuleFor(x => x.Date, x => x.Date.Between(DateTime.Now.AddMonths(-1), DateTime.Now.AddMonths(1)))
                    .RuleFor(x => x.UserId, x => new Guid("F2DCE61F-828B-4310-0FD0-08D949626D84"))
                    .RuleFor(x => x.Description, x => x.Commerce.ProductDescription());

                var expenses = transactionFaker.GenerateLazy(count);

                foreach (var expense in expenses)
                {
                    expense.Category.Description = expense.Category.Description.Length > 128
                        ? expense.Category.Description.Substring(0, 128)
                        : expense.Category.Description;

                    expense.Description = expense.Description.Length > 128
                        ? expense.Description.Substring(0, 128)
                        : expense.Description;

                    await categoryRepository.AddAsync(expense.Category);
                    await transactionRepository.AddAsync(expense);
                }
            }

            #endregion
        }
    }
}