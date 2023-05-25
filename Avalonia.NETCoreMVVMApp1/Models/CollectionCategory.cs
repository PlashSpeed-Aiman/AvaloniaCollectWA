namespace Avalonia.NETCoreMVVMApp1.Models;

public class CollectionCategory
{
    public int Id { get; set; }
    public int CollectionEntityId { get; set; }
    public string Category { get; set; }
    public CollectionEntity Entity { get; set; } = null!;
}