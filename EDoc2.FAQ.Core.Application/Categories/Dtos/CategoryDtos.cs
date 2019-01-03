using EDoc2.FAQ.Core.Application.DtoBase;
using EDoc2.FAQ.Core.Domain.Categories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EDoc2.FAQ.Core.Application.Categories.Dtos
{
    public class CategoryDtos
    {
        #region 请求

        public class AddCategoryReq
        {
            /// <summary>
            /// 父类别编号
            /// </summary>
            public Guid? ParentId { get; set; }

            /// <summary>
            /// 分类名称
            /// </summary>
            [MaxLength(50)]
            [Required]
            public string Name { get; set; }

            /// <summary>
            /// 分类描述
            /// </summary>
            public string Description { get; set; }

            public Category To()
            {
                return new Category
                {
                    ParentId = ParentId,
                    Name = Name,
                    Description = Description
                };
            }
        }

        /// <summary>
        /// 禁用/启用类别请求
        /// </summary>
        public class EnableReq : EntityDto<Guid>
        {
            /// <summary>
            /// 禁用/启用
            /// </summary>
            [Required]
            public bool Enable { get; set; }
        }

        #endregion

        #region 响应

        public class CategroyNode
        {
            public Guid Key { get; set; }
            public Guid? ParentId { get; set; }

            public object Value { get; set; }

            public string Title { get; set; }

            public bool IsLeaf { get; set; }

            public List<CategroyNode> Children { get; set; } = new List<CategroyNode>();

            public static CategroyNode From(Category category)
            {
                return new CategroyNode
                {
                    Key = category.Id,
                    ParentId = category.ParentId,
                    Value = category.Id,
                    Title = category.Name,
                    IsLeaf = false
                };
            }
        }

        public class CategoryResp : EntityDto<Guid>
        {
            public Guid? ParentId { get; set; }

            public string Name { get; set; }

            public string Description { get; set; }

            public bool Enable { get; set; }

            public List<CategoryResp> Children { get; set; } = new List<CategoryResp>();

            public static CategoryResp From(Category category)
            {
                return new CategoryResp
                {
                    Id = category.Id,
                    ParentId = category.ParentId,
                    Name = category.Name,
                    Description = category.Description,
                    Enable = category.Enabled
                };
            }
        }

        #endregion
    }
}
