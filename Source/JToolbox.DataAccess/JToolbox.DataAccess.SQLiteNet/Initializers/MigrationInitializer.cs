using JToolbox.DataAccess.SQLiteNet.Entities;
using JToolbox.DataAccess.SQLiteNet.Migrations;
using JToolbox.DataAccess.SQLiteNet.Repositories;
using SQLite;
using System;
using System.Collections.Generic;

namespace JToolbox.DataAccess.SQLiteNet.Initializers
{
    public class MigrationInitializer
    {
        protected virtual List<BaseMigration> Migrations { get; }

        public void Run(SQLiteConnection db)
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
    }
}