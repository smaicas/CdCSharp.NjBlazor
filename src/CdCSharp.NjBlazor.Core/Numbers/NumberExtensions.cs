﻿namespace CdCSharp.NjBlazor.Core.Numbers;

public static class NumberExtensions
{
    public static double EnsureRange(this double input, double max) => input.EnsureRange(0.0, max);

    public static double EnsureRange(this double input, double min, double max) => Math.Max(min, Math.Min(max, input));

    public static byte EnsureRange(this byte input, byte max) => input.EnsureRange((byte)0, max);

    public static byte EnsureRange(this byte input, byte min, byte max) => Math.Max(min, Math.Min(max, input));

    public static int EnsureRange(this int input, int max) => input.EnsureRange(0, max);

    public static int EnsureRange(this int input, int min, int max) => Math.Max(min, Math.Min(max, input));

    public static byte EnsureRangeToByte(this int input) => (byte)input.EnsureRange(0, 255);
}