namespace Game.Scripts.GameEntities
{
    public interface IShipReviveHandler
    {
        void StartRevive();
        void Subscribe();
        void Unsubscribe();
        bool IsReviving { get; set; }
    }
}