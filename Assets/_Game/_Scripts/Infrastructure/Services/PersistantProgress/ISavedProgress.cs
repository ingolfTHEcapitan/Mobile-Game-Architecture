using _Game._Scripts.Data;

namespace _Game._Scripts.Infrastructure.Services.PersistantProgress
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