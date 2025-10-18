using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyect.Data.Repositories
{
   public interface IRepositoryStickyNote : IRepositoryBase<StickyNote>
    {
    }

    public class RepositoryStickyNote : RepositoryBase<StickyNote>, IRepositoryStickyNote
    {
        public RepositoryStickyNote() : base()
        {
        }
    }
}
