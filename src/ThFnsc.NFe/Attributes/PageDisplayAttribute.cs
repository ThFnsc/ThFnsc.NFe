using System;

namespace ThFnsc.NFe.Attributes
{
    public class PageDisplayAttribute : Attribute
    {
        public string OpenIcon { get; set; }

        public string Name { get; set; }

        public float Order { get; set; }
    }
}
