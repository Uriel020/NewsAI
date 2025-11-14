using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAI.Core.Models.Category
{
    public class CategoryDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public bool IsActive { get; set; }
    }
}