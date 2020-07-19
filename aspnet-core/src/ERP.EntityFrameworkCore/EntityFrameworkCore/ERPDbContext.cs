using Abp.IdentityServer4;
using Abp.Zero.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ERP.Authorization.Roles;
using ERP.Authorization.Users;
using ERP.Chat;
using ERP.Editions;
using ERP.Friendships;
using ERP.MultiTenancy;
using ERP.MultiTenancy.Accounting;
using ERP.MultiTenancy.Payments;
using ERP.Storage;
using ERP.Models;

namespace ERP.EntityFrameworkCore
{
    public class ERPDbContext : AbpZeroDbContext<Tenant, Role, User, ERPDbContext>, IAbpPersistedGrantDbContext
    {
        /* Define an IDbSet for each entity of the application */

        public virtual DbSet<BinaryObject> BinaryObjects { get; set; }

        public virtual DbSet<Friendship> Friendships { get; set; }

        public virtual DbSet<ChatMessage> ChatMessages { get; set; }

        public virtual DbSet<SubscribableEdition> SubscribableEditions { get; set; }

        public virtual DbSet<SubscriptionPayment> SubscriptionPayments { get; set; }

        public virtual DbSet<Invoice> Invoices { get; set; }

        public virtual DbSet<PersistedGrantEntity> PersistedGrants { get; set; }

        // add vào db
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<Issue> Issues { get; set; }

        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<HistoryStatusIssue> HistoryStatusIssues { get; set; }

        //public virtual DbSet<RoleProject> RoleProject { get; set; }

        //public virtual DbSet<IssueType> IssueType { get; set; }
        //public virtual DbSet<Status> Status { get; set; }
        
        //public virtual DbSet<Priority> Priority { get; set; }
        //public virtual DbSet<ConfigView> ConfigView { get; set; }
        public virtual DbSet<Sprint> Sprint { get; set; }

        

        public ERPDbContext(DbContextOptions<ERPDbContext> options)
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BinaryObject>(b =>
            {
                b.HasIndex(e => new { e.TenantId });
            });

            modelBuilder.Entity<ChatMessage>(b =>
            {
                b.HasIndex(e => new { e.TenantId, e.UserId, e.ReadState });
                b.HasIndex(e => new { e.TenantId, e.TargetUserId, e.ReadState });
                b.HasIndex(e => new { e.TargetTenantId, e.TargetUserId, e.ReadState });
                b.HasIndex(e => new { e.TargetTenantId, e.UserId, e.ReadState });
            });

            modelBuilder.Entity<Friendship>(b =>
            {
                b.HasIndex(e => new { e.TenantId, e.UserId });
                b.HasIndex(e => new { e.TenantId, e.FriendUserId });
                b.HasIndex(e => new { e.FriendTenantId, e.UserId });
                b.HasIndex(e => new { e.FriendTenantId, e.FriendUserId });
            });

            modelBuilder.Entity<Tenant>(b =>
            {
                b.HasIndex(e => new { e.SubscriptionEndDateUtc });
                b.HasIndex(e => new { e.CreationTime });
            });

            modelBuilder.Entity<SubscriptionPayment>(b =>
            {
                b.HasIndex(e => new { e.Status, e.CreationTime });
                b.HasIndex(e => new { PaymentId = e.ExternalPaymentId, e.Gateway });
            });

            modelBuilder.ConfigurePersistedGrantEntity();
        }
    }
}
