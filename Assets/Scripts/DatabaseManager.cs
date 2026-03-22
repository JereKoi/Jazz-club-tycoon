using UnityEngine;
using SQLite;
using System.Linq.Expressions;
using System.Linq;
using System;
using Unity.VisualScripting;

public sealed class DatabaseManager : IDisposable
{
    private static DatabaseManager _instance;
    private SQLiteConnection _connection;
    string databasePath = System.IO.Path.Combine(Application.persistentDataPath, "JazzClub.db");
    public MusicianData musicianData;
    public ClubData clubData;
    public InventoryItemData inventoryItemData;

    private void InitializeDatabase()
    {
        _connection = new SQLiteConnection(databasePath);
        Debug.Log("Connection exists, connected succesfully: " + databasePath);


        _connection.CreateTable<ClubData>();
        Debug.Log("Created a new Club table");
        LoadClub(GameManager.Instance.currentClubId);
        Debug.Log("Loaded club: " + GameManager.Instance.currentClubId);
        _connection.CreateTable<MusicianData>();
        Debug.Log("Created a new musician table");
        LoadMusician(GameManager.Instance.currentMusicianId);
        Debug.Log("Loaded club: " + GameManager.Instance.currentMusicianId);
        _connection.CreateTable<InventoryItemData>();
        Debug.Log("Created a new InventoryItem table");
        LoadInventoryItem(GameManager.Instance.currentInventoryItemId);
        Debug.Log("Loaded InventoryItem: " + GameManager.Instance.currentInventoryItemId);
    }

    public static DatabaseManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new DatabaseManager();
                _instance.InitializeDatabase();
            }
            return _instance;
        }
    }

    public MusicianData LoadMusician(int id)
    {
        return _connection.Find<MusicianData>(id);
    }

    public InventoryItemData LoadInventoryItem(int id)
    {
        return _connection.Find<InventoryItemData>(id);
    }

    public void SaveMusician(MusicianData data)
    {
        _connection.InsertOrReplace(data);
    }

    public MusicianData CreateMusician(string name)
    {
        var newMusician = new MusicianData { Name = name, Virtuosity = 0, Charisma = 0 };
        _connection.Insert(newMusician);
        return newMusician;
    }


    public void CloseConnection()
    {
        if (_connection != null)
        {
            _connection.Close();
            _connection = null;
            Debug.Log("Database connection closed.");
        }
    }
    public void Dispose()
    {
        CloseConnection();
    }

    public ClubData CreateClub(string name)
    {
        var newClub = new ClubData { name = name };
        _connection.Insert(newClub);
        return newClub;
    }

    public void SaveClub(ClubData data)
    {
        _connection.InsertOrReplace(data);
    }

    public ClubData LoadClub(int id)
    {
       return _connection.Find<ClubData>(id);
    }
}

