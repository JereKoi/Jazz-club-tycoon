using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public GameObject customerPrefab;
    public Transform entrance;
    public Transform table;
    public Transform exit;
    public Table[] tables;

    [Header("Club settings")]
    public float dayDuration = 300f;
    private bool _isClubOpen = true;

    private void Start()
    {
        _isClubOpen = true;
        //Create a new customer TODO: maybe some randomness based on reputation of club?
        Invoke("SpawnCustomer", 1f);

        Invoke("CloseClub", dayDuration);
    }

    //Need to do spawning also take notice on reputation and maybe how earlier day went, word to mouth fame

    void SpawnCustomer()
    {
        if (_isClubOpen) return;
        {
            // search for free tables
            System.Collections.Generic.List<Table> freeTables = new System.Collections.Generic.List<Table>();
            foreach (Table t in tables)
            {
                if (!t.isOccupied) freeTables.Add(t);
            }

            // If free tables are found
            if (freeTables.Count > 0)
            {
                Table selectedTable = freeTables[Random.Range(0, freeTables.Count)];
                selectedTable.isOccupied = true; // Occupy table right away

                GameObject newCustomer = Instantiate(customerPrefab, entrance.position, Quaternion.identity);
                newCustomer.GetComponent<CustomerAI>().Setup(selectedTable, exit);
            }
            else
            {
                Debug.Log("Customer turns away as there is no free tables.");
            }

            int randomIndex = Random.Range(0, tables.Length);

            float randomWait = Random.Range(5f, 15f);
            Invoke("SpawnCustomer", randomWait);
            Debug.Log("New customer has arrived!");
        }

        void CloseClub()
        {
            _isClubOpen = false;
            Debug.Log("Club is now closed. No new customers.");

            // Call day 1, day 2 etc screen and dim and dim back or dim to show some stats, clean up and when cleaned up and prepared for next day, go to exit and then day 1, 2 etc screen
        }
    }
}