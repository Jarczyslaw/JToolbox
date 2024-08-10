using JToolbox.Core.TimeProvider;
using JToolbox.DataAccess.L2DB.Abstraction;
using JToolbox.DataAccess.L2DB.Migrations;
using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JToolbox.DataAccess.L2DB
{
    public abstract class BaseInitializer<TMigration> : IBaseInitializer
        where TMigration : class, IMigrationEntity, new()
    {
        protected readonly ITimeProvider _timeProvider;

        protected BaseInitializer(ITimeProvider timeProvider)
        {
            _timeProvider = timeProvider;
        }

        public abstract DataOptions GetDataOptions { get; }

        public bool InitializeData { get; set; } = true;

        protected abstract List<BaseMigration> Migrations { get; }

        public virtual void CreateMigrationsTableIfNotExists(BaseDbContext db)
        {
            db.CreateTable<TMigration>(tableOptions: TableOptions.CheckExistence);
        }

        public int GetDbVersion(BaseDbContext db)
        {
            return db.GetTable<TMigration>().Max(x => x.Version);
        }

        public void InitializeMigrations(BaseDbContext db)
        {
            if (Migrations == null || Migrations.Count == 0)
            {
                throw new Exception("No migrations defined. Add initial migration to initialize database");
            }

            if (Migrations.Any(x => x.GetVersion() == null))
            {
                throw new Exception("At least one migration has invalid name. Migration should be named as 'Migration_{version}_{migrationName}");
            }

            var currentMigrations = GetCurrentMigrationsNames(db);

            if (currentMigrations.Count > Migrations.Count)
            {
                throw new Exception("Application is not compatible with the database version");
            }

            bool newDatabase = currentMigrations.Count == 0;

            foreach (var migration in Migrations)
            {
                var migrationName = migration.GetType().Name;
                if (!currentMigrations.Contains(migrationName))
                {
                    migration.Up(db, newDatabase);

                    if (InitializeData)
                    {
                        migration.InitializeData(db, newDatabase);
                    }

                    InsertNewMigration(db, migrationName, _timeProvider.Now, migration.GetVersion().Value);
                }
            }
        }

        protected virtual List<string> GetCurrentMigrationsNames(BaseDbContext db)
        {
            return db.GetTable<TMigration>().Select(x => x.Name).ToList();
        }

        protected virtual void InsertNewMigration(BaseDbContext db, string migrationName, DateTime executionDate, int version)
        {
            db.Insert(new TMigration()
            {
                ExecutionDate = executionDate,
                Name = migrationName,
                Version = version
            });
        }
    }
}