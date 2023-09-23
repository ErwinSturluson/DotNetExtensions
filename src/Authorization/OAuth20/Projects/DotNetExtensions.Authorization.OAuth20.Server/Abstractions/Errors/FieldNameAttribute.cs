// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Errors;

[AttributeUsage(AttributeTargets.Field)]
public class FieldNameAttribute : Attribute
{
    public FieldNameAttribute(string name)
    {
        Name = name;
    }

    public string Name { get; }
}
