namespace MiniStackOverflow.Domain.Exceptions
{
    public class DuplicateUserNameException : Exception
    {
        public DuplicateUserNameException() : base("UserName already taken") { }
    }
}
