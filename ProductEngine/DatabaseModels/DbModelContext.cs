namespace DatabaseModels
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DbModelContext : DbContext
    {
        public DbModelContext()
            : base("name=DbModelContext")
        {
        }

        public virtual DbSet<AparitieProdus> AparitieProdus { get; set; }
        public virtual DbSet<EvolutiaPretului> EvolutiaPretului { get; set; }
        public virtual DbSet<IstoricCautari> IstoricCautari { get; set; }
        public virtual DbSet<Produs> Produs { get; set; }
        public virtual DbSet<UrmarireProdus> UrmarireProdus { get; set; }
        public virtual DbSet<Utilizator> Utilizator { get; set; }
        public virtual DbSet<Vanzator> Vanzator { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produs>()
                .HasMany(e => e.AparitieProdus)
                .WithOptional(e => e.Produs)
                .HasForeignKey(e => e.Id_Produs);

            modelBuilder.Entity<Produs>()
                .HasMany(e => e.EvolutiaPretului)
                .WithOptional(e => e.Produs)
                .HasForeignKey(e => e.Id_Produs)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Produs>()
                .HasMany(e => e.UrmarireProdus)
                .WithRequired(e => e.Produs)
                .HasForeignKey(e => e.Id_Produs)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Utilizator>()
                .HasMany(e => e.UrmarireProdus)
                .WithRequired(e => e.Utilizator)
                .HasForeignKey(e => e.Id_Utilizator)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Vanzator>()
                .HasMany(e => e.Produs)
                .WithOptional(e => e.Vanzator)
                .HasForeignKey(e => e.Id_Vanzator);
        }
    }
}
