using UnityEngine;

public class Table : MonoBehaviour
{
    public bool isOccupied = false;
    public Transform sitPoint;

    private void Start()
    {
        if (sitPoint == null) sitPoint = this.transform;
    }
}
