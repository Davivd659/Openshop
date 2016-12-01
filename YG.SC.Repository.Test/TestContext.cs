
using System.Data.Common;
using YG.SC.DataAccess;

namespace YG.SC.Repository.Test
{
    using System.Data.Entity;

    public class TestContext : DbContext
    {
        public TestContext()
            : base("Name=FoodMaterialAllianceEntities")
        {
        }
        public TestContext(DbConnection connection)
            : base(connection, true)
        {
            Configuration.LazyLoadingEnabled = false;
        }
    }
}
