using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public GameObject customerPrefab;
    public Transform entrance;
    public Transform table;
    public Transform exit;

    private void Start()
    {
        //Create a new customer after 20 seconds TODO: maybe some randomness based on reputation of club?
        Invoke("SpawnCustomer", 20f);
    }

    void SpawnCustomer()
    {
        GameObject newCustomer = Instantiate(customerPrefab, entrance.position, Quaternion.identity);
        newCustomer.GetComponent<CustomerAI>().Setup(table, exit);
    }
}
