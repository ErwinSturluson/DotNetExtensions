namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions;

public class ValidationResult
{
    public bool Success { get; set; }

    public string? Status { get; set; }

    public string? Description { get; set; }
}
