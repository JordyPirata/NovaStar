using Services.Player;

namespace Services
{
    public interface IInventoryService
    {
        int TryPickItem(Item item, int quantity);
    }
}