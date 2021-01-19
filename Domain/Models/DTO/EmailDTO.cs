namespace Domain.Models.DTO
{
    public class EmailDTO
    {
        public string Template { get; set; }
        public string Greetings { get; set; }
        public string Subject { get; set; }
        public string To { get; set; }
        public string From { get; set; }
        public string ReplyTo { get; set; }
        public string ReplyEmail { get; set; } = "";
        public string Content { get; set; }
        public string Username { get; set; }
        public string Fullname { get; set; }
    }
}
