using Services.Player;

namespace Services.Interfaces
{
    public interface IInventoryService
    {
        int TryPickItem(Item item, int quantity);
    }
}