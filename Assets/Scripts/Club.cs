using UnityEngine;
using SQLite;
using NUnit.Framework.Internal.Commands;
using TMPro;

public class ClubData
{
    public int level;
    public int id;
    public int maxAudience;
    public float experience;
    public float reputation;



    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Name { get; set; }
}

public class Club : MonoBehaviour
{
    public int Id { get; set; }
    private ClubData _data = new ClubData();
    [SerializeField] private TextMeshProUGUI _nameText;

    private void Start()
    {

        if (_data == null)
        {
            _data = new ClubData();
            DatabaseManager.Instance.CreateClub(name);
            DatabaseManager.Instance.LoadClub(Id);
        }
    }

    private void ApplyChanges()
    {
        DatabaseManager.Instance.SaveClub(_data);
        UpdateUI();
    }

    public void IncreaseReputation()
    {
        _data.reputation = Mathf.Clamp(_data.reputation, 0f, 1f);
        ApplyChanges();
    }

    public void DecreaseReputation()
    {

    }

    public void IncreaseExperience()
    {

    }

    public void UpdateUI()
    {
        _nameText.text = "Name: " + _data.Name;
    }
}