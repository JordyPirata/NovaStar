namespace Services.Interfaces 
{
    public interface IStaminaService
    {
        int Stamina { get; set; }
        void TakeStamina(int stamina);
        void RecoverStamina(int recover);
    }
}