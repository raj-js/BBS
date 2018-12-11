namespace EDoc2.FAQ.Core.Domain.Exceptions
{
    public class DuplicateException : DomainException
    {
        public DuplicateException(string message)
            :base(message)
        {
            
        }
    }
}
