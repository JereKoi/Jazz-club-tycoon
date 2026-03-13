using SQLite;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;


public class MusicianData
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Name { get; set; }
    public int Virtuosity { get; set; }
    public int Charisma { get; set; }
}

public class Musician : MonoBehaviour
{
    public int Id { get; set; }
    private MusicianData _data = new MusicianData();

    private void Start()
    {
        _data = DatabaseManager.Instance.LoadMusician(1);
        if (_data == null )
        {
            _data = new MusicianData { Name = "New musician" };
        }
        _data.Name = "Francesca Smiles";
        Debug.Log("Start method");
        Debug.Log("Musician name is: " + _data.Name);
    }

    public void IncreaseCharisma()
    {
        _data.Virtuosity = Mathf.Clamp(_data.Virtuosity + 1, 0, 10);
        _data.Charisma = Mathf.Clamp(_data.Virtuosity + 1, 0, 10);
        Debug.Log("Charisma increase!");
    }


}