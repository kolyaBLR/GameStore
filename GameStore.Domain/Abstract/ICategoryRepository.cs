using GameStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.Abstract
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> Category { get; }
        void SaveCategory(Category category);
        Category DeleteCategory(int category);
    }
}
