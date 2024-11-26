namespace Services.Interfaces
{
    public interface ITeleportService
    {
        void Interacted();
        void EquipTeleport(bool canOpen);
    }
}