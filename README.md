# NewInputSystem

How to setup and use the new Unity input system

## Setting up the project

First switch on the new input system in the `Project Settings`. Go to the `Edit` menu, and choose `Project Settings`. Then click on the `Player` category and find the `Active Input Handling` entry in the `Configuration` section. Now click on the `Input System Package (New)` option.

![image](https://github.com/LSBUSGP/NewInputSystem/assets/3679392/0a25915a-e05e-410b-b4ad-084461cefb49)

After you have done this, a dialog box will pop up:

![image](https://github.com/LSBUSGP/NewInputSystem/assets/3679392/fdf6dc2c-b506-4b64-a740-9c1ae1f3d2d6)

Click `Apply` and then your project will close itself and open again.

Before you can start using the new input system, you need to import the package. You can do this by opening the package manager, clicking on the `Packages: In Project` button, and choosing `Unity Registry`:

![image](https://github.com/LSBUSGP/NewInputSystem/assets/3679392/392d6ee9-2e61-40d3-9978-ebcd6965d705)

Now, if you type `Input` into the search box, it should find the `Input System` package which you can install by clicking on the `Install` button.

![image](https://github.com/LSBUSGP/NewInputSystem/assets/3679392/ba57ac21-6d96-418e-97d5-7fa6ce29e534)

## Direct keyboard input

Like the old input system, you can use the new input system to directly access the keyboard input.

To demonstrate this, lets create a new scene. Add a plane to act as the ground. Add a sphere. Adjust the sphere position so that it's y coordinate is `0.5` above the ground.

### GetKeyDown equivalent

Now create a new script `JumpDirectKeyboardInput.cs`:

```.cs
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
```

Add this script to the sphere and hit the play button. Pressing the space key on the keyboard should now cause the sphere to jump upwards. This is equivalent to the old input system `GetKeyDown` functionality. The equivalent to the old `GetKeyUp` function is `wasReleasedThisFrame`.

### GetKey equivalent

Remove the `JumpDirectKeyboardInput` component from the sphere, and make a new script `HoverDirectKeyboardInput.cs`:

```.cs
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
```

Add this script to the sphere and hit play. Pressing and holding the space key should now cause the sphere to accelerate upwards. This is the equivalent to the old input system `GetKey` functionality.

## Direct gamepad input

In addition to directly reading the keyboard, we can directly read a connect gamepad (or any other input device.)

Remove the hover script from the sphere and make a new script `MoveDirectGamepadInput.cs`:

```.cs
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class MoveDirectGamepadInput : MonoBehaviour
{
    public Rigidbody body;
    Vector2 movement;

    void Reset()
    {
        body = GetComponent<Rigidbody>();
    }

    void Update()
    {
        movement = Gamepad.current.leftStick.value;
    }

    void FixedUpdate()
    {
        body.AddForce(new Vector3(movement.x, 0, movement.y) * 2);
    }
}
```

Add this script to the sphere and press play. Now, if you connect a gamepad, you can move the sphere around using the left stick.

## Other gamepad input

You can also directly read the right stick with `Gamepad.current.rightStick.value` or the dpad either as a stick input or the individual buttons. Each button can be read as `isPressed`, `wasPressedThisFrame`, `wasReleasedThisFrame`, and `value` which will be the pressure applied to the button if the controller button is pressure sensative.

You can also refer to buttons directly such as:
- `Gamepad.current.aButton`
- `Gamepad.current.bButton`
- `Gamepad.current.xButton`
- `Gamepad.current.yButton`

Or:
- `Gamepad.current.crossButton`
- `Gamepad.current.circleButton`
- `Gamepad.current.triangleButton`
- `Gamepad.current.squareButton`

Or more generically as:
- `Gamepad.current.buttonNorth`
- `Gamepad.current.buttonSouth`
- `Gamepad.current.buttonEast`
- `Gamepad.current.buttonWest`

## Action input

Like the old input system, you can map both keyboard and gamepad inputs to named actions, and read those inputs in your code instead. This creates an abstraction that allows you to re-map inputs without needing to later change your code.

Remove the `MoveDirectGamepadInput` script and make a new script `JumpActionInput.cs`:

```cs
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
```

Add this component to the Sphere object. You will notice a new slot for the jump action:

![image](https://github.com/LSBUSGP/NewInputSystem/assets/3679392/df5957df-f3c3-460b-84c9-1b1bcb1c1738)

To bind a keyboard or gamepad input to this action, click on the `+` button on the right and choose `Add Binding`:

![image](https://github.com/LSBUSGP/NewInputSystem/assets/3679392/ff64c5cf-db43-4bdc-8905-2aebdbed7cb8)

Double click on the new binding to assign it to an input, either a keyboard key, or a gamepad button. The editor interface for this is a bit buggy but will hopefully be fixed in newer versions. You can add as many alternative bindings as you wish. When you are happy, click the run button and try out your new inputs.

### Axis action inputs

Remove the `JumpActionInput` component and create a new script `MoveActionInput.cs`:

```cs
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
```

Add this component to the sphere and add a binding this time for the Gamepad left stick. Add another binding this time chooseing `Add Up\Down\Left\Right Composite`. With this you can bind a key to each direction. When you have assigned the bindings you want, click on the run button and test your input.

## Default Action Inputs

Like the old input system, the new system comes with a selection of pre-difined actions and bindings. But whereas in the old system you needed to read `"Horizontal"` and `"Vertical"` axis values separately, with the new defaults you only need to read the single action input called `Player.Move`.

Remove the `MoveActionInput` component and create a new script `MoveDefaultInputAsset.cs`:

```cs
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
```

Add this script to the sphere object and press run. You should find that the left stick on the gamepad, the WASD keys on the keyboard, and the arrow keys on the keyboard, all move the sphere around.

## Custom Action Inputs

With the new input system, you can create an asset to hold all of your actions and input control bindings separately from your game objects. This helps to keep your code and data organised and means that all of your controls can be managed in one location. To create a custom action input asset, right click in your project window and choose `Input Actions`. You can name this asset anything that makes sense for your game. I will use `GameControls`.

Once created, double clicking this box will open the Input Actions editor window:

![image](https://github.com/LSBUSGP/NewInputSystem/assets/3679392/370c4ddb-787e-4ee4-84fe-18cfb9769ade)

Here you can create and store the input actions for your game. Also in the `Inspector`, while this asset is selected, you will see this option:

![image](https://github.com/LSBUSGP/NewInputSystem/assets/3679392/5c5c1de8-da24-4864-abdb-e0ed76226f31)

If you tick the `Generate C# Class` box, Unity will create some code for you that directly interacts with this asset.

![image](https://github.com/LSBUSGP/NewInputSystem/assets/3679392/99bb0328-8c2c-4fb7-bbcb-51add72a319e)

The `Input Actions Asset` also adds another level of organisation for your inputs which it calls `Action Maps`. Each map is simply a collection of Input Actions. You only need one to store your actions, but you can add as many as you like. These are useful if, for example, your game controls change while you are riding a vehicle, or using different tools. Each map can be Enabled and Disabled separately. For this example, I will create one `Action Map` called `Player`. Once I have defined an action map, I can add actions to it.

I will add actions for `Move` and `Jump` and bind them to controller inputs as above.

Note that, when adding actions in this editor, you have an additional option to define this as a `Button` action or a `Value` action (or `Pass Through` which we will ignore here.)

![image](https://github.com/LSBUSGP/NewInputSystem/assets/3679392/5714d538-1142-4a7c-9ba8-c3b2e2d3e488)

For the move action type we will choose `Value`, whereas for the `Jump` action we will choose `Button`. When you choose `Value` as the action type you also choose the `Control Type` of the value and in this case we want the input as a `Vector2`.

When you have created an bound your actions, it should look like this:

![image](https://github.com/LSBUSGP/NewInputSystem/assets/3679392/68da388d-58da-4342-970b-f070792c1828)

You need to explicity click the `Save Asset` button to save your changes. This also generates the C# file `GameControls.cs` if you have chosen that option.

Finally, we can add these controls to our object. Remove the `MoveDefaultInputAsset` component and add a new script `MoveCustomInputAsset.cs`:

```cs

```


