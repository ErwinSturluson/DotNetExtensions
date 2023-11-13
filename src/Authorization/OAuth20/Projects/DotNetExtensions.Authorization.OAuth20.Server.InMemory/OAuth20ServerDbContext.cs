// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Domain;
using Microsoft.EntityFrameworkCore;

namespace DotNetExtensions.Authorization.OAuth20.Server.InMemory;

public class OAuth20ServerDbContext : DbContext
{
    public OAuth20ServerDbContext(DbContextOptions<OAuth20ServerDbContext> options)
        : base(options)
    {
    }

    public DbSet<Client> Clients { get; set; }

    public DbSet<ClientFlow> ClientFlows { get; set; }

    public DbSet<ClientProfile> ClientProfiles { get; set; }

    public DbSet<ClientRedirectionEndpoint> ClientRedirectionEndpoints { get; set; }

    public DbSet<ClientScope> ClientScopes { get; set; }

    public DbSet<ClientSecret> ClientSecrets { get; set; }

    public DbSet<ClientSecretType> ClientSecretTypes { get; set; }

    public DbSet<ClientType> ClientTypes { get; set; }

    public DbSet<EndUser> EndUsers { get; set; }

    public DbSet<EndUserClientScope> EndUserClientScopes { get; set; }

    public DbSet<EndUserInfo> EndUserInfos { get; set; }

    public DbSet<Flow> Flows { get; set; }

    public DbSet<Resource> Resources { get; set; }

    public DbSet<ResourceSigningCredentialsAlgorithm> ResourceSigningCredentialsAlgorithms { get; set; }

    public DbSet<Scope> Scopes { get; set; }

    public DbSet<SigningCredentialsAlgorithm> SigningCredentialsAlgorithms { get; set; }

    public DbSet<TokenAdditionalParameter> TokenAdditionalParameters { get; set; }

    public DbSet<TokenType> TokenTypes { get; set; }

    public DbSet<TokenTypeTokenAdditionalParameter> TokenTypeTokenAdditionalParameters { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EndUser>()
            .HasOne(x => x.EndUserInfo)
            .WithOne(x => x.EndUser)
            .HasForeignKey<EndUser>(x => x.EndUserInfoId);
    }
}
