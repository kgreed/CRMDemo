using System;
using DevExpress.ExpressApp.EFCore.Updating;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using DevExpress.Persistent.BaseImpl.EF.PermissionPolicy;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.ExpressApp.Design;
using DevExpress.ExpressApp.EFCore.DesignTime;
using DevExpress.Persistent.Base;
using System.ComponentModel.DataAnnotations;

namespace CRM.Module.BusinessObjects {
	[NavigationItem("CRM")]
	public class Contact :Person
	{ 
		[VisibleInDetailView(true)]
		[VisibleInListView(false)]
		public string Hobbies { get; set; }
		public virtual Drink DrinkPreference { get; set; }
	}
	[NavigationItem("CRM")]
	public class Drink { 
		[Key] public int Id { get; set; }
		public string DrinkName { get; set; }
	}
    // This code allows our Model Editor to get relevant EF Core metadata at design time.
    // For details, please refer to https://supportcenter.devexpress.com/ticket/details/t933891.
	public class CRMContextInitializer : DbContextTypesInfoInitializerBase {
		protected override DbContext CreateDbContext() {
			var optionsBuilder = new DbContextOptionsBuilder<CRMEFCoreDbContext>()
                .UseSqlServer(@";");
            return new CRMEFCoreDbContext(optionsBuilder.Options);
		}
	}
	//This factory creates DbContext for design-time services. For example, it is required for database migration.
	public class CRMDesignTimeDbContextFactory : IDesignTimeDbContextFactory<CRMEFCoreDbContext> {
		public CRMEFCoreDbContext CreateDbContext(string[] args) {
			throw new InvalidOperationException("Make sure that the database connection string and connection provider are correct. After that, uncomment the code below and remove this exception.");
			//var optionsBuilder = new DbContextOptionsBuilder<CRMEFCoreDbContext>();
			//optionsBuilder.UseSqlServer(@"Integrated Security=SSPI;Pooling=false;Data Source=(localdb)\\mssqllocaldb;Initial Catalog=CRM");
			//return new CRMEFCoreDbContext(optionsBuilder.Options);
		}
	}
	[TypesInfoInitializer(typeof(CRMContextInitializer))]
	public class CRMEFCoreDbContext : DbContext {
		public CRMEFCoreDbContext(DbContextOptions<CRMEFCoreDbContext> options) : base(options) {
		}
		public DbSet<Drink> Drinks { get; set; }
		public DbSet<Contact>  Contacts { get; set; }
		public DbSet<ModuleInfo> ModulesInfo { get; set; }
		public DbSet<ModelDifference> ModelDifferences { get; set; }
		public DbSet<ModelDifferenceAspect> ModelDifferenceAspects { get; set; }
	    public DbSet<PermissionPolicyRole> Roles { get; set; }
	    public DbSet<CRM.Module.BusinessObjects.ApplicationUser> Users { get; set; }
        public DbSet<CRM.Module.BusinessObjects.ApplicationUserLoginInfo> UserLoginInfos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<ApplicationUserLoginInfo>(b => {
                b.HasIndex(nameof(DevExpress.ExpressApp.Security.ISecurityUserLoginInfo.LoginProviderName), nameof(DevExpress.ExpressApp.Security.ISecurityUserLoginInfo.ProviderUserKey)).IsUnique();
            });
			modelBuilder.Entity<Contact>().HasOne(x => x.DrinkPreference);
        }
	}
}
