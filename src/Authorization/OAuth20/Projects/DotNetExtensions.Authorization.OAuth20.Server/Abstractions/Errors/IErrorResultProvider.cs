// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Errors.Exceptions;
using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Errors.Exceptions.Authorize;
using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Errors.Exceptions.Token;
using DotNetExtensions.Authorization.OAuth20.Server.Options;

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Errors;

public interface IErrorResultProvider
{
    public IErrorResult GetAuthorizeErrorResult<TException>(TException exception, string? state = null, string? additionalInfo = null, OAuth20ServerOptions? options = null)
        where TException : AuthorizeException;

    public IErrorResult GetAuthorizeErrorResult(DefaultAuthorizeErrorType defaultErrorType, string? state = null, string? additionalInfo = null, OAuth20ServerOptions? options = null);

    public IErrorResult GetAuthorizeErrorResult(string authorizeErrorCode, string? state = null, string? additionalInfo = null);

    public bool TryGetAuthorizeErrorResult<TException>(TException exception, out IErrorResult? result, string? state = null, string? additionalInfo = null, OAuth20ServerOptions? options = null)
        where TException : AuthorizeException;

    public bool TryGetAuthorizeErrorResult(DefaultAuthorizeErrorType defaultErrorType, out IErrorResult? result, string? state = null, string? additionalInfo = null, OAuth20ServerOptions? options = null);

    public bool TryGetAuthorizeErrorResult(string authorizeErrorCode, out IErrorResult? result, string? state = null, string? additionalInfo = null);

    public IErrorResult GetTokenErrorResult<TException>(TException exception, string? state = null, string? additionalInfo = null, OAuth20ServerOptions? options = null)
        where TException : TokenException;

    public IErrorResult GetTokenErrorResult(DefaultTokenErrorType defaultErrorType, string? state = null, string? additionalInfo = null, OAuth20ServerOptions? options = null);

    public IErrorResult GetTokenErrorResult(string tokenErrorCode, string? state = null, string? additionalInfo = null);

    public bool TryGetTokenErrorResult<TException>(TException exception, out IErrorResult? result, string? state = null, string? additionalInfo = null, OAuth20ServerOptions? options = null)
        where TException : TokenException;

    public bool TryGetTokenErrorResult(DefaultTokenErrorType defaultErrorType, out IErrorResult? result, string? state = null, string? additionalInfo = null, OAuth20ServerOptions? options = null);

    public bool TryGetTokenErrorResult(string tokenErrorCode, out IErrorResult? result, string? state = null, string? additionalInfo = null);

    public IResult GetErrorResult(OAuth20Exception exception, OAuth20ServerOptions? options = null);

    public bool TryGetErrorResult(OAuth20Exception exception, out IErrorResult? result, OAuth20ServerOptions? options = null);
}
