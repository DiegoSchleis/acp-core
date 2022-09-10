using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace acp_core.Models
{
    public class FriendShip
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public Guid Id { get; set; }
        [ForeignKey("followed_athlete_id")]
        public Guid FollowedAthleteId { get; set; }
        [ForeignKey("following_athlete_id")]
        public Guid FollowingAthleteId { get; set; }
        public FriendShipStatus FriendShipStatus { get; set; }
    }

    public enum FriendShipStatus
    {
        Pending, Accepted, Ended
    }
}
