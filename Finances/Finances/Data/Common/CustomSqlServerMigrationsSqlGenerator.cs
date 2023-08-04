using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Update;

namespace Finances.Data.Common
{
    internal class CustomSqlServerMigrationsSqlGenerator : SqlServerMigrationsSqlGenerator
    {
        internal const string DatabaseCollationName = "Cyrillic_General_CS_AI";

        public CustomSqlServerMigrationsSqlGenerator(
            MigrationsSqlGeneratorDependencies dependencies,
            ICommandBatchPreparer migrationsAnnotations)
        : base(dependencies, migrationsAnnotations)
        {
        }

        protected override void Generate(SqlServerCreateDatabaseOperation operation, IModel model, MigrationCommandListBuilder builder)
        {
            base.Generate(operation, model, builder);

            if (DatabaseCollationName != null)
            {
                builder
                    .Append("ALTER DATABASE ")
                    .Append(Dependencies.SqlGenerationHelper.DelimitIdentifier(operation.Name))
                    .Append(" COLLATE ")
                    .Append(DatabaseCollationName)
                    .AppendLine(Dependencies.SqlGenerationHelper.StatementTerminator)
                    .EndCommand(suppressTransaction: true);
            }
        }
    }
}
