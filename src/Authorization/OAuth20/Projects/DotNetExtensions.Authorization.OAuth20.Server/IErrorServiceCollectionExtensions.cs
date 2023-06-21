﻿// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Errors;
using DotNetExtensions.Authorization.OAuth20.Server.Default.Errors;
using DotNetExtensions.Authorization.OAuth20.Server.Options;
using Microsoft.Extensions.Options;

namespace DotNetExtensions.Authorization.OAuth20.Server;

public static class IErrorServiceCollectionExtensions
{
    public static IServiceCollection SetOAuth20Errors(this IServiceCollection services)
    {
        services.AddScoped<IErrorMetadataCollection, DefaultErrorMetadataCollection>();

        services.SetOAuth20DefaultErrors();

        services.SetOAuth20ErrorsFromConfiguration();

        services.AddScoped<IErrorResultProvider, DefaultErrorResultProvider>();

        return services;
    }

    public static IServiceCollection SetOAuth20DefaultErrors(this IServiceCollection services)
    {
        services.SetOAuth20DefaultTokenErrors();
        services.SetOAuth20DefaultAuthorizeErrors();

        return services;
    }

    public static IServiceCollection SetOAuth20DefaultAuthorizeErrors(this IServiceCollection services)
    {
        var servicesScope = services.BuildServiceProvider().CreateScope();
        var options = servicesScope.ServiceProvider.GetRequiredService<IOptions<OAuth20ServerOptions>>().Value;

        services.SetOAuth20DefaultTokenError(
            code: options.AuthorizeInvalidRequestErrorCode ?? "invalid_request",
            description:
                "The request is missing a required parameter, includes an invalid parameter value, includes a parameter more than once, or is otherwise malformed.",
            uri: "https://datatracker.ietf.org/doc/html/rfc6749#section-4.1.2.1");

        services.SetOAuth20DefaultTokenError(
            code: options.AuthorizeUnauthorizedClientErrorCode ?? "unauthorized_client",
            description:
                "The client is not authorized to request an authorization code using this method.",
            uri: "https://datatracker.ietf.org/doc/html/rfc6749#section-4.1.2.1");

        services.SetOAuth20DefaultTokenError(
            code: options.AuthorizeAccessDeniedErrorCode ?? "access_denied",
            description:
                "The resource owner or authorization server denied the request.",
            uri: "https://datatracker.ietf.org/doc/html/rfc6749#section-4.1.2.1");

        services.SetOAuth20DefaultTokenError(
            code: options.AuthorizeUnsupportedResponseTypeErrorCode ?? "unsupported_response_type",
            description:
                "The authorization server does not support obtaining an authorization code using " +
                "this method.",
            uri: "https://datatracker.ietf.org/doc/html/rfc6749#section-4.1.2.1");

        services.SetOAuth20DefaultTokenError(
            code: options.AuthorizeInvalidScopeErrorCode ?? "invalid_scope",
            description:
                "The requested scope is invalid, unknown, or malformed.",
            uri: "https://datatracker.ietf.org/doc/html/rfc6749#section-4.1.2.1");

        services.SetOAuth20DefaultTokenError(
            code: options.AuthorizeServerErrorErrorCode ?? "server_error",
            description:
                "The authorization server encountered an unexpected condition that prevented it " +
                "from fulfilling the request. (This error code is needed because a 500 Internal " +
                "Server Error HTTP status code cannot be returned to the client via an HTTP redirect.)",
            uri: "https://datatracker.ietf.org/doc/html/rfc6749#section-4.1.2.1");

        services.SetOAuth20DefaultTokenError(
            code: options.AuthorizeTemporarilyUnavailableErrorCode ?? "temporarily_unavailable",
            description:
                "The authorization server is currently unable to handle the request due to a " +
                "temporary overloading or maintenance of the server.  (This error code is needed " +
                "because a 503 Service Unavailable HTTP status code cannot be returned to the " +
                "client via an HTTP redirect.)",
            uri: "https://datatracker.ietf.org/doc/html/rfc6749#section-4.1.2.1");

        return services;
    }

    public static IServiceCollection SetOAuth20DefaultTokenErrors(this IServiceCollection services)
    {
        var servicesScope = services.BuildServiceProvider().CreateScope();
        var options = servicesScope.ServiceProvider.GetRequiredService<IOptions<OAuth20ServerOptions>>().Value;

        services.SetOAuth20DefaultTokenError(
            code: options.TokenInvalidRequestErrorCode ?? "invalid_request",
            description:
                "The request is missing a required parameter, includes an invalid parameter value, " +
                "includes a parameter more than once, or is otherwise malformed.",
            uri: "https://datatracker.ietf.org/doc/html/rfc6749#section-5.2");

        services.SetOAuth20DefaultTokenError(
            code: options.TokenInvalidClientErrorCode ?? "invalid_client",
            description:
                "Client authentication failed (e.g., unknown client, no client authentication included, " +
                "or unsupported authentication method).  The authorization server MAY return an HTTP 401 " +
                "(Unauthorized) status code to indicate which HTTP authentication schemes are supported.  " +
                "If the client attempted to authenticate via the \"Authorization\" request header field, " +
                "the authorization server MUST respond with an HTTP 401 (Unauthorized) status code and include " +
                "the \"WWW-Authenticate\" response header field matching the authentication scheme used by the client.",
            uri: "https://datatracker.ietf.org/doc/html/rfc6749#section-5.2");

        services.SetOAuth20DefaultTokenError(
            code: options.TokenInvalidGrantErrorCode ?? "invalid_grant",
            description: "The provided authorization grant (e.g., authorization code, resource owner credentials) " +
                "or refresh token is invalid, expired, revoked, does not match the redirection URI used in the " +
                "authorization request, or was issued to another client.",
            uri: "https://datatracker.ietf.org/doc/html/rfc6749#section-5.2");

        services.SetOAuth20DefaultTokenError(
            code: options.TokenUnauthorizedClientErrorCode ?? "unauthorized_client",
            description: "The authenticated client is not authorized to use this authorization grant type.",
            uri: "https://datatracker.ietf.org/doc/html/rfc6749#section-5.2");

        services.SetOAuth20DefaultTokenError(
            code: options.TokenUnsupportedGrantTypeErrorCode ?? "unsupported_grant_type",
            description: "The authorization grant type is not supported by the authorization server.",
            uri: "https://datatracker.ietf.org/doc/html/rfc6749#section-5.2");

        services.SetOAuth20DefaultTokenError(
            code: options.TokenInvalidScopeErrorCode ?? "invalid_scope",
            description: "The requested scope is invalid, unknown, malformed, or exceeds the scope granted by the resource owner.",
            uri: "https://datatracker.ietf.org/doc/html/rfc6749#section-5.2");

        return services;
    }

    public static IServiceCollection SetOAuth20DefaultAuthorizeError(this IServiceCollection services, string code, string? description = null, string? uri = null)
    {
        services.SetOAuth20AuthorizeError(ErrorMetadata.Create(code, description, uri));

        return services;
    }

    public static IServiceCollection SetOAuth20DefaultTokenError(this IServiceCollection services, string code, string? description = null, string? uri = null)
    {
        services.SetOAuth20TokenError(ErrorMetadata.Create(code, description, uri));

        return services;
    }

    public static IServiceCollection SetOAuth20ErrorsFromConfiguration(this IServiceCollection services)
    {
        var servicesScope = services.BuildServiceProvider().CreateScope();
        var options = servicesScope.ServiceProvider.GetRequiredService<IOptions<OAuth20ServerOptions>>().Value;

        foreach (var errorOptions in options.AuthorizeErrors)
        {
            var errorMetadata = ErrorMetadata.Create(errorOptions.Code, errorOptions.Description, errorOptions.Uri);

            SetOAuth20AuthorizeError(services, errorMetadata);
        }

        foreach (var errorOptions in options.TokenErrors)
        {
            var errorMetadata = ErrorMetadata.Create(errorOptions.Code, errorOptions.Description, errorOptions.Uri);

            SetOAuth20TokenError(services, errorMetadata);
        }

        return services;
    }

    public static IServiceCollection SetOAuth20AuthorizeError(this IServiceCollection services, ErrorMetadata errorMetadata)
    {
        using var servicesScope = services.BuildServiceProvider().CreateScope();
        var errorMetadataCollection = servicesScope.ServiceProvider.GetRequiredService<IErrorMetadataCollection>();

        errorMetadataCollection.AuthorizeErrors[errorMetadata.Code] = errorMetadata;

        return services;
    }

    public static IServiceCollection SetOAuth20TokenError(this IServiceCollection services, ErrorMetadata errorMetadata)
    {
        using var servicesScope = services.BuildServiceProvider().CreateScope();
        var errorMetadataCollection = servicesScope.ServiceProvider.GetRequiredService<IErrorMetadataCollection>();

        errorMetadataCollection.TokenErrors[errorMetadata.Code] = errorMetadata;

        return services;
    }
}
