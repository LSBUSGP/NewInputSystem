using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CustomInputAsset : MonoBehaviour
{
    public Rigidbody body;
    GameControls input;
    Vector2 movement;

    void Reset()
    {
        body = GetComponent<Rigidbody>();
    }

    void Awake()
    {
        input = new GameControls();
    }

    void OnEnable()
    {
        input.Enable();
    }

    void OnDisable()
    {
        input.Disable();
    }

    void Update()
    {
        movement = input.Player.Move.ReadValue<Vector2>();
        if (input.Player.Jump.triggered)
        {
            body.AddForce(Vector3.up * 10, ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        body.AddForce(new Vector3(movement.x, 0, movement.y) * 2);
    }
}
