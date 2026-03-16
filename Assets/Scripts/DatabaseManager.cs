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
    string databasePath = System.IO.Path.Combine(Application.persistentDataPath, "JazzClub.db"); // Is there alternative way since no monobehaviour is being used?
    public Musician currentMusician;

    private void InitializeDatabase()
    {
        _connection = new SQLiteConnection(databasePath);
        Debug.Log("Connection exists, connected succesfully");
        Debug.Log(databasePath);
        if (_connection == null)
        {

            _connection = new SQLiteConnection(databasePath);
        }
        _connection.CreateTable<MusicianData>();
        _connection.CreateTable<InventoryItem>();
        Debug.Log("Created a new musician table");
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

    public InventoryItem LoadInventoryItem(int id)
    {
        return _connection.Find<InventoryItem>(id);
    }

    public void SaveMusician(MusicianData data)
    {
        _connection.InsertOrReplace(data);
    }

    public void CreateMusician(string name)
    {
        var newMusician = new MusicianData { Name = name, Virtuosity = 0, Charisma = 0 };
        _connection.Insert(newMusician);
    }

    public void updateMusician()
    {
        _connection.InsertOrReplace(currentMusician);
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
}

