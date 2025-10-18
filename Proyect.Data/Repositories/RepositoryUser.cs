using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyect.Data.Repositories
{
    public interface IRepositoryUser : IRepositoryBase<User>
    {
    }

    public class RepositoryUser : RepositoryBase<User>, IRepositoryUser
    {
        public RepositoryUser() : base()
        {
        }
    }
}
