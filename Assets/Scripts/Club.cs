using SQLite;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

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
    [Header("Club numbers")]
    [SerializeField] private float _cleanInterval = 20f;
    [SerializeField] private float _maxExperience = 1000f;
    [SerializeField] private float _timeSinceCleaned = 0;
    [SerializeField] private float _maxReputation = 1000f;
    [SerializeField] private float _maxDirtyness = 1000f;
    [SerializeField] private float _increaseReputationWhenClnd = 0.1f;
    [SerializeField] private float _increaseReputation = 0.5f;
    [SerializeField] private float _decreaseReputation = 0.2f;
    [SerializeField] private float _increaseExperience = 0.5f;
    [SerializeField] private float _increaseDirtyness = 0.2f;
    [SerializeField] private float _decreaseDirtyness = 0.2f;
    [SerializeField] private float _autoSaveInterval = 300f;
    [SerializeField] private int _timeSinceBeenCleaned = 0;

    public static event Action OnActivate;

    public static Club Instance { get; private set; }

    public static event Action<ClubData> OnClubDataChanged;

    // Event: Sends info that dirtyness has changed
    public static event Action<float> OnDirtynessChanged;

    private ClubData _data = new ClubData();
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _clubNeedsCleaningText;
    [SerializeField] private TextMeshProUGUI _dirtynessLevelText;
    [SerializeField] private GameObject _cleanButton;

    private Coroutine _autoSaveCoroutine;

    public bool hasBeenCleaned = false;
    private bool hasFocus = false;

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

        ApplyChanges();

        StartCoroutine(DirtynessTimer());
        _autoSaveCoroutine = StartCoroutine(AutoSaveRoutine());

    }

    // This writes what is on memory to database. This is called rarely, because it is slow process
    public void SaveToDatabase()
    {
        if (_data == null || DatabaseManager.Instance == null) return;

        DatabaseManager.Instance.SaveClub(_data);
        Debug.Log("<color=green>Game progress have been saved to database!</color>");
    }

    private IEnumerator AutoSaveRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(_autoSaveInterval);
            SaveToDatabase();
        }
    }

    // TODO: for this would need to do some timer stopper when cleaning has been done, for example
    //       stop coroutine for 240 seconds ( 4 minutes )
    private IEnumerator DirtynessTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(_timeSinceBeenCleaned);
            IncreaseDirtyness();
            Debug.Log("Dirtyness increased!");
            _timeSinceBeenCleaned = 0;
        }
    }

    private void ApplyChanges()
    {
        DatabaseManager.Instance.SaveClub(_data);
        Debug.Log("Saving club changes." + _data);
        OnClubDataChanged?.Invoke(_data);
    }

    private void OnApplicationQuit()
    {
        ApplyChanges();
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!hasFocus)
        {
            ApplyChanges();
        }
    }

    public void IncreaseReputation()
    {
        if (_data == null)
        {
            Debug.LogError("Club data was null. Cannot change reputation.");
            return;
        }
        _data.reputation = Mathf.Clamp(_data.reputation + _increaseReputation, 0f, _maxReputation);
        Debug.Log("Increased reputation. Reputaiton now: " + _data.reputation);
        if (hasBeenCleaned == true)
        {
            _data.reputation = Mathf.Clamp(_data.reputation + _increaseReputationWhenClnd, 0f, _maxReputation);
            Debug.Log("Increased reputation. Reputation now: " + _data.reputation);
            hasBeenCleaned = false;
        }
        ApplyChanges();
    }

    public void DecreaseReputation()
    {
        if (_data == null)
        {
            Debug.LogError("Club data was null. Cannot change reputation.");
            return;
        }

        _data.reputation = Mathf.Clamp(_data.reputation - _decreaseReputation, 0f, _maxReputation);
        Debug.Log("Decreased reputation. Reputation now: " + _data.reputation);
        ApplyChanges();
    }

    public void IncreaseExperience()
    {
        if (_data == null)
        {
            Debug.LogError("Club data was null. Cannot increase experience.");
            return;
        }

        _data.experience = Mathf.Clamp(_data.experience + _increaseExperience, 0f, _maxExperience);
        Debug.Log("Increased experience." + _data.experience);
        ApplyChanges();

        // If level up, save to database
        if (_data.experience >= 1000f)
        {
            _data.level++;
            _data.experience = 0f;
            SaveToDatabase();
        }

        ApplyChanges();
    }

    public void IncreaseDirtyness()
    {
        if (_data == null) return;

        _data.dirtyness = Mathf.Clamp(_data.dirtyness + 0.2f, 0f, _maxDirtyness);
        Debug.Log("Dirtyness of club increased!");

        // TODO: check how to increase floats on different script

        DatabaseManager.Instance.SaveClub(_data);

        // ? ensures that game won't crash if nobody is listening to event
        OnDirtynessChanged?.Invoke(_data.dirtyness);
        OnClubDataChanged?.Invoke(_data);

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

        _data.dirtyness = Mathf.Clamp(_data.reputation - _decreaseDirtyness, 0f, _maxReputation);
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

        OnDirtynessChanged?.Invoke(_data.dirtyness);
        SaveToDatabase();
    }
}