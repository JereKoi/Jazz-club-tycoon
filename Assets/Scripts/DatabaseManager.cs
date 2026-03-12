using UnityEngine;
using SQLite;
using System.Linq.Expressions;

public class DatabaseManager
{
    private SQLiteConnection _connection;
    string databasePath = System.IO.Path.Combine(Application.persistentDataPath, "JazzClub.db");
    public object Musician;

    private void InitializeDatabase()
    {
        if (_connection == null)
        {
            _connection = new SQLiteConnection(databasePath);
            Debug.Log("Connection exists, connected succesfully");

        }
        _connection.CreateTable<Musician>();
        Debug.Log("Created a new musician table");
    }

    private void Awake()
    {
        InitializeDatabase();
        Debug.Log(databasePath);
    }

    private void Start()
    {
        
        _connection.Insert(Musician);

        

        _connection.Table<Musician>().ToList();
    }


    public void updateMusician()
    {
        _connection.Update(Musician);
    }

}
