using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using MyMvc4App.Models.Mapping;

namespace MyMvc4App.Models
{
    public partial class AdventureWorksLT2008R2Context : DbContext
    {
        static AdventureWorksLT2008R2Context()
        {
            Database.SetInitializer<AdventureWorksLT2008R2Context>(null);
        }

        public AdventureWorksLT2008R2Context()
            : base("Name=AdventureWorksLT2008R2Context")
        {
        }

        public DbSet<BuildVersion> BuildVersions { get; set; }
        public DbSet<ErrorLog> ErrorLogs { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerAddress> CustomerAddresses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductDescription> ProductDescriptions { get; set; }
        public DbSet<ProductModel> ProductModels { get; set; }
        public DbSet<ProductModelProductDescription> ProductModelProductDescriptions { get; set; }
        public DbSet<SalesOrderDetail> SalesOrderDetails { get; set; }
        public DbSet<SalesOrderHeader> SalesOrderHeaders { get; set; }
        public DbSet<vGetAllCategory> vGetAllCategories { get; set; }
        public DbSet<vProductAndDescription> vProductAndDescriptions { get; set; }
        public DbSet<vProductModelCatalogDescription> vProductModelCatalogDescriptions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new BuildVersionMap());
            modelBuilder.Configurations.Add(new ErrorLogMap());
            modelBuilder.Configurations.Add(new AddressMap());
            modelBuilder.Configurations.Add(new CustomerMap());
            modelBuilder.Configurations.Add(new CustomerAddressMap());
            modelBuilder.Configurations.Add(new ProductMap());
            modelBuilder.Configurations.Add(new ProductCategoryMap());
            modelBuilder.Configurations.Add(new ProductDescriptionMap());
            modelBuilder.Configurations.Add(new ProductModelMap());
            modelBuilder.Configurations.Add(new ProductModelProductDescriptionMap());
            modelBuilder.Configurations.Add(new SalesOrderDetailMap());
            modelBuilder.Configurations.Add(new SalesOrderHeaderMap());
            modelBuilder.Configurations.Add(new vGetAllCategoryMap());
            modelBuilder.Configurations.Add(new vProductAndDescriptionMap());
            modelBuilder.Configurations.Add(new vProductModelCatalogDescriptionMap());
        }
    }
}
