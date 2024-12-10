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
    public HealthBar HealthBar { get; set; }
    public StaminaBar StaminaBar { get; set; }
    public HungerBar HungerBar { get; set; }
    public ThirstBar ThirstBar { get; set; }
    public MiniMap MiniMap { get; set; }
    public FreezeScreen FreezeScreen { get; set;}
    }
}