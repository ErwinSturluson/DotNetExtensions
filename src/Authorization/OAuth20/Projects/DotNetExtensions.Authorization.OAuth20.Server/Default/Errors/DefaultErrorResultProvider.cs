// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Errors;
using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Errors.Exceptions.Authorize;
using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Errors.Exceptions.Token;
using DotNetExtensions.Authorization.OAuth20.Server.Options;

namespace DotNetExtensions.Authorization.OAuth20.Server.Default.Errors;

public class DefaultErrorResultProvider : IErrorResultProvider
{
    private readonly IErrorMetadataCollection _errorMetadataCollection;

    public DefaultErrorResultProvider(IErrorMetadataCollection errorMetadataCollection)
    {
        _errorMetadataCollection = errorMetadataCollection;
    }

    public IErrorResult GetAuthorizeErrorResult(DefaultAuthorizeErrorType defaultErrorType, string? state = null, string? additionalInfo = null, OAuth20ServerOptions? options = null)
        => GetAuthorizeErrorResult(defaultErrorType.GetDescriptionAttributeValue(options), state, additionalInfo);

    public IErrorResult GetAuthorizeErrorResult(string authorizeErrorCode, string? state = null, string? additionalInfo = null)
    {
        if (TryGetAuthorizeErrorResult(authorizeErrorCode, out IErrorResult? result, state, additionalInfo))
        {
            return result!;
        }
        else
        {
            throw new InvalidOperationException($"{nameof(authorizeErrorCode)}:{authorizeErrorCode}");
        }
    }

    public bool TryGetAuthorizeErrorResult(DefaultAuthorizeErrorType defaultErrorType, out IErrorResult? result, string? state = null, string? additionalInfo = null, OAuth20ServerOptions? options = null)
        => TryGetAuthorizeErrorResult(defaultErrorType.GetDescriptionAttributeValue(options), out result, state, additionalInfo);

    public bool TryGetAuthorizeErrorResult(string authorizeErrorCode, out IErrorResult? result, string? state = null, string? additionalInfo = null)
    {
        ErrorMetadata? errorMetadata = _errorMetadataCollection.AuthorizeErrors.Values.FirstOrDefault(x => x.Code == authorizeErrorCode);

        if (errorMetadata is not null)
        {
            string? errorDescription = additionalInfo is null ?
                errorMetadata.Description :
                $"{errorMetadata.Description}({additionalInfo})";

            result = ErrorResult.Create(
                error: errorMetadata.Code,
                errorDescription: errorDescription,
                errorUri: errorMetadata.Uri,
                state
                );

            return true;
        }
        else
        {
            result = null;
            return false;
        }
    }

    public IErrorResult GetTokenErrorResult(DefaultTokenErrorType defaultErrorType, string? state = null, string? additionalInfo = null, OAuth20ServerOptions? options = null)
        => GetTokenErrorResult(defaultErrorType.GetDescriptionAttributeValue(options), state, additionalInfo);

    public IErrorResult GetTokenErrorResult(string tokenErrorCode, string? state = null, string? additionalInfo = null)
    {
        if (TryGetTokenErrorResult(tokenErrorCode, out IErrorResult? result, state, additionalInfo))
        {
            return result!;
        }
        else
        {
            throw new InvalidOperationException($"{nameof(tokenErrorCode)}:{tokenErrorCode}");
        }
    }

    public bool TryGetTokenErrorResult(DefaultTokenErrorType defaultErrorType, out IErrorResult? result, string? state = null, string? additionalInfo = null, OAuth20ServerOptions? options = null)
        => TryGetTokenErrorResult(defaultErrorType.GetDescriptionAttributeValue(options), out result, state, additionalInfo);

    public bool TryGetTokenErrorResult(string tokenErrorCode, out IErrorResult? result, string? state = null, string? additionalInfo = null)
    {
        ErrorMetadata? errorMetadata = _errorMetadataCollection.TokenErrors.Values.FirstOrDefault(x => x.Code == tokenErrorCode);

        if (errorMetadata is not null)
        {
            string? errorDescription = additionalInfo is null ?
                errorMetadata.Description :
                $"{errorMetadata.Description}({additionalInfo})";

            result = ErrorResult.Create(
                error: errorMetadata.Code,
                errorDescription: errorDescription,
                errorUri: errorMetadata.Uri,
                state
                );

            return true;
        }
        else
        {
            result = null;
            return false;
        }
    }

    public IErrorResult GetAuthorizeErrorResult<TException>(TException exception, string? state = null, string? additionalInfo = null, OAuth20ServerOptions? options = null)
        where TException : AuthorizeException
    {
        if (TryGetAuthorizeErrorResult(exception, out IErrorResult? result, state, additionalInfo, options))
        {
            return result!;
        }
        else
        {
            throw new InvalidOperationException($"{nameof(exception)}:{exception}");
        }
    }

    public bool TryGetAuthorizeErrorResult<TException>(TException exception, out IErrorResult? result, string? state = null, string? additionalInfo = null, OAuth20ServerOptions? options = null)
        where TException : AuthorizeException
    {
        result = null;

        bool success = exception switch
        {
            Abstractions.Errors.Exceptions.Authorize.InvalidRequestException => TryGetAuthorizeErrorResult(DefaultAuthorizeErrorType.InvalidRequest, out result, state, additionalInfo, options),
            Abstractions.Errors.Exceptions.Authorize.UnauthorizedClientException => TryGetAuthorizeErrorResult(DefaultAuthorizeErrorType.UnauthorizedClient, out result, state, additionalInfo, options),
            Abstractions.Errors.Exceptions.Authorize.AccessDeniedException => TryGetAuthorizeErrorResult(DefaultAuthorizeErrorType.AccessDenied, out result, state, additionalInfo, options),
            Abstractions.Errors.Exceptions.Authorize.UnsupportedResponseTypeException => TryGetAuthorizeErrorResult(DefaultAuthorizeErrorType.UnsupportedResponseType, out result, state, additionalInfo, options),
            Abstractions.Errors.Exceptions.Authorize.InvalidScopeException => TryGetAuthorizeErrorResult(DefaultAuthorizeErrorType.InvalidScope, out result, state, additionalInfo, options),
            Abstractions.Errors.Exceptions.Authorize.ServerErrorException => TryGetAuthorizeErrorResult(DefaultAuthorizeErrorType.ServerError, out result, state, additionalInfo, options),
            Abstractions.Errors.Exceptions.Authorize.TemporarilyUnavailableException => TryGetAuthorizeErrorResult(DefaultAuthorizeErrorType.TemporarilyUnavailable, out result, state, additionalInfo, options),
            _ => false
        };

        return success;
    }

    public IErrorResult GetTokenErrorResult<TException>(TException exception, string? state = null, string? additionalInfo = null, OAuth20ServerOptions? options = null) where TException : TokenException
    {
        if (TryGetTokenErrorResult(exception, out IErrorResult? result, state, additionalInfo, options))
        {
            return result!;
        }
        else
        {
            throw new InvalidOperationException($"{nameof(exception)}:{exception}");
        }
    }

    public bool TryGetTokenErrorResult<TException>(TException exception, out IErrorResult? result, string? state = null, string? additionalInfo = null, OAuth20ServerOptions? options = null) where TException : TokenException
    {
        result = null;

        bool success = exception switch
        {
            Abstractions.Errors.Exceptions.Token.InvalidRequestException => TryGetTokenErrorResult(DefaultTokenErrorType.InvalidRequest, out result, state, additionalInfo, options),
            Abstractions.Errors.Exceptions.Token.InvalidClientException => TryGetTokenErrorResult(DefaultTokenErrorType.InvalidClient, out result, state, additionalInfo, options),
            Abstractions.Errors.Exceptions.Token.InvalidGrantException => TryGetTokenErrorResult(DefaultTokenErrorType.InvalidGrant, out result, state, additionalInfo, options),
            Abstractions.Errors.Exceptions.Token.UnauthorizedClientException => TryGetTokenErrorResult(DefaultTokenErrorType.UnauthorizedClient, out result, state, additionalInfo, options),
            Abstractions.Errors.Exceptions.Token.UnsupportedGrantTypeException => TryGetTokenErrorResult(DefaultTokenErrorType.UnsupportedGrantType, out result, state, additionalInfo, options),
            Abstractions.Errors.Exceptions.Token.InvalidScopeException => TryGetTokenErrorResult(DefaultTokenErrorType.InvalidScope, out result, state, additionalInfo, options),
            _ => false
        };

        return success;
    }
}
