using Microsoft.EntityFrameworkCore;
using PROG6212_PART2_ST10396724.Models;
/*
 * Author: Michael AMM.
 * ST10396724
 * ProgPOEPart3
 * Group 1
 */

namespace PROG6212_PART2_ST10396724.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public  DbSet<Lecturer> lecturer { get; set; }

        public  DbSet<Claim> claim { get; set; }

        public  DbSet<test> tests { get; set; }  

        public DbSet<AcademicManager> academicManagers { get; set; }

        public DbSet<ProgCoOrdinator> progs { get; set; }

        public DbSet<ClaimApproval> claimApproval { get; set; }

        public DbSet<Document> document { get; set; }   

        //public DbSet<ClaimStatusViewModel> claimStatusViewModel { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Claim>()
               .HasOne(c => c.lecturer)            // Claim has one Lecturer
               .WithMany(l => l.claims)            // Lecturer has many Claims
               .HasForeignKey(c => c.LecturerID);  // Foreign key in Claim is LecturerId

            modelBuilder.Entity<ClaimApproval>()
                .HasOne(ca => ca.progs)   // ClaimApproval has one ProgCoOrdinator
                .WithMany()                         // ProgCoOrdinator has many ClaimApprovals
                .HasForeignKey(ca => ca.ProgCoOrdinatorID) // Foreign key in ClaimApproval is ProgCoOrdinatorID
                .OnDelete(DeleteBehavior.Restrict); // Specify ON DELETE NO ACTION

            modelBuilder.Entity<ClaimApproval>()
                .HasOne(ca => ca.claim)             // ClaimApproval has one Claim
                .WithMany()                         // Claim has many ClaimApprovals
                .HasForeignKey(ca => ca.ClaimID)    // Foreign key in ClaimApproval is ClaimID
                .OnDelete(DeleteBehavior.Restrict); // Specify ON DELETE NO ACTION

            modelBuilder.Entity<ClaimApproval>()
                .HasOne(ca => ca.lecturer)          // ClaimApproval has one Lecturer
                .WithMany()                         // Lecturer has many ClaimApprovals
                .HasForeignKey(ca => ca.LecturerID) // Foreign key in ClaimApproval is LecturerID
                .OnDelete(DeleteBehavior.Restrict); // Specify ON DELETE NO ACTION

            modelBuilder.Entity<ClaimApproval>()
                .HasOne(ca => ca.AcademicManagers)  // ClaimApproval has one AcademicManager
                .WithMany()                         // AcademicManager has many ClaimApprovals
                .HasForeignKey(ca => ca.AcademicManagerID) // Foreign key in ClaimApproval is LecturerID
                .OnDelete(DeleteBehavior.Restrict); // Specify ON DELETE NO ACTION


            modelBuilder.Entity<Document>()
                .HasOne(d => d.lecturer)            // Document has one Lecturer
                .WithMany(l => l.documentsLecturer)         // Lecturer has many Documents
                .HasForeignKey(d => d.LecturerID)   // Foreign key in Document is LecturerID
                .OnDelete(DeleteBehavior.Restrict); // Specify ON DELETE NO ACTION

            modelBuilder.Entity<Document>()
                .HasOne(d => d.claim)               // Document has one Claim
                .WithMany(c => c.documentsClaim)         // Claim has many Documents
                .HasForeignKey(d => d.ClaimID)      // Foreign key in Document is ClaimID
                .OnDelete(DeleteBehavior.Restrict); // Specify ON DELETE NO ACTION





            base.OnModelCreating(modelBuilder);  // Foreign key in Claim is LecturerId
        }


    }
}
