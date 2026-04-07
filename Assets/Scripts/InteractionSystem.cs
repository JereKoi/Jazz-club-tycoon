using TMPro;
using UnityEngine;

public class InteractionSystem : MonoBehaviour
{
    public Transform hand;
    public GameObject itemPrefab;
    public TextMeshProUGUI moneyText;

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
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 2.0f);

        foreach (var hitCollider in hitColliders)
        {
            CustomerAI customer = hitCollider.GetComponent<CustomerAI>();
            if (customer != null && customer.currentState == CustomerAI.CustomerState.Waiting)
            {
                Destroy(_carriedItem);
                _carriedItem = null;

                int tip = Mathf.RoundToInt(customer.patience / 5f);
                _money += tip;
                Debug.Log("Drink delivered! Money total: " + _money);

                if (moneyText != null)
                {
                    moneyText.text = "Money: " + _money;
                }

                Debug.Log("Got tip: " + tip);
                customer.ReceiveDrink();

                return;
            }
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
