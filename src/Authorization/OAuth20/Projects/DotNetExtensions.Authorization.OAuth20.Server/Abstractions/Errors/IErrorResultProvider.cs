// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Errors.Exceptions;
using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Errors.Exceptions.Authorize;
using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Errors.Exceptions.Common;
using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Errors.Exceptions.Token;
using DotNetExtensions.Authorization.OAuth20.Server.Options;

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Errors;

public interface IErrorResultProvider
{
    public IResult GetErrorResult(OAuth20Exception exception);

    public bool TryGetErrorResult(OAuth20Exception exception, out IErrorResult? result);

    public IErrorResult GetCommonErrorResult<TException>(TException exception, string? state, string? additionalInfo = null)
    where TException : CommonException;

    public IErrorResult GetCommonErrorResult(DefaultCommonErrorType defaultErrorType, string? state = null, string? additionalInfo = null);

    public IErrorResult GetCommonErrorResult(string tokenErrorCode, string? state = null, string? additionalInfo = null);

    public bool TryGetCommonErrorResult<TException>(TException exception, out IErrorResult? result, string? state, string? additionalInfo = null)
        where TException : CommonException;

    public bool TryGetCommonErrorResult(DefaultCommonErrorType defaultErrorType, out IErrorResult? result, string? state = null, string? additionalInfo = null);

    public bool TryGetCommonErrorResult(string commonErrorCode, out IErrorResult? result, string? state = null, string? additionalInfo = null);

    public IErrorResult GetAuthorizeErrorResult<TException>(TException exception, string? state = null, string? additionalInfo = null)
        where TException : AuthorizeException;

    public IErrorResult GetAuthorizeErrorResult(DefaultAuthorizeErrorType defaultErrorType, string? state = null, string? additionalInfo = null);

    public IErrorResult GetAuthorizeErrorResult(string authorizeErrorCode, string? state = null, string? additionalInfo = null);

    public bool TryGetAuthorizeErrorResult<TException>(TException exception, out IErrorResult? result, string? state = null, string? additionalInfo = null)
        where TException : AuthorizeException;

    public bool TryGetAuthorizeErrorResult(DefaultAuthorizeErrorType defaultErrorType, out IErrorResult? result, string? state = null, string? additionalInfo = null);

    public bool TryGetAuthorizeErrorResult(string authorizeErrorCode, out IErrorResult? result, string? state = null, string? additionalInfo = null);

    public IErrorResult GetTokenErrorResult<TException>(TException exception, string? state = null, string? additionalInfo = null)
        where TException : TokenException;

    public IErrorResult GetTokenErrorResult(DefaultTokenErrorType defaultErrorType, string? state = null, string? additionalInfo = null);

    public IErrorResult GetTokenErrorResult(string tokenErrorCode, string? state = null, string? additionalInfo = null);

    public bool TryGetTokenErrorResult<TException>(TException exception, out IErrorResult? result, string? state = null, string? additionalInfo = null)
        where TException : TokenException;

    public bool TryGetTokenErrorResult(DefaultTokenErrorType defaultErrorType, out IErrorResult? result, string? state = null, string? additionalInfo = null);

    public bool TryGetTokenErrorResult(string tokenErrorCode, out IErrorResult? result, string? state = null, string? additionalInfo = null);
}
