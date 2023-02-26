using coding_exercise_2_25_2023.Exceptions;
using coding_exercise_2_25_2023.Models;
using System.ComponentModel;

namespace coding_exercise_2_25_2023.Services
{
    public interface IUserServices
    {
        public Task<UserModel> Retrieve(string email);

        public Task<List<UserModel>> RetrieveAll();

        public void Create(UserModel user);

        public Task<bool> Update(UserModel update);

        public Task<bool> Delete(string email);

    }
}
