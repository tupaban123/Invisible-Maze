using InvisibleMaze.InputSystem;

public class AndroidInputSystem : IInputSystem
{
    private Joystick _playerJoystick;

    public AndroidInputSystem(Joystick playerJoystick)
    {
        _playerJoystick = playerJoystick;
    }

    public float Horizontal => _playerJoystick.Horizontal;

    public float Vertical => _playerJoystick.Vertical;
}