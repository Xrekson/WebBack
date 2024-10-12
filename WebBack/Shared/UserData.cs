namespace WebBack.Data
{
    public record UserData
    {
        public UserData(int id, string name, string email, string mobile)
        {
            this.id = id;
            this.name = name;
            this.email = email;
            this.mobile = mobile;
            this.password = "";
        }

        public int id { get; set; }
        public string name { get; set; } 
        public string email { get; set; } 
        public string mobile { get; set; } 
        public string password { get; set; } 

    }
}
