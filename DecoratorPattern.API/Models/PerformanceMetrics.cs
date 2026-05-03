namespace DecoratorPattern.API.Models;

/// <summary>
/// Contains performance metrics for operations
/// </summary>
public class PerformanceMetrics
{
    /// <summary>
    /// Operation name
    /// </summary>
    public string OperationName { get; set; } = string.Empty;

    /// <summary>
    /// Execution time in milliseconds
    /// </summary>
    public long ExecutionTimeMs { get; set; }

    /// <summary>
    /// Memory used in bytes
    /// </summary>
    public long MemoryUsedBytes { get; set; }

    /// <summary>
    /// Timestamp when operation started
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// Timestamp when operation ended
    /// </summary>
    public DateTime EndTime { get; set; }

    /// <summary>
    /// Whether operation was cached
    /// </summary>
    public bool WasCached { get; set; }

    /// <summary>
    /// Additional details
    /// </summary>
    public Dictionary<string, string>? Details { get; set; }
}
