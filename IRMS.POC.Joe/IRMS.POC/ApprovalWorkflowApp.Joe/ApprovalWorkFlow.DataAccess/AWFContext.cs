using ApprovalWorkFlow.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApprovalWorkFlow.DataAccess
{
    public class AWFContext : DbContext
    {
        public AWFContext()
            : base("name=IDSConn")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
       // public DbSet<Role> Roles { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Audit> Audits { get; set; }
        public DbSet<RequestStatus> RequestStatus { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<ITAdmin> ITAdmins { get; set; }

    }
}
