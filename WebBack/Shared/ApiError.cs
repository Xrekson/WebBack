namespace WebBack.Data
{
    public class ApiError : Exception
    {
        public ApiError(string name, string msg)
        {
            this.name = name;
            this.msg = msg;
        }
        public string name { get; set; } 
        public string msg { get; set; } 
    }
}
