using UnityEngine;
using SQLite;
using System.Linq.Expressions;
using System.Linq;

public sealed class DatabaseManager
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
        _connection.CreateTable<Musician>();
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

    public void SaveMusician(MusicianData data)
    {
        _connection.Update(data);
    }

    private void h234()
    {
        var allMusicians = _connection.Table<MusicianData>().ToList();
        //_connection.Table<Musician>().Select<Musician>();
    }


    public void updateMusician()
    {
        _connection.Update(currentMusician);
        //DatabaseManager.instance.Save(data);
    }

}

