using CdCSharp.NjBlazor.Core.Abstractions.Components;
using CdCSharp.NjBlazor.Features.Dom.Abstractions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace CdCSharp.NjBlazor.Features.Forms.Date;

/// <summary>
/// Base component to create a date selector.
/// </summary>
/// <typeparam name="TValue">
/// </typeparam>
public abstract class NjInputDateBase<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] TValue> : NjInputBase<TValue>
{
    protected const string DateFormat = "yyyy-MM-dd";

    /// <summary>
    /// The DateTimeLocal format. Compatible with HTML 'date' inputs. Input type date uses ISO 8601
    /// to be culture invariant.
    /// </summary>
    protected const string DateTimeLocalFormat = "yyyy-MM-dd HH:mm:ss";

    /// <summary>
    /// The Month format. Compatible with HTML 'datetime-local' inputs. Input type datetime-local
    /// uses ISO 8601 to be culture invariant.
    /// </summary>
    protected const string MonthFormat = "yyyy-MM";

    /// <summary>
    /// The Time format. Compatible with HTML 'month' inputs Input type month uses ISO 8601 to be
    /// culture invariant.
    /// </summary>
    protected const string TimeFormat = "HH:mm:ss";

    /// <summary>
    /// The calendar icon html element reference
    /// </summary>
    protected ElementReference? _calendarIconRef;

    /// <summary>
    /// The root element reference
    /// </summary>
    protected string? _inputReferenceId;

    /// <summary>
    /// The value of the type attribute.
    /// </summary>
    protected string _typeAttributeValue = default!;

    private string _format = default!;

    private string _parsingErrorMessage = default!;

    /// <summary>
    /// Constructs an instance of <see cref="NjInputDateBase{TValue}" />
    /// </summary>
    public NjInputDateBase()
    {
        Type type = Nullable.GetUnderlyingType(typeof(TValue)) ?? typeof(TValue);

        if (type != typeof(DateTime) &&
            type != typeof(DateTimeOffset) &&
            type != typeof(DateOnly) &&
            type != typeof(TimeOnly))
            throw new InvalidOperationException($"Unsupported {GetType()} type param '{type}'.");
    }

    /// <summary>
    /// Gets or sets the display format for the parameter.
    /// </summary>
    /// <value>
    /// The display format for the parameter.
    /// </value>
    [Parameter] public string? DisplayFormat { get; set; }

    /// <summary>
    /// Gets or sets the parsing error message.
    /// </summary>
    /// <value>
    /// The parsing error message.
    /// </value>
    [Parameter] public string ParsingErrorMessage { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the type of input for the date.
    /// </summary>
    /// <value>
    /// The type of input for the date.
    /// </value>
    [Parameter] public InputDateType Type { get; set; } = InputDateType.Date;

    protected Dictionary<int, Func<string, bool>> _partialValidations { get; set; } = [];

    protected string? CurrentValueAsStringWrapper
    {
        get => CurrentValueAsString;
        set
        {
            if (value != CurrentValueAsString)
            {
                CurrentValueAsString = value;
                if (value != null)
                {
                    OnStringValueChanged(value).Wait();
                }
            }
        }
    }

    /// <summary>
    /// Gets or sets the DOM JavaScript Interop service.
    /// </summary>
    /// <value>
    /// The DOM JavaScript Interop service.
    /// </value>
    [Inject] protected IDOMJsInterop DomJs { get; set; } = default!;

    /// <summary>
    /// Formats a nullable value as a string.
    /// </summary>
    /// <param name="value">
    /// The nullable value to format.
    /// </param>
    /// <returns>
    /// The formatted string representation of the value.
    /// </returns>
    protected override string FormatValueAsString(TValue? value)
        => value switch
        {
            DateTime dateTimeValue => BindConverter.FormatValue(dateTimeValue, _format, CultureInfo.InvariantCulture),
            DateTimeOffset dateTimeOffsetValue => BindConverter.FormatValue(dateTimeOffsetValue, _format, CultureInfo.InvariantCulture),
            DateOnly dateOnlyValue => BindConverter.FormatValue(dateOnlyValue, _format, CultureInfo.InvariantCulture),
            TimeOnly timeOnlyValue => BindConverter.FormatValue(timeOnlyValue, _format, CultureInfo.InvariantCulture),
            _ => string.Empty, // Handles null for Nullable<DateTime>, etc.
        };

    /// <summary>
    /// Get the default text based on the input type.
    /// </summary>
    /// <returns>
    /// The default text based on the input type.
    /// </returns>
    protected string GetDefaultText()
    {
        return Type switch
        {
            InputDateType.DateTimeLocal => DateTimeLocalFormat,
            InputDateType.Month => MonthFormat,
            InputDateType.Time => TimeFormat,
            _ => DateFormat,
        };
    }

    /// <summary>
    /// Gets the formatted text based on the input date type.
    /// </summary>
    /// <returns>
    /// The formatted text based on the input date type.
    /// </returns>
    protected string GetFormatTextFormat()
    {
        return Type switch
        {
            InputDateType.DateTimeLocal => BindConverter.FormatValue(DateTimeOffset.Now, DateTimeLocalFormat, CultureInfo.InvariantCulture),
            InputDateType.Month => BindConverter.FormatValue(DateOnly.MinValue, MonthFormat, CultureInfo.InvariantCulture),
            InputDateType.Time => BindConverter.FormatValue(TimeOnly.MinValue, TimeFormat, CultureInfo.InvariantCulture),
            _ => BindConverter.FormatValue(DateTime.Now, DateFormat, CultureInfo.InvariantCulture),
        };
    }

    /// <summary>
    /// Method called after the component has been rendered.
    /// </summary>
    /// <param name="firstRender">
    /// A boolean value indicating if this is the first render of the component.
    /// </param>
    /// <remarks>
    /// If it is the first render, and the InputReference is not null, the _inputReferenceId is set
    /// to the Id of the InputReference and the component's state is updated.
    /// </remarks>
    protected override void OnAfterRender(bool firstRender)
    {
        base.OnAfterRender(firstRender);
        if (firstRender)
        {
            if (InputReference != null)
            {
                _inputReferenceId = InputReference.Value.Id;
                StateHasChanged();
            }
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);
        if (firstRender)
        {
            if (_calendarIconRef != null && InputReference != null)
            {
                await DomJs.AddShowPickerEventHandler((ElementReference)_calendarIconRef, (ElementReference)InputReference);
            }
        }
    }

    /// <summary>
    /// Updates the current value as a string based on the provided change event arguments.
    /// </summary>
    /// <param name="args">
    /// The change event arguments containing the new value.
    /// </param>
    /// <returns>
    /// A task representing the asynchronous operation.
    /// </returns>
    protected Task OnChangeAsync(ChangeEventArgs args)
    {
        CurrentValueAsString = args.Value?.ToString();
        return Task.CompletedTask;
    }

    /// <summary>
    /// Asynchronously handles the focus event.
    /// </summary>
    /// <param name="focusEventArgs">
    /// The focus event arguments.
    /// </param>
    /// <returns>
    /// A task representing the asynchronous operation.
    /// </returns>
    protected override async Task OnFocusAsync(FocusEventArgs? focusEventArgs)
    {
        if (ReadOnly) return;
        await base.OnFocusAsync(focusEventArgs);
    }

    /// <summary>
    /// Sets the parameters based on the specified input type.
    /// </summary>
    /// <remarks>
    /// This method determines the type attribute value, format, and format description based on the
    /// input type.
    /// </remarks>
    /// <exception cref="InvalidOperationException">
    /// Thrown when an unsupported InputDateType is encountered.
    /// </exception>
    protected override void OnParametersSet()
    {
        (_typeAttributeValue, _format, string? formatDescription) = Type switch
        {
            InputDateType.Date => ("date", DateFormat, "date"),
            InputDateType.DateTimeLocal => ("datetime-local", DateTimeLocalFormat, "date and time"),
            InputDateType.Month => ("month", MonthFormat, "year and month"),
            InputDateType.Time => ("time", TimeFormat, "time"),
            _ => throw new InvalidOperationException($"Unsupported {nameof(InputDateType)} '{Type}'.")
        };

        _parsingErrorMessage = string.IsNullOrEmpty(ParsingErrorMessage)
            ? $"The {{0}} field must be a {formatDescription}."
            : ParsingErrorMessage;

        // Year
        _partialValidations.TryAdd(0, (text) =>
        {
            if (int.TryParse(text, out int parsedInt))
            {
                return parsedInt is > 0 and <= 9999;
            }
            else { return false; }
        });
        // Month
        _partialValidations.TryAdd(2, (text) =>
        {
            if (int.TryParse(text, out int parsedInt))
            {
                return parsedInt is > 0 and <= 12;
            }
            else { return false; }
        });
        //Day
        _partialValidations.TryAdd(4, (text) =>
        {
            if (int.TryParse(text, out int parsedInt))
            {
                return parsedInt is > 0 and <= 31;
            }
            else { return false; }
        });
    }

    protected async Task OnStringValueChanged(string stringValue)
    {
        if (InputReference == null) { return; }
        await DomJs.SetCalendarDatepickerValue((ElementReference)InputReference, stringValue);
    }

    /// <summary>
    /// Tries to parse a value from a string representation.
    /// </summary>
    /// <param name="value">
    /// The string value to parse.
    /// </param>
    /// <param name="result">
    /// When this method returns, contains the parsed value if the parsing succeeded, otherwise the
    /// default value for the type.
    /// </param>
    /// <param name="validationErrorMessage">
    /// When this method returns, contains an error message if the parsing failed, otherwise null.
    /// </param>
    /// <returns>
    /// True if the parsing was successful; otherwise, false.
    /// </returns>
    protected override bool TryParseValueFromString(string? value, [MaybeNullWhen(false)] out TValue result, [NotNullWhen(false)] out string? validationErrorMessage)
    {
        if (BindConverter.TryConvertTo(value, CultureInfo.InvariantCulture, out result))
        {
            Debug.Assert(result != null);
            validationErrorMessage = null;
            return true;
        }
        else
        {
            validationErrorMessage = string.Format(CultureInfo.InvariantCulture, _parsingErrorMessage, DisplayName ?? FieldIdentifier.FieldName);
            return false;
        }
    }
}