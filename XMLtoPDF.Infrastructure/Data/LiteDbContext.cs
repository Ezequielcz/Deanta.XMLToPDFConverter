using LiteDB;
using Microsoft.Extensions.Options;
using SE.GMO.Billing.Application.Options;

namespace XMLtoPDF.Infrastructure.Persistence
{
    using LiteDB;
    using Microsoft.Extensions.Configuration;

    namespace XMLtoPDF.Infrastructure.Data
    {
        public class LiteDbContext
        {
            private readonly LiteDatabase _db;

            public LiteDbContext(IConfiguration configuration)
            {
                var dbPath = configuration["LiteDb:DatabasePath"] ?? "data/XMLtoPDF.db";
                _db = new LiteDatabase(dbPath);
            }

            public LiteDatabase Database => _db;
        }
    }


}
