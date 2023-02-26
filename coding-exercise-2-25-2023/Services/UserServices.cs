using coding_exercise_2_25_2023.Exceptions;
using coding_exercise_2_25_2023.Models;
using System.ComponentModel;

namespace coding_exercise_2_25_2023.Services
{
    public class UserServices : IUserServices
    {
        private static List<UserModel>? Users;
        public UserServices()
        {
            Users = new List<UserModel>
            {
                new UserModel { FirstName = "Marcos", LastName = "Tassara", DateOfBirth = new DateTime(1994,8,28), Email= "marcos.tassara3@gmail.com"},
            };

        }

        public async Task<UserModel> Retrieve(string email)
        {
            UserModel model = Users.FirstOrDefault(x => x.Email == email);
            if (model != null) { model.CalculateAge(); }
            return model;
        }

        public async Task<List<UserModel>> RetrieveAll()
        {
            Users.ForEach(user => user.CalculateAge());
            return Users;
        }

        public void Create(UserModel user)
        {
            if (!Users.Exists(x => x.Email == user.Email))
            {
                try
                {
                    user.Validate();
                    Users.Add(user);
                }
                catch (ValidationException ex)
                {
                    throw ex;
                }
            }
            else
            {
                throw new ValidationException("A user with the specified email already exists");
            }
        }


        public async Task<bool> Update(UserModel update)
        {
            UserModel current = Users.FirstOrDefault(x => x.Email == update.Email);
            if (current != null)
            {
                try
                {
                    update.Validate();
                    current.FirstName = update.FirstName;
                    current.LastName = update.LastName;
                    current.DateOfBirth = update.DateOfBirth;
                    return true;
                }
                catch (ValidationException ex)
                {
                    throw ex;
                }
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> Delete(string email)
        {
            UserModel current = Users.FirstOrDefault(x => x.Email == email);

            if (current != null)
            {
                Users.Remove(current);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
