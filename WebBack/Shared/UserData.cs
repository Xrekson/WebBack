namespace WebBack.Data
{
    public class UserData
    {
        public int id { get; set; }
        public string name { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public string mobile { get; set; } = string.Empty;

    }
        public record UserRecord(int Id, string Name, string Email,string Mobile="");
        public record UserRecordNull();
}
