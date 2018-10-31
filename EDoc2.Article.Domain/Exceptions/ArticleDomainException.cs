using System;
using System.Collections.Generic;
using System.Text;

namespace EDoc2.Article.Domain.Exceptions
{
    public class ArticleDomainException : Exception
    {
        public ArticleDomainException()
        {
            
        }

        public ArticleDomainException(string message)
            :base(message)
        {
            
        }

        public ArticleDomainException(string message, Exception innerException)
            :base(message, innerException)
        {
            
        }
    }
}
