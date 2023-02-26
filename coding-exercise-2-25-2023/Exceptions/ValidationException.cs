using System.Runtime.Serialization;

namespace coding_exercise_2_25_2023.Exceptions
{
    [Serializable]
    public class ValidationException : Exception
    {
        public ValidationException(string message) : base(message)
        {
        }
    }
}
