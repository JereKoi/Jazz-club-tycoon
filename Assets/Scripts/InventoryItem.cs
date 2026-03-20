using UnityEngine;
using SQLite;

public class InventoryItem
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public bool isPlaced;
    public int levelRequired;
}
