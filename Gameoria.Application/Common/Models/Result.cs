using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Common/Models/Result.cs
namespace GameOria.Application.Common.Models;

public class Result
{
    public bool Succeeded { get; private set; }
    public string[] Errors { get; private set; }

    private Result(bool succeeded, string[] errors)
    {
        Succeeded = succeeded;
        Errors = errors;
    }

    public static Result Success()
    {
        return new Result(true, Array.Empty<string>());
    }

    public static Result Failure(string[] errors)
    {
        return new Result(false, errors);
    }
}

public class Result<T>
{
    public bool Succeeded { get; private set; }
    public T? Data { get; private set; }
    public string[] Errors { get; private set; }

    private Result(bool succeeded, T? data, string[] errors)
    {
        Succeeded = succeeded;
        Data = data;
        Errors = errors;
    }

    public static Result<T> Success(T data)
    {
        return new Result<T>(true, data, Array.Empty<string>());
    }

    public static Result<T> Failure(string[] errors)
    {
        return new Result<T>(false, default, errors);
    }

    // Convert from non-generic Result
    public static implicit operator Result<T>(Result result)
    {
        return result.Succeeded
            ? Success(default!)
            : Failure(result.Errors);
    }
}
