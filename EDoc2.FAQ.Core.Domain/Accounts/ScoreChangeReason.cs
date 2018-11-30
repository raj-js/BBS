using System;
using System.Collections.Generic;
using System.Linq;
using EDoc2.FAQ.Core.Domain.SeedWork;

namespace EDoc2.FAQ.Core.Domain.Accounts
{
    public class ScoreChangeReason : Enumeration
    {
        public static ScoreChangeReason SignIn = new ScoreChangeReason(1, "每日签到");
        public static ScoreChangeReason BestReply = new ScoreChangeReason(2, "最佳回复");
        public static ScoreChangeReason AskQuestion = new ScoreChangeReason(3, "发布问题");

        public ScoreChangeReason() { }

        public ScoreChangeReason(int id, string name)
            : base(id, name)
        {
        }

        public static IEnumerable<ScoreChangeReason> List() => new[] { SignIn, BestReply, AskQuestion };

        public static ScoreChangeReason FromName(string name)
        {
            var state = List().SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));
            if (state == null)
                throw new ArgumentException($"Possible values for CardType: {string.Join(",", List().Select(s => s.Name))}");

            return state;
        }

        public static ScoreChangeReason From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);
            if (state == null)
                throw new ArgumentException($"Possible values for CardType: {string.Join(",", List().Select(s => s.Name))}");

            return state;
        }
    }
}
