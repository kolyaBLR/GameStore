using GameStore.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.Domain.Entities;

namespace GameStore.Domain.Concrete
{
    public class EFCategoryRepository : ICategoryRepository
    {
        EFDbContext context = new EFDbContext();

        public IEnumerable<Category> Category
        {
            get { return context.Category; }
        }

        public Category DeleteCategory(int categoryId)
        {
            Category dbEntry = context.Category.Find(categoryId);
            if (dbEntry != null)
            {
                context.Category.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public void SaveCategory(Category category)
        {
            if (category.CategoryId == 0)
                context.Category.Add(category);
            else
            {
                Category dbEntry = context.Category.Find(category.CategoryId);
                if (dbEntry != null)
                {
                    dbEntry.Name = category.Name;
                }
            }
            context.SaveChanges();
        }
    }
}
