namespace Services.Interfaces
{
    public interface IJetPackService
    {
        void EquipJetpack(bool b, bool b1);
        public bool Propelling { get; }
        public float PropellingForce { get; }
    }
}