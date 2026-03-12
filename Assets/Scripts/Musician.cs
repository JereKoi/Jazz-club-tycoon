using SQLite;
using Unity.VisualScripting;
using UnityEngine;

public class Musician : MonoBehaviour
{
    [field: SerializeField]
    public string Name { get; set; }
    [field: SerializeField]
    public int Virtuosity { get; set; }
    [field: SerializeField]
    public int Charisma { get; set; }
    [PrimaryKey]
    [AutoIncrement]
    public int Id { get; set; }

    private void Start()
    {
        Name = "Francesca Smiles";
        Debug.Log("Start method");
        Debug.Log("Musician name is: " + Name);
    }

    public void IncreaseCharisma()
    {
        Virtuosity = Mathf.Clamp(Virtuosity + 1, 0, 10);
        Charisma = Mathf.Clamp(Virtuosity + 1, 0, 10);
        Debug.Log("Charisma increase!");
    }
}
