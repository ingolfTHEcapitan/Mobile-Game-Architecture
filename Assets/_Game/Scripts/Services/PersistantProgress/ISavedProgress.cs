using _Game.Scripts.Data.Player;

namespace _Game.Scripts.Services.PersistantProgress
{
    public interface ISavedProgress : ISavedProgressReader
    {
        // Дописать что-то в прогресс
        void UpdateProgress(PlayerProgress progress);
    }
    
    public interface ISavedProgressReader
    {
        void LoadProgress(PlayerProgress progress);
    }
}