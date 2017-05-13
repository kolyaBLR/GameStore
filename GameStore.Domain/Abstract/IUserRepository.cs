using GameStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.Abstract
{
    public interface IUserRepository
    {
        IEnumerable<User> User { get; }
        void SaveUser(User user);
    }
}
