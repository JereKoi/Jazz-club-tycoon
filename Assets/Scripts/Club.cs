using UnityEngine;
using SQLite;
using NUnit.Framework.Internal.Commands;
using TMPro;
using System;

public class ClubData
{
    public int level;
    public int maxAudience;
    public float experience;
    public float reputation;
    public string name;
    public float dirtyness = 0f;



    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
}

public class Club : MonoBehaviour
{
    public static event Action OnActivate;

    public static Club Instance { get; private set; }
    private ClubData _data = new ClubData();
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _clubNeedsCleaningText;
    [SerializeField] private GameObject _cleanButton;
    
    public bool hasBeenCleaned = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
             DontDestroyOnLoad(gameObject);
            Debug.Log("Club instance was null, now instance = this");
        }
        else if (Instance != this)
        {
           // Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Club.Instance == null) return;
    }

    private void Start()
    {
        if (GameManager.Instance == null)
        {
            Debug.LogError("Club didnt find game manager instance! Make sure its on scene and its wake has been ran.");
        }

        int clubId = GameManager.Instance.currentClubId;
        Debug.Log("Trying to load club with id" + clubId);

        if (DatabaseManager.Instance == null)
        {
            Debug.LogError("DatabaseManager.Instacne is null!");
        }

        ClubData loadedData = null;
        try
        {
            loadedData = DatabaseManager.Instance.LoadClub(clubId);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Database retrieval failed! Is connection lost? Error: " + e);
        }

       

        if (loadedData == null)
        {

            _data = DatabaseManager.Instance.CreateClub(name);
            GameManager.Instance.currentClubId = _data.Id;
            _data.name = "Jazz club";
            Debug.Log("Club was not found, loaded new one: " + _data.name);
        }
        else
        {
            _data = loadedData;
            _data.name = "Jazz club";
            Debug.Log("Club loaded succesfully with id: " + GameManager.Instance.currentClubId);
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
        if (_data == null)
        {
            Debug.LogError("Club data was null. Cannot change reputation.");
        }
        _data.reputation = Mathf.Clamp(_data.reputation + 0.5f, 0f, 1f);
        Debug.Log("Increased reputation. Reputaiton now: " + _data.reputation);
        if (hasBeenCleaned == true)
        {
            _data.reputation = Mathf.Clamp(_data.reputation + 0.1f, 0f, 1f);
            Debug.Log("Increased reputation. Reputaiton now: " + _data.reputation);
            hasBeenCleaned = false;
        }
        ApplyChanges();
    }

    public void DecreaseReputation()
    {
        if (_data == null)
        {
            Debug.LogError("Club data was null. Cannot change reputation.");
        }

        _data.reputation = Mathf.Clamp(_data.reputation - 0.2f, 0f, 1f);
        Debug.Log("Decreased reputation. Reputation now: " + _data.reputation);
    }

    public void IncreaseExperience()
    {
        _data.experience = Mathf.Clamp(_data.experience + +0.5f, 0f, 1000f);
        Debug.Log("Increased experience." + _data.experience);
        ApplyChanges();
    }

    public void IncreaseDirtyness()
    {
        if (_data == null) return;

        _data.dirtyness = Mathf.Clamp(_data.reputation + 0.2f, 0f, 1f);
        Debug.Log("Dirtyness of club increased!");

        // TODO: check how to increase floats on different script

        if (_data.dirtyness >= 50f)
        {
            hasBeenCleaned = false;
            _cleanButton.SetActive(true);
        }
        ApplyChanges();
    }

    public void DecreaseDirtyness()
    {
        if (_data == null) return;

        _data.dirtyness = Mathf.Clamp(_data.reputation - 0.2f, 0f, 1f);
        Debug.Log("Nice job at cleaning! Dirtyness of club Decreased! Slight increase in reputation.");
        if (_data.dirtyness <= 10)
        hasBeenCleaned = true;
        ApplyChanges();
    }

    public void ResetDirtyness()
    {
        if (_data == null) return;

        _data.dirtyness = 0f;
        Debug.Log("Club has been fully cleaned.");
        hasBeenCleaned = true;
        ApplyChanges();
        _cleanButton.SetActive(false);
    }

    public void UpdateUI()
    {
        if (_data == null) return;
        _nameText.text = "Name: " + _data.name;

        int dirtyPercentage = Mathf.RoundToInt(_data.dirtyness * 100f);

        if (_clubNeedsCleaningText != null)
        {
            _clubNeedsCleaningText.text = "Dirtyness: " + dirtyPercentage + "%";
        }
    }
}