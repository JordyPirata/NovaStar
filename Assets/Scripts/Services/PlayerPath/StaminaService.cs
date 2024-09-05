using Services.Interfaces;
namespace Services.PlayerPath
{
    public class StaminaService : IStaminaService
    {
        public int Stamina { get; set; }
        public StaminaService()
        {
            Stamina = 100;
        }
        public void TakeStamina(int stamina)
        {
            Stamina -= stamina;
        }
        public void RecoverStamina(int recover)
        {
            Stamina += recover;
        }
    }
}