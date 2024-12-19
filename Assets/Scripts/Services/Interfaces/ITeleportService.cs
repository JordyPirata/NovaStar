using Models;

namespace Services.Interfaces
{
    public interface ITeleportService
    {
        void Interacted();
        void EquipTeleport(bool canOpen, bool is15Teleports);
        TeleportsModel GetTeleportsModel();
        void LoadTeleports(TeleportsModel teleportsModel);
    }
}