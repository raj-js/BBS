namespace EDoc2.FAQ.Core.Application
{
    public interface IEntityDto<TPrimaryKey>
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
