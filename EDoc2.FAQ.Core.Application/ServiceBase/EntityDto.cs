namespace EDoc2.FAQ.Core.Application.ServiceBase
{
    public interface IEntityDto<out TPrimaryKey>
    {
        TPrimaryKey Id { get; }
    }

    public class EntityDto<TPrimaryKey> : IEntityDto<TPrimaryKey>
    {
        public EntityDto()
        {

        }

        public EntityDto(TPrimaryKey id)
        {
            Id = id;
        }

        public TPrimaryKey Id { get; set; }
    }
}
