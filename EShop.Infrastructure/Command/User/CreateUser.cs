using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Infrastructure.Command.User
{
    public class CreateUser
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? UserId { get; set; }
        public string UserName { get; set; }
        public string EmailId { get; set; }
        public string ContactNo { get; set; }
        public string Password { get; set; }
    }
}
