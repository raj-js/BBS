using EDoc2.FAQ.Core.Infrastructure.Extensions;
using System;

namespace EDoc2.FAQ.Core.Infrastructure.Exceptions
{
    public static class ExpManagerBuilder
    {
        public static ExpManager Handle<TE>(this ExpManager @self, TE e, Action action) where TE : Exception
        {

            return new ExpManager();
        }



        public static void Execute(this ExpManager @self, Action action)
        {

        }
    }
}
