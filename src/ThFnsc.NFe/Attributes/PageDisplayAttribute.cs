using System;

namespace ThFnsc.NFe.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class PageDisplayAttribute : Attribute
{
    public string OpenIcon { get; set; }

    public string Name { get; set; }

    public float Order { get; set; }
}
