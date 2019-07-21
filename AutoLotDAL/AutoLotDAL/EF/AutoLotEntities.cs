namespace AutoLotDAL.EF
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Core.Objects;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using AutoLotDAL.Models;
    using AutoLotDAL.Interception;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Infrastructure.Interception;

    public partial class AutoLotEntities : DbContext
    {
        static readonly DatabaseLogger databaseLogger = new DatabaseLogger("sqllog.txt", true);
        public AutoLotEntities()
            : base("name=AutoLotConnection")
        {
            var context = (this as IObjectContextAdapter).ObjectContext;
            context.ObjectMaterialized += OnObjectMaterialized;
            context.SavingChanges += OnSavingChanges;
        }

        public virtual DbSet<CreditRisk> CreditRisks { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Inventory> Cars { get; set; }
        public virtual DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Inventory>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Car)
                .WillCascadeOnDelete(false);
        }

        private void OnSavingChanges(object sender, EventArgs eventArgs)
        {
            var context = sender as ObjectContext;
            if (context == null) return;

            foreach(ObjectStateEntry item in context.ObjectStateManager.GetObjectStateEntries(EntityState.Modified | EntityState.Added)
            {
                if ((item.Entity as Inventory) != null)
                {
                    var entity = (Inventory)item.Entity;
                    if (entity.Color == "Red")
                    {
                        item.RejectPropertyChanges(nameof(entity.Color));
                    }
                }
            }
        }

        private void OnObjectMaterialized(object sender, System.Data.Entity.Core.Objects.ObjectMaterializedEventArgs eventArgs)
        {

        }

        protected override void Dispose(bool disposing)
        {
            DbInterception.Remove(databaseLogger);
            databaseLogger.StopLogging();
            base.Dispose(disposing);
        }
    }
}
