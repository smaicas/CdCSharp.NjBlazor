namespace CdCSharp.NjBlazor.Core.Abstractions.Components;

/// <summary>
/// Represents a class for handling events related to Nj.
/// </summary>
public class NjEvents
{
    /// <summary>
    /// Creates a debounced action that will execute the provided action with the specified interval.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the argument for the action.
    /// </typeparam>
    /// <param name="action">
    /// The action to be debounced.
    /// </param>
    /// <param name="interval">
    /// The interval to wait before executing the action.
    /// </param>
    /// <returns>
    /// A debounced action that will execute the provided action with the specified interval.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when the action is null.
    /// </exception>
    public static Action<T> Debounce<T>(Action<T> action, TimeSpan interval)
    {
        if (action == null) throw new ArgumentNullException(nameof(action));

        int last = 0;
        return arg =>
        {
            int current = Interlocked.Increment(ref last);
            Task.Delay(interval).ContinueWith(task =>
            {
                if (current == last)
                    action(arg);
            });
        };
    }

    /// <summary>
    /// Creates a throttled action that limits the rate at which the provided action is invoked.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the argument for the action.
    /// </typeparam>
    /// <param name="action">
    /// The action to be throttled.
    /// </param>
    /// <param name="interval">
    /// The time interval to wait between invocations of the action.
    /// </param>
    /// <returns>
    /// A throttled action that can be invoked with an argument of type T.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when the provided action is null.
    /// </exception>
    public static Action<T> Throttle<T>(Action<T> action, TimeSpan interval)
    {
        if (action == null) throw new ArgumentNullException(nameof(action));

        Task task = null;
        object l = new();
        T args = default;
        return (arg) =>
        {
            args = arg;
            if (task != null)
                return;

            lock (l)
            {
                if (task != null)
                    return;

                task = Task.Delay(interval).ContinueWith(t =>
                {
                    action(args);
                    task = null;
                });
            }
        };
    }
}