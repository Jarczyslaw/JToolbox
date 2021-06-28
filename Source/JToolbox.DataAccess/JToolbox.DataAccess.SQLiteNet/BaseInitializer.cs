using JToolbox.DataAccess.SQLiteNet.Entities;
using JToolbox.DataAccess.SQLiteNet.Migrations;
using JToolbox.DataAccess.SQLiteNet.Repositories;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace JToolbox.DataAccess.SQLiteNet
{
    public abstract class BaseInitializer
    {
        protected virtual Assembly EntitiesAssembly { get; }

        protected abstract List<BaseMigration> Migrations { get; }

        public virtual void InitializeData(SQLiteConnection db)
        {
        }

        public void InitializeMigrations(SQLiteConnection db)
        {
            if (Migrations == null || Migrations.Count == 0)
            {
                throw new Exception("No migrations defined. Add initial migration to initialize database");
            }

            var repository = new MigrationsRepository();
            var currentMigrations = repository.GetAll(db)
                .ConvertAll(s => s.Name);

            if (currentMigrations.Count > Migrations.Count)
            {
                throw new Exception("Application is not compatible with the database version");
            }

            foreach (var migration in Migrations)
            {
                var migrationName = migration.GetType().Name;
                if (!currentMigrations.Contains(migrationName))
                {
                    migration.Up(db);
                    db.Insert(new MigrationEntity
                    {
                        Name = migrationName,
                        ExecutionDate = DateTime.Now
                    });
                }
            }
        }

        public virtual void InitializeTables(SQLiteConnection db)
        {
            var entityTypes = EntitiesAssembly
               .GetTypes()
               .Where(t => typeof(BaseEntity).IsAssignableFrom(t) && !t.IsAbstract);
            foreach (var entityType in entityTypes)
            {
                db.CreateTable(entityType);
            }
        }
    }
}