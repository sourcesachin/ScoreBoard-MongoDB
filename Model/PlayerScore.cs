using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LeaderboardMicroServices.Model
{
    public class PlayerScoreViewModel
    {
        public IList<PlayerScore> stats;
        public IList<PlayerScore> PlayersStats;
    }
    public class PlayerScore
    {
        public PlayerScore()
        { }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        [Required]
        [BsonElement("UserName")]
        [StringLength(100, ErrorMessage = "Name length can't be more than 100.")]
        public String UserName { get; set; }
        
        [Required]
        [BsonElement("MatchName")]
        [StringLength(100, ErrorMessage = "Match length can't be more than 100.")]
        public String MatchName { get; set; }

        [BsonElement("Rank")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Must be numeric.")]
        public String Rank { get; set; }
        
        [BsonElement("Kills")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Must be numeric.")]
        public String Kills { get; set; }

        [BsonElement("Score")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Must be numeric.")]
        public String Score { get; set; }

        [BsonElement("EnteryDate")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime EnteryDate { get; set; } = DateTime.Now;
    }
}
