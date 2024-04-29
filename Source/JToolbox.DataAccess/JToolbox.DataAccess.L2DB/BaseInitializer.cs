using JToolbox.Core.TimeProvider;
using JToolbox.DataAccess.L2DB.Entities;
using JToolbox.DataAccess.L2DB.Migrations;
using JToolbox.DataAccess.L2DB.Repositories;
using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace JToolbox.DataAccess.L2DB
{
    public abstract class BaseInitializer
    {
        protected readonly ITimeProvider _timeProvider;

        protected BaseInitializer(ITimeProvider timeProvider)
        {
            _timeProvider = timeProvider;
        }

        public abstract DataOptions GetDataOptions { get; }

        protected abstract Assembly EntitiesAssembly { get; }

        protected abstract List<BaseMigration> Migrations { get; }

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

            var repository = new MigrationsRepository();
            var currentMigrations = repository.GetAll(db)
                .ConvertAll(s => s.Name);

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
                    db.Insert(new MigrationEntity
                    {
                        Name = migrationName,
                        ExecutionDate = _timeProvider.Now,
                        Version = migration.GetVersion().Value
                    });
                }
            }
        }
    }
}