namespace EDoc2.FAQ.Api.Models
{
    public interface IReqBase<T> where T : class
    {
        T ToDto();
    }
}
