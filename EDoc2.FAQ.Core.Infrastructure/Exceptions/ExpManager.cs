using System;

namespace EDoc2.FAQ.Core.Infrastructure.Extensions
{
    public class ExpManager
    {
        public static ExpManager Instance => new ExpManager();

        //public TYPE Type { get; set; }


        //public static ExpManager Handle<TE>(Action @try, Action<TE> @catch = null, Action @finally = null) where TE : Exception
        //{
        //    if (@try.IsNull())
        //        throw new ArgumentNullException(nameof(@try));

        //    try
        //    {
        //        @try();
        //    }
        //    catch (TE e)
        //    {
        //        @catch?.Invoke(e);
        //    }
        //    finally
        //    {
        //        @finally?.Invoke();
        //    }
        //}
    }
}
