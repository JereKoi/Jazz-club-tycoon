using UnityEngine;
using SQLite;
using System.Linq.Expressions;

public class DatabaseManager
{
    private SQLiteConnection _connection;


    private void Start()
    {
        _connection = new SQLiteConnection(""); //TODO: What path needs to be?
    }
}
