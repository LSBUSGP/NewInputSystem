using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class MoveActionInput : MonoBehaviour
{
    public Rigidbody body;
    public InputAction move;
    Vector2 movement;

    void Reset()
    {
        body = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        move.Enable();
    }

    void OnDisable()
    {
        move.Disable();
    }

    void Update()
    {
        movement = move.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        body.AddForce(new Vector3(movement.x, 0, movement.y) * 2);
    }
}
