using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public Transform entrance;


    public void Start()
    {
        GameObject newCustomer = Instantiate(playerPrefab, entrance.position, Quaternion.identity);
    }
}
