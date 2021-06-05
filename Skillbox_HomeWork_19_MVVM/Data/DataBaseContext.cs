using System.Data.Entity;

namespace Skillbox_HomeWork_19_MVVM.Data
{
    class DataBaseContext : DbContext
    {
        public DataBaseContext() : base("DbConnection") { }

        public DbSet<Client> Client { get; set; }
        public DbSet<NaturalPerson> NaturalPerson { get; set; }
        public DbSet<Organization> Organization { get; set; }

    }
}
