using Proyect.Data;
using Proyect.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyect.Core
{
    public class StickyNoteXUserBusiness
    {
        private readonly IRepositoryStickyUser _repositorySU; //la clase puede ser seteada solo una vez 
        public StickyNoteXUserBusiness()
        {
            _repositorySU = new RepositoryStickyUser();

        }

        public bool SaveOrUpdate(StickyNoteXUser stickyUser) //Upsert (Update / Insert)
        {
            if (stickyUser.StickyUserID <= 0)
                _repositorySU.Add(stickyUser);
            else
                _repositorySU.Update(stickyUser);

            return true;
        }

        public bool Delete(int id)
        {
            _repositorySU.Delete(id);
            return true;
        }

        public IEnumerable<StickyNoteXUser> GetNotesXUsers(int id)
        {
            return id <= 0
                ? _repositorySU.GetAll()
                : new List<StickyNoteXUser>() { _repositorySU.GetById(id) };
        }

        /*public IEnumerable<StickyNoteXUser> Filtered(string value)
        {
            var notesUsers = GetNotesXUsers(0);

            if (string.IsNullOrEmpty(value))
                return notesUsers;

            //value = value.ToLower();

            return notesUsers.Where(x =>
                (x.Username != null && x.Username.IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0) ||
                (x.Email != null && x.Email.IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0) ||
                (x.UserID.ToString().Contains(value))
            );
        }*/


    }
}
