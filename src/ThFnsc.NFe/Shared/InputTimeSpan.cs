using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Rendering;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace ThFnsc.NFe.Shared
{
    public class InputTimeSpan : InputBase<TimeSpan>
    {
        public static string DefaultFormat { get; } = @"d\d\:h\h\:m\m\:s\s";

        [DisallowNull] public ElementReference? Element { get; protected set; }

        [Parameter] public string Format { get; set; } = DefaultFormat;

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenElement(0, "input");
            builder.AddMultipleAttributes(1, AdditionalAttributes);
            builder.AddAttribute(2, "type", "text");
            builder.AddAttribute(3, "class", CssClass);
            builder.AddAttribute(4, "value", BindConverter.FormatValue(CurrentValueAsString));
            builder.AddAttribute(5, "onchange", EventCallback.Factory.CreateBinder<string>(this, __value => CurrentValueAsString = __value, CurrentValueAsString));
            builder.AddElementReferenceCapture(6, __inputReference => Element = __inputReference);
            builder.CloseElement();
        }

        protected override bool TryParseValueFromString(string value, [MaybeNullWhen(false)] out TimeSpan result, [NotNullWhen(false)] out string validationErrorMessage)
        {
            if (TimeSpan.TryParseExact(value, Format, CultureInfo.InvariantCulture, out result))
            {
                validationErrorMessage = null;
                return true;
            }
            validationErrorMessage = "Invalid TimeSpan";
            return false;
        }

        protected override string FormatValueAsString(TimeSpan value) =>
            value.ToString(Format, CultureInfo.InvariantCulture);
    }
}
