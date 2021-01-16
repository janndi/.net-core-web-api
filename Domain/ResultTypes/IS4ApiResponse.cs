namespace Domain.ResultTypes
{
    public class IS4ApiResponse
    {
        public string Access_Token { get; set; }
        public string Refresh_Token { get; set; }
        public int Expires_In { get; set; }
        public string Error { get; set; }
        public string Error_Description { get; set; }
    }

    public class IS4SuccessResponse
    {
        public string Access_Token { get; set; }
        public string Refresh_Token { get; set; }
        public int Expires_In { get; set; }
    }

    public class IS4ErrorResponse
    {
        public string Error { get; set; }
        public string Error_Description { get; set; }
    }
}
