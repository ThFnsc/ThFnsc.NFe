using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ThFnsc.NFe.Extensions;

public static class AttributeProviderExtensions
{
    public static string GetDisplayName(this ICustomAttributeProvider attrProvider) =>
        attrProvider.GetCustomAttributes(false)
            .OfType<DisplayAttribute>()
            .FirstOrDefault()?.Name
            ?? attrProvider
            .GetType()
            .GetProperty(nameof(Type.Name))?
            .GetValue(attrProvider)
            .ToString();
}
