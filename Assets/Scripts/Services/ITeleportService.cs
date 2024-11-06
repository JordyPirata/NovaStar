namespace Services
{
    public interface ITeleportService
    {
        void Interacted();
        void EquipTeleport(bool canOpen);
    }
}