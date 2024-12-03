namespace Services.Interfaces
{
    public interface IHoverboardService
    {
        void EquipHoverboard(bool b);
        public bool HoverboardEquipped { get; }
        public float HoverBoardSpeedMultiplier { get; }
    }
}