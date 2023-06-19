namespace ABCLearn.Models
{
    public class UserMeetingRoom
    {
        public string UserId { get; set; }
        public string RoomId { get; set; }
    }

    public static class ListUserMeetingRoom
    {
        public static Dictionary<string, UserMeetingRoom> UserMeetingRooms { get; } = new Dictionary<string, UserMeetingRoom>();
    }
}
