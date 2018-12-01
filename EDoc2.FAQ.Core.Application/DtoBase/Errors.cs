namespace EDoc2.FAQ.Core.Application.DtoBase
{
    /// <summary>
    /// 定义错误 如果使用本地化， 可忽略 Error.Description , 关注 Error.Code 即可
    /// </summary>
    public static class Errors
    {
        public static readonly Error AccountNotFound = new Error { Code = nameof(AccountNotFound), Description = "用户不存在" };
        public static readonly Error InvalidOperation = new Error { Code = nameof(InvalidOperation), Description = "无效操作" };
        public static readonly Error AccountMuted = new Error { Code = nameof(AccountMuted), Description = "用户已屏蔽" };
        public static readonly Error EmailNotFound = new Error { Code = nameof(EmailNotFound), Description = "邮箱不存在" };
    }
}
