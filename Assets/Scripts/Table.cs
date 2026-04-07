using UnityEngine;

public class Table : MonoBehaviour
{
    public bool isOccupied = false;
    public Transform sitPoint;

    private void Awake()
    {
        if (sitPoint == null) sitPoint = this.transform;
    }
}
