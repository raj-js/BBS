using System.ComponentModel.DataAnnotations;

namespace EDoc2.FAQ.Core.Application.DtoBase
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

        [Required]
        public TPrimaryKey Id { get; set; }
    }
}
