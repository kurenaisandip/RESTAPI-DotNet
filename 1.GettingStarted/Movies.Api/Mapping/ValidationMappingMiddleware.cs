﻿using FluentValidation;
using Movies.Contracts.Responses;

namespace Movies.Api.Mapping;

public class ValidationMappingMiddleware
{
    // this is a classic way to do middleware in ASP.NET Core
    private readonly RequestDelegate _next;

    public ValidationMappingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException e)
        {
            context.Response.StatusCode = 400;
            var validationFailureResponse = new ValidationFailureResponse
            {
                Errors = e.Errors.Select(x => new ValidationResponse
                {
                    PropertyName = x.PropertyName,
                    Message = x.ErrorMessage
                })
            };
            
            await context.Response.WriteAsJsonAsync(validationFailureResponse);
        }
    }
}