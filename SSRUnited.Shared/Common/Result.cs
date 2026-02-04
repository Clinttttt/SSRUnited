using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSRUnited.Shared.Common
{
    public class Result<T>
    {
        public bool is_success { get; set; }
        public T? value { get; set; }
        public int status_code { get; set; }
        public string? error { get; set; }
        public Result(bool is_success, T? value, int status_code, string? error = null)
        {
            this.is_success = is_success;
            this.value = value;
            this.status_code = status_code;
            this.error = error;
            this.error = error;
        }
        public static Result<T> Success(T value) => new(true, value, 200);
        public static Result<T> Failure(string error) => new(false, default, 500, error);
        public static Result<T> BadRequest() => new(false, default, 400);
        public static Result<T> NotFound() => new(false, default, 404);
        public static Result<T> Unauthorized() => new(false, default, 401);
        public static Result<T> Conflict() => new(false, default, 409);
        public static Result<T> Forbidden() => new(false, default, 403);
        public static Result<T> NoContent() => new(true, default, 204);
        public static Result<T> InternalServerError() => new(false, default, 500);

    }
}
