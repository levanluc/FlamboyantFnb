using Amazon.DynamoDBv2.DataModel;
using FlamboyantFnb.Domain.RequestModel;

namespace FlamboyantFnb.Domain.Entities
{
    [DynamoDBTable("Rooms")]
    public class Room : BaseEntity
    {
        public string RoomName { get; set; }
        public MessageRequest LatestMessage { get; set; }
        public bool IsGroup { get; set; }
    }
}
