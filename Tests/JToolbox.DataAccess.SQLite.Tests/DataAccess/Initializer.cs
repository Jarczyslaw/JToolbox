using JToolbox.DataAccess.SQLiteNet;
using JToolbox.DataAccess.SQLiteNet.Migrations;
using System.Reflection;

namespace JToolbox.DataAccess.SQLite.Tests.DataAccess
{
    internal class Initializer : BaseInitializer
    {
        protected override Assembly EntitiesAssembly => GetType().Assembly;

        protected override List<BaseMigration> Migrations => [new Migration000_Init()];
    }
}