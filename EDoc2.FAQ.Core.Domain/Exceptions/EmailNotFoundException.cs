namespace EDoc2.FAQ.Core.Domain.Exceptions
{
    public class EmailNotFoundException : DomainException
    {
        public string Email { get; }

        public EmailNotFoundException(string email)
            : base($"未找到邮箱为 {email} 的用户")
        {
            Email = email;
        }
    }
}
