namespace Services.Interfaces
{
    public interface IEquipablesService
    {
        void EquipPlanner(bool b);
        void EquipJetpack(bool isInfinite, bool equip);
        void EquipHoverboard(bool b);
        void EquipCoat(bool b);
    }
}