namespace Atoms.Discoveries.Database.API.Data.Models;

public class PaginationParameters
{
    /// <summary>
    /// Index of first item to take
    /// </summary>
    public uint Offset { get; set; } = 0;
    /// <summary>
    /// Number of items to take
    /// </summary>
    public uint Limit { get; set; } = int.MaxValue;
}
