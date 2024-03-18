using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class MoveDirectGamepadInput : MonoBehaviour
{
    public Rigidbody body;

    void Reset()
    {
        body = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector2 move = Gamepad.current.leftStick.value;
        body.AddForce(new Vector3(move.x, 0, move.y) * 10);
    }
}
