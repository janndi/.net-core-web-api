namespace Domain.ResultTypes
{
    public class BooleanResult
    {
        public bool Result { get; set; }
        public string Message { get; set; }

        public BooleanResult()
        {
        }

        public BooleanResult(string message, bool result)
        {
            Result = result;
            Message = message;
        }
    }
}
