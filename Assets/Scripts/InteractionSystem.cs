using UnityEngine;

public class InteractionSystem : MonoBehaviour
{
    public Transform hand;
    public GameObject itemPrefab;

    private GameObject _carriedItem;
    private bool _canPickUp = false;


    private void Update()
    {
        if (_canPickUp && _carriedItem == null)
        {
            PickUp();
        }
    }

    void PickUp()
    {
        _carriedItem = Instantiate(itemPrefab, hand.position, hand.rotation);

        _carriedItem.transform.SetParent(hand);

        Debug.Log("Drink grapped!");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Counter"))
        {
            _canPickUp = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Counter"))
        {
            _canPickUp = false;
        }
    }
}
