using UI.Hud;
using UnityEngine.Events;
namespace Services.Interfaces
{
interface IHUDService
{
    public float HealthValue { get; set; }
    public float StaminaValue { get; set; }
    public float HungerValue { get; set; }
    public float ThirstValue { get; set; }
    public float FreezeScreenValue { get; set; }
    public HealthBar healthBar { get; set; }
    public StaminaBar staminaBar { get; set; }
    public HungerBar hungerBar { get; set; }
    public ThirstBar thirstBar { get; set; }
    public MiniMap miniMap { get; set; }
    public FreezeScreen freezeScreen { get; set;}
    }
}