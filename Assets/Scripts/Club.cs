using UnityEngine;
using SQLite;
using NUnit.Framework.Internal.Commands;
using TMPro;

public class ClubData
{
    public int level;
    public int maxAudience;
    public float experience;
    public float reputation;
    public string name;


    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
}

public class Club : MonoBehaviour
{
    private ClubData _data = new ClubData();
    [SerializeField] private TextMeshProUGUI _nameText;

    private void Start()
    {
        _data = DatabaseManager.Instance.LoadClub(GameManager.Instance.currentClubId);
        Debug.Log("Club was loaded succesfully: " + GameManager.Instance.currentClubId);
        _data.name = "Jazz club";
        if (_data == null)
        {
            _data = new ClubData();
            _data = DatabaseManager.Instance.CreateClub(name);
            GameManager.Instance.currentClubId = _data.Id;
            _data.name = "Jazz club";
            Debug.Log("Club was not found on database, created and loaded a new one: " + _data.name + _data.Id);
        }
        UpdateUI();
    }

    private void ApplyChanges()
    {
        DatabaseManager.Instance.SaveClub(_data);
        Debug.Log("Saving club changes." + _data);
        UpdateUI();
    }

    public void IncreaseReputation()
    {

        _data.reputation = Mathf.Clamp(_data.reputation + 0.5f, 0f, 1f);
        Debug.Log("Increased reputation." + _data.reputation);
        ApplyChanges();
    }

    public void DecreaseReputation()
    {

        Debug.Log("Decreased reputation.");
    }

    public void IncreaseExperience()
    {
        _data.experience = Mathf.Clamp(_data.experience + +0.5f, 0f, 1000f);
        Debug.Log("Increased experience." + _data.experience);
    }

    public void UpdateUI()
    {
        _nameText.text = "Name: " + _data.name;
    }
}