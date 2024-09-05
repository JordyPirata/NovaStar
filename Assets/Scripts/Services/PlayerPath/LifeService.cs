using Services.Interfaces;
public class LifeService : ILifeService
{
    public LifeService()
    {
        Life = 100;
    }
    public int Life { get; set; }
    public void TakeDamage(int damage)
    {
        Life -= damage;
    }
    public void Heal(int heal)
    {
        Life += heal;
    }
}
