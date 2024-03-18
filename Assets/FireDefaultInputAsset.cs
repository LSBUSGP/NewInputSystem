using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class FireDefaultInputAsset : MonoBehaviour
{
    public Rigidbody body;
    public InputActionAsset actions;
    InputAction fire;

    void Reset()
    {
        body = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        actions.Enable();
        fire = actions.FindAction("Player/Fire");
    }

    void OnDisable()
    {
        actions.Disable();
    }

    void Update()
    {
        if (fire.triggered)
        {
            body.AddForce((Vector3.up + Vector3.forward).normalized * 10, ForceMode.Impulse);
        }
    }
}
