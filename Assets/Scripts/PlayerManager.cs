using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public Transform entrance;
    public TextMeshProUGUI moneyText;
    public static PlayerManager Instance;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void Start()
    {        
        GameObject newCustomer = Instantiate(playerPrefab, entrance.position, Quaternion.identity);
        if (Instance == null)
        {
            Instance = this;
        }
    }
}
