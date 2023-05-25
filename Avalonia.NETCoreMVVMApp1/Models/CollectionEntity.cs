namespace Avalonia.NETCoreMVVMApp1.Models;

public class CollectionEntity
{
    public int Id { get; set; }
    public string? PhoneNum { get; set; }
    public string? Description { get; set; }
    public CollectionCategory? Category { get; set; }
    
}