using System.Collections.Generic;

namespace AirplaneServices.Domain.Entities
{
    public class Result<T>
    {
        public Result()
        {
            this.Errors = new List<string>();
        }

        public T Response { get; set; }

        public IList<string> Errors { get; set; }

        public Result<T> ResultError(string error)
        {
            this.Errors.Add(error);
            return this;
        }

        public Result<T> ResultResponse(T response)
        {
            this.Response = response;
            return this;
        }
    }
}