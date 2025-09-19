using System;
using System.ComponentModel.DataAnnotations;

namespace TeamsApp.Shared
{
    public class AcknowledgementDto
    {
        [Key]
        public string Email { get; set; }

        public bool Is_Acknowledged { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime? Updated_At { get; set; }
    }
}
