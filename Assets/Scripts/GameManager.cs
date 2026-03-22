using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int currentClubId = 1;

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
    }

    private void OnApplicationQuit()
    {
        //Release database resources cleanly
        DatabaseManager.Instance.CloseConnection();
    }
}
