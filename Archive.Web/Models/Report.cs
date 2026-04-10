using System.ComponentModel.DataAnnotations;
using Archive.Web.Models.Enums;

namespace Archive.Web.Models;

public class Report
{
    public int Id { get; set; }

    public int ReporterUserId { get; set; }
    public AppUser? ReporterUser { get; set; }

    public int? TargetUserId { get; set; }
    public AppUser? TargetUser { get; set; }

    public int? TargetPostId { get; set; }
    public Post? TargetPost { get; set; }

    [Required, StringLength(100)]
    public string Reason { get; set; } = string.Empty;

    [StringLength(260)]
    public string? Details { get; set; }

    public ReportStatus Status { get; set; } = ReportStatus.Pending;

    public int? ReviewedByUserId { get; set; }
    public AppUser? ReviewedByUser { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? ReviewedAt { get; set; }
}
