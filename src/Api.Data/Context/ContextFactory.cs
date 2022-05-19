using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace Api.Data.Context
{
    public class ContextFactory : IDesignTimeDbContextFactory<MyContext>
    {
        public MyContext CreateDbContext(string[] args)
        {
            //Usado para Criar as Migrações
            var connectionString = "Server=localhost;Port=3306;DataBase=dbApi;Uid=root;Pwd=root";
            //var connectionString = "Server=(LocalDb)\\MSSQLLocalDB;Initial Catalog=dbApi;MultipleActiveResultSets=true;User ID=sa;Password=#Fmc040878";
            var optionsBuilder = new DbContextOptionsBuilder<MyContext>();
            optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 21)),
                mysqlOptions => mysqlOptions.CharSetBehavior(CharSetBehavior.NeverAppend)
            );
            //optionsBuilder.UseSqlServer(connectionString);
            return new MyContext(optionsBuilder.Options);
        }
    }
}
