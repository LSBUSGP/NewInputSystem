using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class MoveDefaultInputAsset : MonoBehaviour
{
    public Rigidbody body;
    DefaultInputActions inputActions;
    Vector2 movement;

    void Reset()
    {
        body = GetComponent<Rigidbody>();
    }

    void Awake()
    {
        inputActions = new DefaultInputActions();
    }

    void OnEnable()
    {
        inputActions.Enable();
    }

    void OnDisable()
    {
        inputActions.Disable();
    }

    void Update()
    {
        movement = inputActions.Player.Move.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        body.AddForce(new Vector3(movement.x, 0, movement.y) * 2);
    }
}
