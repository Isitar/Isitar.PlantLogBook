using System.Collections.Generic;

namespace Isitar.PlantLogBook.Core.Responses
{
    public class Response
    {
        public virtual bool Success { get; set; }

        public virtual IDictionary<string, IList<string>> ErrorMessages { get; } = new Dictionary<string, IList<string>>();

        public void AddErrorMessage(string key, string message)
        {
            if (!ErrorMessages.ContainsKey(key))
            {
                ErrorMessages.Add(key, new List<string>());
            }
            
            ErrorMessages[key].Add(message);
        }
    }
    public abstract class Response<T> : Response
    {
        public virtual T Data { get; set; }
    }
}