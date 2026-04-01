using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public GameObject customerPrefab;
    public Transform entrance;
    public Transform table;
    public Transform exit;
    public Transform[] tables;

    private void Start()
    {
        //Create a new customer TODO: maybe some randomness based on reputation of club?
        Invoke("SpawnCustomer", 1f);
    }

    void SpawnCustomer()
    {
        int randomIndex = Random.Range(0, tables.Length);
        Transform selectedTable = tables[randomIndex];

        GameObject newCustomer = Instantiate(customerPrefab, entrance.position, Quaternion.identity);
        newCustomer.GetComponent<CustomerAI>().Setup(table, exit);

        float randomWait = Random.Range(5f, 15f);
        Invoke("SpawnCustomer", randomWait);
    }
}
