using acp_core.Util;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Identity;

namespace acp_core.Models
{
    [FirestoreData]
    public class Athlete : IdentityUser, IFirebaseEntity
    {
        [FirestoreProperty]
        new public string Id { get; set; }
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
    }
}
