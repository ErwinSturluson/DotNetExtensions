﻿// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.ServerSigningCredentials;
using DotNetExtensions.Authorization.OAuth20.Server.Default.ServerSigningCredentials;
using DotNetExtensions.Authorization.OAuth20.Server.Options;
using DotNetExtensions.Authorization.OAuth20.Server.Options.ServerSigningCredentials;
using DotNetExtensions.Authorization.OAuth20.Server.Options.ServerSigningCredentials.Enumerations;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace DotNetExtensions.Authorization.OAuth20.Server;

public static class IServerSigningCredentialsServiceCollectionExtensions
{
    public static IServiceCollection SetOAuth20ServerSigningCredentials(this IServiceCollection services)
    {
        services.AddSingleton<IServerSigningCredentialsCollection, DefaultServerSigningCredentialsCollection>();

        services.SetOAuth20ServerSigningCredentialsFromConfiguration();

        services.AddScoped<IServerSigningCredentialsProvider, DefaultServerSigningCredentialsProvider>();

        return services;
    }

    public static IServiceCollection SetOAuth20ServerSigningCredentialsFromConfiguration(this IServiceCollection services)
    {
        var options = services.BuildServiceProvider().GetRequiredService<IOptions<OAuth20ServerOptions>>().Value;

        if (options.ServerSigningCredentials?.X509Certificate2List is not null && options.ServerSigningCredentials.X509Certificate2List.Any())
        {
            foreach (var x509Certificate2Options in options.ServerSigningCredentials.X509Certificate2List)
            {
                X509Certificate2 x509Certificate2 = x509Certificate2Options.DeploymentType switch
                {
                    X509Certificate2DeploymentType.Pem => CreateFromPem(x509Certificate2Options),
                    X509Certificate2DeploymentType.EncryptedPem => CreateFromEncryptedPem(x509Certificate2Options),
                    X509Certificate2DeploymentType.PemFile => CreateFromPemFile(x509Certificate2Options),
                    X509Certificate2DeploymentType.EncryptedPemFile => CreateFromEncryptedPemFile(x509Certificate2Options),

                    _ => throw new NotSupportedException($"{nameof(x509Certificate2Options.DeploymentType)}:{x509Certificate2Options.DeploymentType}"),
                };

                X509SecurityKey x509SecurityKey = new(x509Certificate2);
                SigningCredentials signingCredentials = new(x509SecurityKey, x509Certificate2Options.Algorithm);

                services.SetOAuth20ServerSigningCredentialsInstance(signingCredentials);
            }
        }

        return services;
    }

    public static IServiceCollection SetOAuth20ServerSigningCredentialsInstance(this IServiceCollection services, SigningCredentials signingCredentials)
    {
        var errorMetadataCollection = services.BuildServiceProvider().GetRequiredService<IServerSigningCredentialsCollection>();

        errorMetadataCollection.AlgorithmKeySigningCredentials[signingCredentials.Algorithm] = signingCredentials;
        services.AddSingleton(errorMetadataCollection);
        return services;
    }

    public static IServiceCollection SetOAuth20ServerSelfSignedCredentials(this IServiceCollection services)
    {
        var options = services.BuildServiceProvider().GetRequiredService<IOptions<OAuth20ServerOptions>>().Value;

        var selfSigningCredentialsOptions = options.ServerSigningCredentials?.SelfSigningCredentialsList;

        var securityKeysOptionsList = selfSigningCredentialsOptions?.SecurityKeys;

        if (securityKeysOptionsList is null || !securityKeysOptionsList.Any())
        {
            securityKeysOptionsList = Enum.GetValues<SecurityAlgorithmType>().Select(x => new SecurityKeyOptions
            {
                SecurityKeySize = 2048,
                SecurityKeyId = Guid.NewGuid().ToString("N"),
                SecurityAlgorithmType = x
            });
        }

        foreach (var securityKeysOptions in securityKeysOptionsList)
        {
            int securityKeySize = securityKeysOptions.SecurityKeySize ?? 2048;
            string securityKeyId = securityKeysOptions.SecurityKeyId ?? Guid.NewGuid().ToString("N");

            services.SetOAuth20ServerSelfSignedCredentialsInstance(securityKeySize, securityKeyId, securityKeysOptions.SecurityAlgorithmType);
        }

        return services;
    }

    public static IServiceCollection SetOAuth20ServerSelfSignedCredentialsInstance(this IServiceCollection services, int securityKeySize, string securityKeyId, SecurityAlgorithmType securityAlgorithmType)
    {
        var securityKey = GetSecurityKey(securityKeySize, securityKeyId, securityAlgorithmType);

        var signingCredentials = new SigningCredentials(securityKey, securityAlgorithmType.GetFieldNameAttributeValue());

        services.SetOAuth20ServerSigningCredentialsInstance(signingCredentials);

        return services;
    }

    private static SecurityKey GetSecurityKey(int securityKeySize, string rsaSecurityKeyId, SecurityAlgorithmType securityAlgorithmType)
    {
        SecurityKey securityKey = securityAlgorithmType switch
        {
            SecurityAlgorithmType.RsaSha256 => GetRsaSecurityKey(securityKeySize, rsaSecurityKeyId),
            SecurityAlgorithmType.RsaSha384 => GetRsaSecurityKey(securityKeySize, rsaSecurityKeyId),
            SecurityAlgorithmType.RsaSha512 => GetRsaSecurityKey(securityKeySize, rsaSecurityKeyId),
            SecurityAlgorithmType.RsaSsaPssSha256 => GetRsaSecurityKey(securityKeySize, rsaSecurityKeyId),
            SecurityAlgorithmType.RsaSsaPssSha384 => GetRsaSecurityKey(securityKeySize, rsaSecurityKeyId),
            SecurityAlgorithmType.RsaSsaPssSha512 => GetRsaSecurityKey(securityKeySize, rsaSecurityKeyId),
            SecurityAlgorithmType.EcdsaSha256 => GetEcdsaSecurityKey(rsaSecurityKeyId, securityAlgorithmType),
            SecurityAlgorithmType.EcdsaSha384 => GetEcdsaSecurityKey(rsaSecurityKeyId, securityAlgorithmType),
            SecurityAlgorithmType.EcdsaSha512 => GetEcdsaSecurityKey(rsaSecurityKeyId, securityAlgorithmType),
            _ => throw new NotSupportedException($"{nameof(securityAlgorithmType)}{securityAlgorithmType}")
        };

        return securityKey;
    }

    private static RsaSecurityKey GetRsaSecurityKey(int securityKeySize, string rsaSecurityKeyId)
    {
        RSA rsa = RSA.Create(securityKeySize);
        RsaSecurityKey rsaSecurityKey = new(rsa)
        {
            KeyId = rsaSecurityKeyId,
        };

        return rsaSecurityKey;
    }

    private static ECDsaSecurityKey GetEcdsaSecurityKey(string rsaSecurityKeyId, SecurityAlgorithmType securityAlgorithmType)
    {
        ECCurve ecCurve = securityAlgorithmType switch
        {
            SecurityAlgorithmType.EcdsaSha256 => ECCurve.NamedCurves.nistP256,
            SecurityAlgorithmType.EcdsaSha384 => ECCurve.NamedCurves.nistP384,
            SecurityAlgorithmType.EcdsaSha512 => ECCurve.NamedCurves.nistP521,
            _ => throw new NotSupportedException($"{nameof(securityAlgorithmType)}{securityAlgorithmType}")
        };

        ECDsa ecdsa = ECDsa.Create(ecCurve);

        ECDsaSecurityKey ecdsaSecurityKey = new(ecdsa)
        {
            KeyId = rsaSecurityKeyId,
        };

        return ecdsaSecurityKey;
    }

    private static X509Certificate2 CreateFromEncryptedPem(X509Certificate2Options x509Certificate2Options)
    {
        if (x509Certificate2Options.CertificateContent is null) throw new ArgumentNullException(nameof(x509Certificate2Options.CertificateContent));
        if (x509Certificate2Options.KeyContent is null) throw new ArgumentNullException(nameof(x509Certificate2Options.KeyContent));
        if (x509Certificate2Options.Password is null) throw new ArgumentNullException(nameof(x509Certificate2Options.Password));

        return X509Certificate2.CreateFromEncryptedPem(x509Certificate2Options.CertificateContent!, x509Certificate2Options.KeyContent!, x509Certificate2Options.Password!);
    }

    private static X509Certificate2 CreateFromPemFile(X509Certificate2Options x509Certificate2Options)
    {
        if (x509Certificate2Options.CertificateFileName is null) throw new ArgumentNullException(nameof(x509Certificate2Options.CertificateFileName));

        return X509Certificate2.CreateFromPemFile(x509Certificate2Options.CertificateFileName!, x509Certificate2Options.KeyFileName);
    }

    private static X509Certificate2 CreateFromEncryptedPemFile(X509Certificate2Options x509Certificate2Options)
    {
        if (x509Certificate2Options.CertificateFileName is null) throw new ArgumentNullException(nameof(x509Certificate2Options.CertificateFileName));
        if (x509Certificate2Options.Password is null) throw new ArgumentNullException(nameof(x509Certificate2Options.Password));

        return X509Certificate2.CreateFromEncryptedPemFile(x509Certificate2Options.CertificateFileName!, x509Certificate2Options.Password!, x509Certificate2Options.KeyFileName!);
    }

    private static X509Certificate2 CreateFromPem(X509Certificate2Options x509Certificate2Options)
    {
        if (x509Certificate2Options.CertificateContent is null) throw new ArgumentNullException(nameof(x509Certificate2Options.CertificateContent));

        if (x509Certificate2Options.KeyContent is null)
        {
            return X509Certificate2.CreateFromPem(x509Certificate2Options.CertificateContent!);
        }
        else
        {
            return X509Certificate2.CreateFromPem(x509Certificate2Options.CertificateContent!, x509Certificate2Options.KeyContent!);
        }
    }
}
