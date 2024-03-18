using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class HoverDirectKeyboardInput : MonoBehaviour
{
    public Rigidbody body;
    float force = 0;

    void Reset()
    {
        body = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Keyboard.current.spaceKey.isPressed)
        {
            force = 15;
        }
        else
        {
            force = 0;
        }
    }

    void FixedUpdate()
    {
        body.AddForce(Vector3.up * force);
    }
}
