using Proyect.Data;
using Proyect.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyect.Core
{
    public class UserBusiness
    {
        private readonly IRepositoryUser _repositoryUser; //la clase puede ser seteada solo una vez 
        public UserBusiness()
        {
            _repositoryUser = new RepositoryUser();

        }

        public bool SaveOrUpdate(User user) //Upsert (Update / Insert)
        {
            if (user.UserID <= 0)
                _repositoryUser.Add(user);
            else
                _repositoryUser.Update(user);

            return true;
        }

        public bool Delete(int id)
        {
            _repositoryUser.Delete(id);
            return true;
        }

        public IEnumerable<User> GetUsers(int id)
        {
            return id <= 0
                ? _repositoryUser.GetAll()
                : new List<User>() { _repositoryUser.GetById(id) };
        }

        public IEnumerable<User> Filtered(string value)
        {
            var users = GetUsers(0);

            if (string.IsNullOrEmpty(value))
                return users;

            //value = value.ToLower();

            return users.Where(x =>
                (x.Username != null && x.Username.IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0) ||
                (x.Email != null && x.Email.IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0) ||
                (x.UserID.ToString().Contains(value))
            );
        }


    }
}
