using GameStore.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.Domain.Entities;

namespace GameStore.Domain.Concrete
{
    public class EFUserRepository : IUserRepository
    {
        EFDbContext context = new EFDbContext();

        public IEnumerable<User> User
        {
            get { return context.User; }
        }
        public void SaveUser(User user)
        {
            if (user.UserId == 0)
                context.User.Add(user);
            else
            {
                User dbEntry = context.User.Find(user.UserId);
                if (dbEntry != null)
                {
                    dbEntry.FirstName = user.FirstName;
                    dbEntry.LastName = user.LastName;
                    dbEntry.Email = user.Email;
                    dbEntry.Passwod = user.Passwod;
                }
            }
            context.SaveChanges();
        }
    }
}
