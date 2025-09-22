using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamsApp.Shared
{
    [Table("useracknowledgement", Schema = "public")]
    public class AcknowledgementDto
    {
        [Key]
        [Column("email")]
        [Required]
        [MaxLength(254)]
        public string Email { get; set; }

        [Column("is_acknowledged")]
        public bool IsAcknowledged { get; set; } = false;

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
