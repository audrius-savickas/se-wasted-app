using Microsoft.EntityFrameworkCore;
using Persistence;
using Services.Repositories;
using System;
using Xunit;

namespace Services.Tests
{
    public class FoodEFRepositoryUnitTests
    {
        private readonly DatabaseContext context;

        public FoodEFRepositoryUnitTests()
        {
            DbContextOptionsBuilder dbOptions = new DbContextOptionsBuilder()
                .UseInMemoryDatabase(
                                Guid.NewGuid().ToString()
                );

            context = new DatabaseContext((DbContextOptions<DatabaseContext>)dbOptions.Options);   
        }


        [Fact]
        public void Add()
        {

        }
    }
}
