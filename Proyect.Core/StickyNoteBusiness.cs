using Proyect.Data;
using Proyect.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyect.Core
{
    public class StickyNoteBusiness
    {
        private readonly IRepositoryStickyNote _repositoryNote; //la clase puede ser seteada solo una vez 
        public StickyNoteBusiness()
        {
            _repositoryNote = new RepositoryStickyNote();

        }

        public bool SaveOrUpdate(StickyNote note) //Upsert (Update / Insert)
        {
            if (note.StickynoteID <= 0)
                _repositoryNote.Add(note);
            else
                _repositoryNote.Update(note);

            return true;
        }

        public bool Delete(int id)
        {
            _repositoryNote.Delete(id);
            return true;
        }

        public IEnumerable<StickyNote> GetNotes(int id)
        {
            return id <= 0
                ? _repositoryNote.GetAll()
                : new List<StickyNote>() { _repositoryNote.GetById(id) };
        }

        public IEnumerable<StickyNote> Filtered(string value)
        {
            var notes = GetNotes(0);

            if (string.IsNullOrEmpty(value))
                return notes;

            //value = value.ToLower();

            return notes.Where(x =>
                (x.Title != null && x.Title.IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0) ||
                (x.Description != null && x.Description.IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0) ||
                (x.Status != null && x.Status.IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0) ||
                (x.DueDate.ToString().Contains(value)) ||
                (x.StickynoteID.ToString().Contains(value))
            );
        }


    }
}
