using UnityEngine;
using SQLite;

public class InventoryItemData
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public bool isPlaced;
    public int levelRequired;

    public class InventoryItem
    {

    }
}
