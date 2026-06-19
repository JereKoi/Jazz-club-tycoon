using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public Transform entrance;
    public TextMeshProUGUI moneyText;
    public static PlayerManager Instance;

    public void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        GameObject newCustomer = Instantiate(playerPrefab, entrance.position, Quaternion.identity);
      
    }
}
