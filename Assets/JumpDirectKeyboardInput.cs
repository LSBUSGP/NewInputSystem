using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class JumpDirectKeyboardInput : MonoBehaviour
{
    public Rigidbody body;

    void Reset()
    {
        body = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            body.AddForce(Vector3.up * 10, ForceMode.Impulse);
        }
    }
}
