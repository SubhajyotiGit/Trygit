using ApprovalWorkFlow.DataAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApprovalWorkFlow.DataAccess
{
    public sealed class MigrationConfiguration : DbMigrationsConfiguration<AWFContext>
    {
        public MigrationConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }
        protected override void Seed(AWFContext context)
        {
            // context.Database.ExecuteSqlCommand("CREATE UNIQUE INDEX IX_User_EmailId ON Users (EmailId)");

            base.Seed(context);
        }
    }
}
