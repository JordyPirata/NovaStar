namespace Services.Interfaces
{
    public interface IPlayerMediator
    {
        public IRayCastController _raycastController { get; set; }
        public IPlayerMediator _playerMediator { get; set; }
        public ILifeService _lifeService { get; set; }
        public IPlayerInfo _playerInfo { get; set; }
        public IStaminaService _staminaService { get; set; }
    
        void MapLoaded();
    }
}