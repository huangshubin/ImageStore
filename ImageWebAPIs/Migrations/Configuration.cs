namespace ImageWebAPIs.Migrations
{
    using FizzWare.NBuilder;
    using Infrastructure;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Model;
    using System.Data.Entity.SqlServer;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Infrastructure.AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            SetSqlGenerator("System.Data.SqlClient", new CustomSqlServerMigrationSqlGenerator());

        }

        protected override void Seed(Infrastructure.AppDbContext context)
        {



            var users = Builder<Client>.CreateListOfSize(100).All()
                .With(c => c.LastName = Faker.Name.Last())
                .With(c => c.FirstName = Faker.Name.First())
                .With(c => c.City = Faker.Address.City())
                .With(c => c.Country = Faker.Address.Country())
                .With(c => c.DateRegistered = DateTime.Now)
                .With(c => c.Password = Encrypter.Encrypt("111111"))
                .With(c => c.Phone = Faker.Phone.Number())
                .With(c => c.State = Faker.Address.UsState())
                .With(c => c.Street = Faker.Address.StreetAddress())
                .With(c => c.UserName = Faker.Name.First())
                .Build();
            context.Clients.AddIfNoExist(c => c.ID, users.ToArray());


            context.SaveChanges();

        }
    }



    internal class CustomSqlServerMigrationSqlGenerator : SqlServerMigrationSqlGenerator
    {
        protected override void Generate(AddColumnOperation addColumnOperation)
        {
            SeCustomColumn(addColumnOperation.Column);

            base.Generate(addColumnOperation);
        }

        protected override void Generate(CreateTableOperation createTableOperation)
        {
            SetCustomColumns(createTableOperation.Columns);

            base.Generate(createTableOperation);
        }

        private static void SetCustomColumns(IEnumerable<ColumnModel> columns)
        {
            foreach (var columnModel in columns)
            {
                SeCustomColumn(columnModel);
            }
        }

        private static void SeCustomColumn(PropertyModel column)
        {
            if (column.Name == "CreatedOn")
            {
                column.DefaultValueSql = "GETUTCDATE()";

            }
        }
    }
}