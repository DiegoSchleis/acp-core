using System.ComponentModel.DataAnnotations;

namespace acp_core.Models
{
    public class PrivacyOptions
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public bool PublicActivity { get; set; } = true;
        public bool PrivateProfile { get; set; } = false;
        public bool PendingFriendrequests { get; set; } = false;
    }
}
