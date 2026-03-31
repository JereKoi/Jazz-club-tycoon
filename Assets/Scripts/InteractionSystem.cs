using UnityEngine;

public class InteractionSystem : MonoBehaviour
{
    public Transform hand;
    public GameObject itemPrefab;

    private GameObject _carriedItem;
    private bool _onCounter = false;
    private bool _onTable = false;

    private int _money = 0;


    private void Update()
    {
        if (_onCounter && _carriedItem == null)
        {
            PickUp();
        }

        if (_onTable && _carriedItem != null)
        {
            DropOff();
        }
    }

    void PickUp()
    {
        _carriedItem = Instantiate(itemPrefab, hand.position, hand.rotation);

        _carriedItem.transform.SetParent(hand);

        Debug.Log("Drink grapped!");
    }

    void DropOff()
    {
        CustomerAI customer = FindAnyObjectByType<CustomerAI>();
        if (customer != null && Customer.currentState == CustomerAI.CustomerState.Waiting)
        {
            Destroy(_carriedItem);
            _carriedItem = null;

            _money += 10;
            Debug.Log("Drink delivered! Money total: " + _money);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Counter"))
        {
            _onCounter = true;
        }
        if (other.CompareTag("Table"))
        {
            _onTable = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Counter"))
        {
            _onCounter = false;
        }
        if (other.CompareTag("Table"))
        {
            _onTable = false;
        }
    }
}
