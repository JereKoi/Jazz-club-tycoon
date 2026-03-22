using SQLite;
using System.Xml.Linq;
using TMPro;
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
    public bool isHired;
    public float dailyWage;
    private MusicianData _data = new MusicianData();
    [SerializeField] private TextMeshProUGUI _nameText;


    private void Start()
    {
        if (_nameText == null)
        {
            Debug.LogError("Remember to attach name to TextMeshPro");
            return; // return so game won't crash
        }
        else
        {
            _data = DatabaseManager.Instance.LoadMusician(_data.Id);
        }
        if (_data == null)
        {
            _data = new MusicianData { Name = "MusicianData" };
        }
        _data.Name = "Francesca Smiles";
        Debug.Log("Start method");
        Debug.Log("Musician name is: " + _data.Name);
        UpdateUI();
    }

    public void CreateMusicican(int Id)
    {
        Debug.Log("Musician was created succesfully.");
    }

    public void SelectMusicican()
    {
        Debug.Log("Musician was selected: " + _data.Name);
        isHired = true;
    }

    private void ApplyChanges()
    {
        DatabaseManager.Instance.SaveMusician(_data);
        UpdateUI();
        Debug.Log("Musician changes were updated succesfully.");
    }

    public void IncreaseCharisma()
    {
        if (_data == null) _data = new MusicianData { Name = "New Musicican" };
        _data.Charisma = Mathf.Clamp(_data.Charisma + 1, 0, 10);
        ApplyChanges();
        Debug.Log("Charisma increase!");
    }

    public void IncreaseVirtuosity()
    {
        if (_data == null) _data = new MusicianData { Name = "New Musicican" };
        _data.Virtuosity = Mathf.Clamp(_data.Virtuosity + 1, 0, 10);
        ApplyChanges();
        Debug.Log("Virtuosity increase!");
    }

    public void UpdateUI()
    {
        _nameText.text = "Name: " + _data.Name;
    }
}