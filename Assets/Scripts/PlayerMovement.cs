using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public NavMeshAgent _agent;
    private Camera _mainCamera;
    float startTime = 0f;
    float holdTime = 5.0f;
    public InputAction InputActions;

    public KeyCode Key;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _mainCamera = Camera.main;

        if (_agent != null)
        {
            if (NavMesh.SamplePosition(transform.position, out NavMeshHit hit, 3.0f, NavMesh.AllAreas))
            {
                _agent.Warp(hit.position);
            }
            else
            {
                Debug.LogError("Player couldnt be set to NavMesh! Is entrance point on NavMesh?");
            }
        }
    }

private void Update()
    {
        if (Pointer.current != null && Pointer.current.press.wasPressedThisFrame)
        {
            Vector2 touchPosition = Pointer.current.position.ReadValue();

            Ray ray = _mainCamera.ScreenPointToRay(touchPosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (_agent != null)
                {
                    if (!_agent.enabled)
                    {
                        _agent.enabled = true;
                    }

                    if (!_agent.isOnNavMesh)
                    {
                        if (NavMesh.SamplePosition(transform.position, out NavMeshHit navHit, 2.0f, NavMesh.AllAreas))
                        {
                            _agent.Warp(navHit.position);
                        }
                    }

                    if (NavMesh.SamplePosition(hit.point, out NavMeshHit edgeHit, 3.0f, NavMesh.AllAreas))
                    {
                        _agent.SetDestination(edgeHit.position);
                    }
                }
            }
        }
    }
}
