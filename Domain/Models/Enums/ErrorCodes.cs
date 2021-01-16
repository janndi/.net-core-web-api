using System.ComponentModel;

namespace Domain.Models.Enums
{
    public enum ErrorCodes
    {
        [Description("A problem occured while proccessing your request.")]
        GeneralException = 1111,
        [Description("A failed run time check.")]
        SystemException = 1000,
        [Description("Failure to access a type member.")]
        AccessException = 1001,
        [Description("An argument to a method was invalid.")]
        ArgumentException = 1010,
        [Description("A null argument was passed to a method that does not accept it.")]
        ArgumentNullException = 1100,
        [Description("Argument value is out of range.")]
        ArgumentOutOfRangeException = 1101,
        [Description("An invoked method is not supported.")]
        NotSupportedException = 1110,
        [Description("You are not authorized to connect.")]
        NotAuthorized = 11001,
        [Description("The format of an argument is invalid.")]
        FormatException = 1011,
        [Description("The time allotted for a process or operation has expired.")]
        TimeoutException = 111,
        [Description("The key specified for accessing an element in a collection does not match any key in the collection.")]
        KeyNotFoundException = 11011,
        [Description("A page not found.")]
        NotFoundException = 404,
        [Description("An attempt is made to access an element of an array or collection with an index that is outside its bounds.")]
        IndexOutOfRangeException = 10101,
        [Description("Requested user cannot be found on the database.")]
        UserNotFoundException = 1002,
        [Description("UnHandled Exception")]
        UnhandledException = 5000,
        [Description("Bad Request")]
        BadRequest = 400,
        [Description("Not Found")]
        NotFound = 404
    }
}
