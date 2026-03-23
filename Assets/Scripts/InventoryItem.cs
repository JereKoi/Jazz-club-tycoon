using UnityEngine;
using SQLite;
using Unity.VisualScripting;

public class InventoryItemData
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public bool isPlaced;
    public int levelRequired;

    public class InventoryItem
    {
        private void Start()
        {
            Debug.Log("Created a new InventoryItem table");
            DatabaseManager.Instance.LoadInventoryItem(GameManager.Instance.currentInventoryItemId);
        }
    }
}
