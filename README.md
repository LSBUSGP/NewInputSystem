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

Add this script to the sphere and hit the play button. Pressing the space key on the keyboard should now cause the sphere to jump upwards. This is equivalent to the old input system `GetKeyDown` functionality.

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

