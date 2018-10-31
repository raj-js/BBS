using System;
using System.Collections.Generic;
using System.Linq;

namespace EDoc2.FAQ.Web.Services
{
    public class Rule
    {
        /// <summary>
        /// Min less than or equal {days} less than Max
        /// </summary>
        public (int Min, int Max)[] Range { get; set; }
        public int[] Points { get; set; }
        public int Score { get; set; }
        public string Decription { get; set; }
        public int Order { get; set; }
    }

    public class RuleComparer : IComparer<Rule>
    {
        public int Compare(Rule x, Rule y)
        {
            return x.Score.CompareTo(y.Score);
        }
    }

    /// <summary>
    /// 后期可置于数据库已便于更加灵活
    /// </summary>
    public class DailySignRule
    {
        public static SortedSet<Rule> Rules { get; set; }

        static DailySignRule()
        {
            Rules = new SortedSet<Rule>(new RuleComparer())
            {
                new Rule{Range = new []{(0, 5) },Score = 5},
                new Rule{Range = new []{(5, 15) },Score = 10},
                new Rule{Range = new []{(15, 30) },Score = 15},
                new Rule{Range = new []{(30, 100) },Score = 20},
                new Rule{Range = new []{(100, 365) },Score = 30},
                new Rule{Range = new []{(360, int.MaxValue) },Score = 50}
            };
        }

        public static Rule MatchRule(int days)
        {
            if (Rules == null)
                throw new TypeInitializationException(typeof(DailySignRule).FullName, new Exception(nameof(Rules)));

            var rule = Rules
                .Where(r => r.Range.Any(range => range.Min <= days && days < range.Max))
                .FirstOrDefault();

            return rule;
        }

        public static Rule Default => Rules.Min;
    }
}
