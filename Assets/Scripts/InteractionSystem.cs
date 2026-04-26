using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionSystem : MonoBehaviour
{
    public Transform hand;
    public GameObject itemPrefab;

    PlayerManager playerManager;
    Club club;
    ClubData clubData;
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
        Debug.Log("Drink hits the table");

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

                if (PlayerManager.Instance.moneyText != null)
                {
                    PlayerManager.Instance.moneyText.text = "Money: " + _money;
                    Debug.Log("Money text was null");
                }

                Debug.Log("Got tip: " + tip);
                customer.ReceiveDrink();

                return;
            }
        }
    }

    private void CleanUp()
    {
        if (clubData.dirtyness < 10f && Pointer.current != null && Pointer.current.press.isPressed)
        {
            Debug.Log("Player starts cleaning!");
        }
    }

    // TODO: even though I walk into customer, nothing happens
    private void OnCollisionEnter(Collision collision)
    {
        // Makes sure that CustomerAI is used
        CustomerAI customer = collision.gameObject.GetComponent<CustomerAI>();
        Debug.Log("Hit something : " + collision.gameObject.name);

        if (customer != null)
        {
            // Calculating position, where layer is heading on moment of collision
            // collision.contacts[0].normal gives collision direction
            Vector3 bumpDir = -collision.contacts[0].normal;
            bumpDir.y = 0; // keeps bump direction on horizontal way. ( So that customer does not fly upwards)

            // call customer getBumped method.
            customer.GetBumped(bumpDir.normalized);

            Debug.Log("You pumbed into a customer!");
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
