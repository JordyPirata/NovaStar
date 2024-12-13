using InputSystem;
using Services.Interfaces;
namespace Services
{
public class PlayerInputService : IInputActions
{
    public PlayerInputService()
    {
        InputActions = new InputActions();
    }
    public InputActions InputActions { get; set; }

}    

}