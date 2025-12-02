using _Game.Scripts.Data.Player;

namespace _Game.Scripts.Services.PersistantProgress
{
    public interface ISavedProgressReader
    {
        void LoadProgress(PlayerProgress progress);
    }
}