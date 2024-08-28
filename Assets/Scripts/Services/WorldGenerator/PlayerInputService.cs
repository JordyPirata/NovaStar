namespace Services
{
public class PlayerInputService : IInputActions
{
    private InputActions inputActions;

    public InputActions InputActions => inputActions;

    public void Awake()
    {
        inputActions = new InputActions();
    }

}    
}