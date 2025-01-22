using Microsoft.EntityFrameworkCore;
using MVC.Core.Dtos;
using MVC.Core.Entities;

namespace MVC.Core.Context;

public partial class OutagesDbContext : DbContext
{
    public OutagesDbContext()
    {
    }

    public OutagesDbContext(DbContextOptions<OutagesDbContext> options)
        : base(options)
    {
    }


    public virtual DbSet<Channel> Channels { get; set; }
    public virtual DbSet<ResultDto> ResultDtos { get; set; }
    public virtual DbSet<NetworkElementDto> NetworkElementDtos { get; set; }
    public virtual DbSet<AffectedCustomersDto> AffectedCustomersDtos { get; set; }
    public virtual DbSet<CuttingsForAddDto> CuttingsForAddDtos { get; set; }

    public virtual DbSet<CuttingDownDetail> CuttingDownDetails { get; set; }

    public virtual DbSet<CuttingDownHeader> CuttingDownHeaders { get; set; }

    public virtual DbSet<CuttingDownIgnored> CuttingDownIgnoreds { get; set; }

    public virtual DbSet<NetworkElement> NetworkElements { get; set; }

    public virtual DbSet<NetworkElementHierarchyPath> NetworkElementHierarchyPaths { get; set; }

    public virtual DbSet<NetworkElementType> NetworkElementTypes { get; set; }
    
    public virtual DbSet<User> Users { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https: //go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=.;DataBase=we_interview_exam;Integrated Security=true;Encrypt=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        

        modelBuilder.Entity<Channel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__channel__3213E83F742E29AA");

            entity.ToTable("Channel", "fta");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<ResultDto>().HasNoKey();
        modelBuilder.Entity<CuttingsForAddDto>().HasNoKey();
        modelBuilder.Entity<AffectedCustomersDto>().HasNoKey();
        modelBuilder.Entity<NetworkElementDto>().HasNoKey();

        modelBuilder.Entity<CuttingDownDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cutting___3214EC27920A4AF8");

            entity.ToTable("Cutting_Down_Detail", "fta");

            entity.HasIndex(e => e.CuttingDownHeaderId, "IX_Cutting_Down_Header_ID");

            entity.HasIndex(e => e.NetworkElementId, "IX_Network_Element_ID");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ActualCreateDate).HasColumnName("Actual_Create_Date");
            entity.Property(e => e.ActualEndDate).HasColumnName("Actual_End_Date");
            entity.Property(e => e.CuttingDownHeaderId).HasColumnName("Cutting_Down_Header_ID");
            entity.Property(e => e.ImpactedCustomers).HasColumnName("Impacted_Customers");
            entity.Property(e => e.NetworkElementId).HasColumnName("Network_Element_ID");

            entity.HasOne(d => d.CuttingDownHeader).WithMany(p => p.CuttingDownDetails)
                .HasForeignKey(d => d.CuttingDownHeaderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cutting_Down_Detail_Cutting_Down_Header");

            entity.HasOne(d => d.NetworkElement).WithMany(p => p.CuttingDownDetails)
                .HasForeignKey(d => d.NetworkElementId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cutting_Down_Detail_Network_Element");
        });

        modelBuilder.Entity<CuttingDownHeader>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cutting___3214EC272ED484D6");

            entity.ToTable("Cutting_Down_Header", "fta");

            entity.HasIndex(e => e.ChannelId, "IX_Channel_ID");

            entity.HasIndex(e => e.CreatedSystemUserId, "IX_Created_System_User_ID");

            entity.HasIndex(e => e.CuttingDownIncidentId, "IX_Cutting_Down_Incident_ID");

            entity.HasIndex(e => e.CuttingDownProblemId, "IX_Cutting_Down_Problem_ID");

            entity.HasIndex(e => e.UpdatedSystemUserId, "IX_Updated_System_User_ID");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ActualCreateDate).HasColumnName("Actual_Create_Date");
            entity.Property(e => e.ActualEndDate).HasColumnName("Actual_End_Date");
            entity.Property(e => e.ChannelId).HasColumnName("Channel_ID");
            entity.Property(e => e.CreatedSystemUserId).HasColumnName("Created_System_User_ID");
            entity.Property(e => e.CuttingDownIncidentId).HasColumnName("Cutting_Down_Incident_ID");
            entity.Property(e => e.CuttingDownProblemId).HasColumnName("Cutting_Down_Problem_ID");
            entity.Property(e => e.IsActive).HasColumnName("Is_Active");
            entity.Property(e => e.IsGlobal).HasColumnName("Is_Global");
            entity.Property(e => e.IsPlanned).HasColumnName("Is_Planned");
            entity.Property(e => e.PlannedEndDatetime)
                .HasColumnName("Planned_End_Datetime");
            entity.Property(e => e.PlannedStartDateyime)
                .HasColumnName("Planned_Start_Dateyime");
            entity.Property(e => e.SynchCreateDate).HasColumnName("Synch_Create_Date");
            entity.Property(e => e.SynchUpdateDate).HasColumnName("Synch_Update_Date");
            entity.Property(e => e.UpdatedSystemUserId).HasColumnName("Updated_System_User_ID");

            entity.HasOne(d => d.Channel).WithMany(p => p.CuttingDownHeaders)
                .HasForeignKey(d => d.ChannelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cutting_Down_Header_Channel");

            entity.HasOne(d => d.CreatedSystemUser).WithMany(p => p.CuttingDownHeaderCreatedSystemUsers)
                .HasForeignKey(d => d.CreatedSystemUserId)
                .HasConstraintName("FK_Cutting_Down_Header_User3");

            entity.HasOne(d => d.UpdatedSystemUser).WithMany(p => p.CuttingDownHeaderUpdatedSystemUsers)
                .HasForeignKey(d => d.UpdatedSystemUserId)
                .HasConstraintName("FK_Cutting_Down_Header_User2");
        });

        modelBuilder.Entity<CuttingDownIgnored>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cutting___3214EC27853328E0");

            entity.ToTable("Cutting_Down_Ignored", "fta");

            entity.HasIndex(e => e.CreatedUserId, "IX_Created_User_Id");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ActualCreateDate).HasColumnName("Actual_Create_Date");
            entity.Property(e => e.CabinName)
                .HasMaxLength(100)
                .HasColumnName("Cabin_Name");
            entity.Property(e => e.CableName)
                .HasMaxLength(100)
                .HasColumnName("Cable_Name");
            entity.Property(e => e.CreatedUserId).HasColumnName("Created_User_Id");
            entity.Property(e => e.SynchDate).HasColumnName("Synch_Date");

            entity.HasOne(d => d.CreatedUser).WithMany(p => p.CuttingDownIgnoreds)
                .HasForeignKey(d => d.CreatedUserId)
                .HasConstraintName("FK_Cutting_Down_Ignored_User");
        });

        modelBuilder.Entity<NetworkElement>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Network___3214EC27114E61DB");

            entity.ToTable("Network_Element", "fta");

            entity.HasIndex(e => e.NetworkElementTypeId, "IX_Network_Element_Type_ID");

            entity.HasIndex(e => e.ParentNetworkElementId, "IX_Parent_Network_Element_ID");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.NetworkElementTypeId).HasColumnName("Network_Element_Type_ID");
            entity.Property(e => e.ParentNetworkElementId).HasColumnName("Parent_Network_Element_ID");

            entity.HasOne(d => d.NetworkElementType).WithMany(p => p.NetworkElements)
                .HasForeignKey(d => d.NetworkElementTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Network_Element_Network_Element_Type");

            entity.HasOne(d => d.ParentNetworkElement).WithMany(p => p.InverseParentNetworkElement)
                .HasForeignKey(d => d.ParentNetworkElementId)
                .HasConstraintName("FK_Network_Element_Network_Element");
        });

        modelBuilder.Entity<NetworkElementHierarchyPath>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Network___3214EC279E35EDF0");

            entity.ToTable("Network_Element_Hierarchy_Path", "fta");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Abbreviation)
                .IsRequired()
                .HasMaxLength(200);
            entity.Property(e => e.Name).IsRequired();
        });

        modelBuilder.Entity<NetworkElementType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__network___3214EC2714360033");

            entity.ToTable("Network_Element_Type", "fta");

            entity.HasIndex(e => e.NetworkElementHierarchyPathId, "IX_Network_Element_Hierarchy_Path_ID");

            entity.HasIndex(e => e.ParentNetworkElementId, "IX_Parent_Network_Element_ID");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.NetworkElementHierarchyPathId).HasColumnName("Network_Element_Hierarchy_Path_ID");
            entity.Property(e => e.ParentNetworkElementId).HasColumnName("Parent_Network_Element_ID");

            entity.HasOne(d => d.NetworkElementHierarchyPath).WithMany(p => p.NetworkElementTypes)
                .HasForeignKey(d => d.NetworkElementHierarchyPathId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Network_Element_Type_Network_Element_Hierarchy_Path");

            entity.HasOne(d => d.ParentNetworkElement).WithMany(p => p.InverseParentNetworkElement)
                .HasForeignKey(d => d.ParentNetworkElementId)
                .HasConstraintName("FK_Network_Element_Type_Network_Element_Type");
        });


        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__user__3213E83FC7BB03C9");

            entity.ToTable("User", "fta");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.HashedPassword)
                .IsRequired()
                .HasColumnName("Hashed_Password");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);
        });
        modelBuilder.Entity<FtaProblemType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Problem___3214EC27FCB5161C");

            entity.ToTable("Problem_Type", "fta");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).IsRequired();
        });
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}