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

        // ---------------------------
        // Nuevo: Login por Email + Password (texto plano)
        // Devuelve el objeto User o null si no existe
        // ---------------------------
        public User Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return null;

            // asume que _repositoryUser.GetAll() devuelve entidad User con Email y Password
            return _repositoryUser.GetAll()
                                  .FirstOrDefault(u => string.Equals(u.Email, email, StringComparison.OrdinalIgnoreCase)
                                                    && u.Password == password);
        }

        // ---------------------------
        // Nuevo: Comprueba si ya existe email (útil para registro)
        // ---------------------------
        public bool ExistsEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return false;

            return _repositoryUser.GetAll()
                                  .Any(u => string.Equals(u.Email, email, StringComparison.OrdinalIgnoreCase));
        }
    }
}
