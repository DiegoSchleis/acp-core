using acp_core.Util;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace acp_core.Models
{
    [FirestoreData]
    public class Athlete : IdentityUser, IFirebaseEntity
    {
        [FirestoreProperty]
        override public string Id { get; set; }
        public bool InitialLogin { get; set; } = true;
        [FirestoreProperty]
        [PersonalData]
        public string? FirstName { get; set; }
        [FirestoreProperty]
        [PersonalData]
        public string? LastName { get; set; }
        [FirestoreProperty]
        public string? Description { get; set; }
        [FirestoreProperty]
        public string? Nationality { get; set; }
        [FirestoreProperty]
        [PersonalData]
        public string? Gender { get; set; }
        [FirestoreProperty]
        [PersonalData]
        public decimal? Weight { get; set; }
        [FirestoreProperty]
        [PersonalData]
        public DateTime? BirthDate { get; set; }
        [FirestoreProperty]
        public int? MaximalHeartRate { get; set; }
        [FirestoreProperty]
        public int? FunctionalThresholdPower { get; set; }
        [FirestoreProperty]
        public byte[]? Avatar { get; set; }
        [ForeignKey("privacyoptions_athlete")]
        public PrivacyOptions PrivacyOptions { get; set; }
    }
}
