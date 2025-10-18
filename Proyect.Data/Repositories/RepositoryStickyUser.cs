using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyect.Data.Repositories
{
    public interface IRepositoryStickyUser: IRepositoryBase<StickyNoteXUser>
    {
    }

    public class RepositoryStickyUser : RepositoryBase<StickyNoteXUser>, IRepositoryStickyUser
    {
        public RepositoryStickyUser() : base()
        {
        }
    }
}
