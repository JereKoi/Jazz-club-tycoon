using UnityEngine;
using UnityEngine.AI;

public class CustomerAI : MonoBehaviour
{
    public enum CustomerState { Entering, Waiting, Drinking, Leaving }
    public CustomerState currentState = CustomerState.Entering;

    private NavMeshAgent _agent;
    private Transform _targetTable;
    private Transform _exitPoint;

    public void Setup(Transform table, Transform exit)
    {
        _agent = GetComponent<NavMeshAgent>();
        _targetTable = table;
        _exitPoint = exit;

        // Stage 1: Walk to table
        _agent.SetDestination(_targetTable.position);
    }

    private void Update()
    {
        // Stage 2: Check if customer is at table
        if (currentState == CustomerState.Entering && _agent.remainingDistance < 0.5f)
        {
            currentState = CustomerState.Waiting;
            Debug.Log("Customer waits for a drink");
        }
    }

    public void ReceiveDrink()
    {
        if (currentState == CustomerState.Waiting)
        {
            currentState = CustomerState.Drinking;
            Debug.Log("Customer drinks");

            // Customer drinks for 3 seconds and then leaves
            Invoke("StartLeaving", 3f);
        }
    }

    void StartLeaving()
    {
        currentState = CustomerState.Leaving;
        _agent.SetDestination(_exitPoint.position);
        Debug.Log("Customer leaves satisfied");
    }
}
