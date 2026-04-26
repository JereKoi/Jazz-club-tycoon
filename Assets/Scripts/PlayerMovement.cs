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
                _agent.SetDestination(hit.point);
            }
            if (Physics.Raycast(ray, out hit))
            {

            }
        }

        //if (Touchscreen.current.)
        //{
        //    startTime = Time.time;
        //    Club.Instance.DecreaseDirtyness();
        //}
    }
}
