using System;
using Microsoft.EntityFrameworkCore;

namespace XUnitTestProject.Common
{
    public static class DatabaseContextMock<T> where T : DbContext
    {
        public static DbContextOptions<T> InMemoryDatabase() 
        {
            DbContextOptions<T> options = new DbContextOptionsBuilder<T>()
                  .UseInMemoryDatabase(Guid.NewGuid().ToString())
                  .Options;

            return options;
        }
    }
}
