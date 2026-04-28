using UnityEngine;
using UnityEngine.AI;

public class CustomerAI : MonoBehaviour
{
    public enum CustomerState { Entering, Waiting, Drinking, Leaving }
    public CustomerState currentState = CustomerState.Entering;

    private NavMeshAgent _agent;
   // private Transform _targetTable;
    private Transform _exitPoint;
    public float patience = 100f;
    private Table _assignedTable;
    Club club = new Club();

    public void Setup(Table table, Transform exit)
    {
        _agent = GetComponent<NavMeshAgent>();
        _assignedTable = table;
        _exitPoint = exit;

        _agent.enabled = true;
        _agent.isStopped = false;

        // Stage 1: Walk to table
        _agent.SetDestination(_assignedTable.sitPoint.position);
    }

    private void Update()
    {
        // Stage 2: Check if customer is at table
        if (currentState == CustomerState.Entering && _agent.remainingDistance < 0.5f)
        {
            currentState = CustomerState.Waiting;
            Debug.Log("Customer waits for a drink");
        }

        // Stage 4: Check if customer is at exit position
        if (currentState == CustomerState.Leaving && _agent.remainingDistance < 0.5f)
        {
            Debug.Log("Customer left bar. Good bye and come by again!");
            Destroy(gameObject);
        }
;
    }

    public void ReceiveDrink()
    {
        if (currentState == CustomerState.Waiting)
        {
            currentState = CustomerState.Drinking;
            Debug.Log("Customer drinks");

            // Customer drinks for 3 seconds and then leaves
            Invoke("StartLeaving", 3f);
            Debug.Log("Table is free as customer starts leaving");
            _assignedTable.isOccupied = false;
        }
    }

    void StartLeaving()
    {
        // free up table when customer leaves
        if (_assignedTable != null)
        {
            Debug.Log("Table is free as customer starts leaving");
            _assignedTable.isOccupied = false;
        }
        currentState = CustomerState.Leaving;
        _agent.SetDestination(_exitPoint.position);
        Debug.Log("Customer leaves satisfied");
    }

    public void GetBumped(Vector3 bumpDirection)
    {
        // Recudes patience
        patience -= 5f;

        // Physical reaction, customer fumbles to direction where bumped
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {

            // IsKinematic needs to be false a while to force take effect
            rb.isKinematic = false;
            rb.AddForce(bumpDirection * 5f, ForceMode.Impulse);

            // Grant control to navmesh after a little while
            Invoke("RecoverFromBump", 0.5f);
        }

        
        // TODO: fix debug values, for debugging they are currently high
        if (patience < 50)
        {
            Debug.Log("Customer starts to lose patience!");
        }
        else if(patience < 100f)
                    {
            Debug.Log("Customer has lost all patience and leaves immediatly. No tips, good bye!");
            Club.Instance.DecreaseReputation();            
        }
    }

    void RecoverFromBump()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        // Continues forward to table
        _agent.SetDestination(_assignedTable.sitPoint.position);
    }
}
