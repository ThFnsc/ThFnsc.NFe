using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Rendering;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace ThFnsc.NFe.Shared;

public class InputJson : InputBase<string>
{
    private bool _needsClearing;

    [DisallowNull] public ElementReference? Element { get; protected set; }

    [Parameter] public Type ModelType { get; set; }

    [Parameter]
    public JsonSerializerOptions JsonSerializerOptions { get; set; } = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true,
        WriteIndented = true,
        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        AllowTrailingCommas = true
    };

    /// <inheritdoc />
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        base.BuildRenderTree(builder);
        if (string.IsNullOrWhiteSpace(CurrentValue))
            Value = JsonSerializer.Serialize(Activator.CreateInstance(ModelType), JsonSerializerOptions);
        builder.OpenElement(0, "textarea");
        builder.AddMultipleAttributes(1, AdditionalAttributes);
        builder.AddAttribute(2, "class", CssClass);
        builder.AddAttribute(3, "rows", 10);
        builder.AddAttribute(4, "style", "font-family:monospace;");
        builder.AddAttribute(5, "value", _needsClearing ? "" : BindConverter.FormatValue(CurrentValueAsString));
        builder.AddAttribute(6, "onchange", EventCallback.Factory.CreateBinder<string>(this, __value => CurrentValueAsString = __value, CurrentValueAsString));
        builder.AddElementReferenceCapture(7, __inputReference => Element = __inputReference);
        builder.CloseElement();
        if (_needsClearing)
        {
            _needsClearing = false;
            StateHasChanged();
        }
    }

    /// <inheritdoc />
    protected override bool TryParseValueFromString(string value, out string result, [NotNullWhen(false)] out string validationErrorMessage)
    {
        try
        {
            var asObj = JsonSerializer.Deserialize(value, ModelType, JsonSerializerOptions);
            result = JsonSerializer.Serialize(asObj, ModelType, JsonSerializerOptions);
            if (value != result)
            {
                _needsClearing = true;
                StateHasChanged();
            }
            validationErrorMessage = null;
            return true;
        }
        catch (Exception e)
        {
            result = default;
            validationErrorMessage = $"Could not parse and restringify JSON: ${e.Message}";
            return false;
        }
    }
}
