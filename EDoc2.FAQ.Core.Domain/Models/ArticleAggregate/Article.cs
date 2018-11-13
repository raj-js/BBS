using EDoc2.FAQ.Core.Domain.SeedWork;
using System;
using System.Collections.Generic;

namespace EDoc2.FAQ.Core.Domain.Models.ArticleAggregate
{
    public class Article : Entity<Guid>
    {
        //标题
        private string _title;
        //摘要
        private string _summary;
        //内容
        private string _content;
        //关键字
        private string _keywords;
        //文章状态编号
        private int _articleStateId;
        //文章类型编号
        private int _articleTypeId;
        //文章属性
        private readonly List<ArticleProperty> _properties;
        //创建者编号
        private string _creator;
        //创建时间
        private DateTime _creationTime;

        public Article()
        {
            _properties = new List<ArticleProperty>();
        }

        public Article(string title, string summary, string content, string keywords, 
            ArticleState articleState, ArticleType articleType, string creatorId, DateTime creationTime)
        : this()
        {
            _title = title;
            _summary = summary;
            _content = content;
            _keywords = keywords;
            
        }

        public string GetTitle() => _title;
    }
}
