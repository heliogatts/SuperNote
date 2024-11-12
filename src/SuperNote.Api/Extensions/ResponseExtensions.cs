﻿using FastEndpoints;
using FluentResults;
using FluentValidation.Results;
using SuperNote.Domain.Abstractions.ErrorHandling;

namespace SuperNote.Api.Extensions;

public static class ResponseExtensions
{
    public static async Task SendProblemDetailsResponse<T>(
        this IEndpoint ep,
        Result<T> result,
        CancellationToken cancellationToken)
    {
        if (result.IsSuccess)
        {
            throw new ArgumentException("The Result<T> object must be in a failed state.");
        }

        var errorType = GetErrorType(result.Errors.First());

        var statusCode = errorType switch
        {
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.InvalidData => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };
        
        var failures = ToFailures(result.Errors);
        await ep.HttpContext.Response.SendErrorsAsync(failures, statusCode, cancellation: cancellationToken);

        static ErrorType GetErrorType(IError error)
            => (ErrorType)error.Metadata[nameof(ErrorType)];

        static List<ValidationFailure> ToFailures(List<IError> errors)
            => errors.Select(error =>
            {
                var errorCode = error.Metadata[DomainError.ErrorCodeLiteral];
                return new ValidationFailure(errorCode.ToString(), error.Message);
            }).ToList();
    }
}