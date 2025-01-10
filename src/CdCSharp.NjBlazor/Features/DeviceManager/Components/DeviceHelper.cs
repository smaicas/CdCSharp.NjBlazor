namespace CdCSharp.NjBlazor.Features.DeviceManager.Components;

/// <summary>
/// Contains helper methods related to device operations.
/// </summary>
public class DeviceHelper
{
    /// <summary>
    /// Gets the value based on the device width.
    /// </summary>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <param name="deviceWidth">The width of the device.</param>
    /// <param name="valueMobile">The value for mobile devices.</param>
    /// <param name="valueTablet">The value for tablet devices.</param>
    /// <param name="valueDesktop">The value for desktop devices.</param>
    /// <param name="valueLargeDesktop">The value for large desktop devices.</param>
    /// <returns>The value based on the device width.</returns>
    private static TValue? GetByWidthCore<TValue>(
        int deviceWidth,
        TValue? valueMobile = null,
        TValue? valueTablet = null,
        TValue? valueDesktop = null,
        TValue? valueLargeDesktop = null
    )
        where TValue : struct
    {
        if (deviceWidth is > 576 and <= 768)
            return valueTablet ?? valueMobile;
        else if (deviceWidth is > 768 and <= 992)
            return valueDesktop ?? valueTablet ?? valueMobile;
        else if (deviceWidth > 992)
            return valueLargeDesktop ?? valueDesktop ?? valueTablet ?? valueMobile;
        return valueMobile;
    }

    /// <summary>
    /// Retrieves a value based on the device width category.
    /// </summary>
    /// <typeparam name="TValue">The type of value to retrieve.</typeparam>
    /// <param name="deviceWidth">The width of the device.</param>
    /// <param name="valueMobile">The value for mobile devices.</param>
    /// <param name="valueTablet">The value for tablet devices.</param>
    /// <param name="valueDesktop">The value for desktop devices.</param>
    /// <param name="valueLargeDesktop">The value for large desktop devices.</param>
    /// <returns>The appropriate value based on the device width category.</returns>
    private static TValue? GetByWidthCore<TValue>(
        int deviceWidth,
        TValue? valueMobile = null,
        TValue? valueTablet = null,
        TValue? valueDesktop = null,
        TValue? valueLargeDesktop = null
    )
        where TValue : class
    {
        Console.WriteLine(deviceWidth);
        if (deviceWidth is > 576 and <= 768)
            return valueTablet ?? valueMobile;
        else if (deviceWidth is > 768 and <= 992)
            return valueDesktop ?? valueTablet ?? valueMobile;
        else if (deviceWidth > 992)
            return valueLargeDesktop ?? valueDesktop ?? valueTablet ?? valueMobile;
        return valueMobile;
    }

    /// <summary>
    /// Gets the appropriate value based on the device width.
    /// </summary>
    /// <typeparam name="TValue">The type of value to return.</typeparam>
    /// <param name="deviceWidth">The width of the device.</param>
    /// <param name="valueMobile">The value for mobile devices.</param>
    /// <param name="valueTablet">The value for tablet devices (optional).</param>
    /// <param name="valueDesktop">The value for desktop devices (optional).</param>
    /// <param name="valueLargeDesktop">The value for large desktop devices (optional).</param>
    /// <returns>The appropriate value based on the device width.</returns>
    /// <remarks>If no specific value is provided for a
    public static TValue GetByWidth<TValue>(
        int deviceWidth,
        TValue valueMobile,
        TValue? valueTablet = null,
        TValue? valueDesktop = null,
        TValue? valueLargeDesktop = null
    )
        where TValue : struct
    {
        return GetByWidthCore(
                deviceWidth,
                valueMobile,
                valueTablet,
                valueDesktop,
                valueLargeDesktop
            ) ?? valueMobile;
    }

    /// <summary>
    /// Gets the value based on the device width.
    /// </summary>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <param name="deviceWidth">The width of the device.</param>
    /// <param name="valueMobile">The value for mobile devices.</param>
    /// <param name="valueTablet">The value for tablet devices (optional, defaults to mobile value).</param>
    /// <param name="valueDesktop">The value for desktop devices (optional, defaults to tablet value).</param>
    /// <param name="valueLargeDesktop">The value for large desktop devices (optional, defaults to desktop value).</param>
    /// <returns>The appropriate value based on the device width.</
    public static TValue? GetByWidth<TValue>(
        int deviceWidth,
        TValue? valueMobile,
        TValue? valueTablet = null,
        TValue? valueDesktop = null,
        TValue? valueLargeDesktop = null
    )
        where TValue : struct
    {
        return GetByWidthCore(
            deviceWidth,
            valueMobile,
            valueTablet ?? valueMobile,
            valueDesktop ?? valueTablet ?? valueMobile,
            valueLargeDesktop ?? valueDesktop ?? valueTablet ?? valueMobile
        );
    }

    /// <summary>
    /// Gets a value based on the device width.
    /// </summary>
    /// <typeparam name="TValue">The type of value to retrieve.</typeparam>
    /// <param name="deviceWidth">The width of the device.</param>
    /// <param name="valueMobile">The value for mobile devices.</param>
    /// <param name="valueTablet">The value for tablet devices.</param>
    /// <param name="valueDesktop">The value for desktop devices.</param>
    /// <param name="valueLargeDesktop">The value for large desktop devices.</param>
    /// <returns>The appropriate value based on the device width.</returns>
    public static TValue GetByWidth<TValue>(
        int deviceWidth,
        TValue valueMobile,
        TValue? valueTablet = null,
        TValue? valueDesktop = null,
        TValue? valueLargeDesktop = null
    )
        where TValue : class
    {
        return GetByWidthCore(
                deviceWidth,
                valueMobile,
                valueTablet,
                valueDesktop,
                valueLargeDesktop
            ) ?? valueMobile;
    }
}
