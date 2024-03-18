using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class JumpActionInput : MonoBehaviour
{
    public Rigidbody body;
    public InputAction jump;

    void Reset()
    {
        body = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        jump.Enable();
    }

    void OnDisable()
    {
        jump.Disable();
    }

    void Update()
    {
        if (jump.triggered)
        {
            body.AddForce(Vector3.up * 10, ForceMode.Impulse);
        }
    }
}
