using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Instance = this;
        
        try
        {
            DatabaseManager.Instance.LoadClub(1);
        }
        catch (Exception e)
        {
            Debug.Log("Error loading club: " + e);
            if (DatabaseManager.Instance.LoadClub())
            {

            }
        }


    }

    private void OnApplicationQuit()
    {
        //Release database resources cleanly
        DatabaseManager.Instance.CloseConnection();
    }
}
