namespace NetObserver.Model
{
    /// <summary>
    /// Enum status port.
    /// </summary>
#pragma warning disable CA1008 // Перечисления должны иметь нулевое значение
    public enum PortStatus
#pragma warning restore CA1008 // Перечисления должны иметь нулевое значение
    {
        /// <summary>
        /// Port in open.
        /// </summary>
        Open = 1,
        /// <summary>
        /// Port in closed.
        /// </summary>
        Closed,
    }
}
