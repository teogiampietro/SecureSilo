using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SecureSilo.Server.Data;

namespace SecureSilo.Tests
{
    public class MainTests
    {

        protected ApplicationDbContext ConstruirContext(string nombreDB) 
        {
            var opciones = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(nombreDB).Options;

            OperationalStoreOptions storeOptions = new OperationalStoreOptions { };

            IOptions<OperationalStoreOptions> operationalStoreOptions = Options.Create(storeOptions);

            var dbContext = new ApplicationDbContext(opciones, operationalStoreOptions);
            return dbContext;

        }
    }
}
