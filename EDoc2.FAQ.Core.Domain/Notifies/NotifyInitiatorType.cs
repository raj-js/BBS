using System.ComponentModel;

namespace EDoc2.FAQ.Core.Domain.Notifies
{
    /// <summary>
    /// 消息发起者类型
    /// </summary>
    public enum NotifyInitiatorType
    {
        [DisplayName("系统")]
        System = 1,

        [DisplayName("管理员")]
        Administrator = 1 << 1,

        [DisplayName("版主")]
        Moderator = 1 << 2,

        [DisplayName("会员")]
        Member = 1 << 3
    }

    //public class NotifyInitiatorType : Enumeration
    //{
    //    public static NotifyInitiatorType System = new NotifyInitiatorType(1, "系统");
    //    public static NotifyInitiatorType Moderator = new NotifyInitiatorType(2, "版主");
    //    public static NotifyInitiatorType Member = new NotifyInitiatorType(3, "会员");

    //    public NotifyInitiatorType()
    //    {
    //    }

    //    public NotifyInitiatorType(int id, string name)
    //    : base(id, name)
    //    {

    //    }

    //    public static IEnumerable<NotifyInitiatorType> List() => new[] { System, Moderator, Member };

    //    public static NotifyInitiatorType FromName(string name)
    //    {
    //        var type = List().SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));
    //        if (type == null)
    //            throw new ArgumentException($"Possible values for CardType: {string.Join(",", List().Select(s => s.Name))}");

    //        return type;
    //    }

    //    public static NotifyInitiatorType From(int id)
    //    {
    //        var type = List().SingleOrDefault(s => s.Id == id);
    //        if (type == null)
    //            throw new ArgumentException($"Possible values for CardType: {string.Join(",", List().Select(s => s.Name))}");

    //        return type;
    //    }
    //}
}
