using System.ComponentModel;

namespace EDoc2.FAQ.Core.Domain.Accounts
{
    public enum UserScoreChangeReason
    {
        [DisplayName("每日签到")]
        SignIn,

        [DisplayName("最佳回复")]
        BestReply,

        [DisplayName("发布问题")]
        AskQuestion
    }

    //public class UserScoreChangeReason : Enumeration
    //{
    //    public static UserScoreChangeReason SignIn = new UserScoreChangeReason(1, "每日签到");
    //    public static UserScoreChangeReason BestReply = new UserScoreChangeReason(2, "最佳回复");
    //    public static UserScoreChangeReason AskQuestion = new UserScoreChangeReason(3, "发布问题");

    //    public UserScoreChangeReason() { }

    //    public UserScoreChangeReason(int id, string name)
    //        : base(id, name)
    //    {
    //    }

    //    public static IEnumerable<UserScoreChangeReason> List() => new[] { SignIn, BestReply, AskQuestion };

    //    public static UserScoreChangeReason FromName(string name)
    //    {
    //        var state = List().SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));
    //        if (state == null)
    //            throw new ArgumentException($"Possible values for CardType: {string.Join(",", List().Select(s => s.Name))}");

    //        return state;
    //    }

    //    public static UserScoreChangeReason From(int id)
    //    {
    //        var state = List().SingleOrDefault(s => s.Id == id);
    //        if (state == null)
    //            throw new ArgumentException($"Possible values for CardType: {string.Join(",", List().Select(s => s.Name))}");

    //        return state;
    //    }
    //}
}
