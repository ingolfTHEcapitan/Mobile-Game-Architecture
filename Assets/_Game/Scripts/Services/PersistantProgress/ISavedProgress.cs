using _Game.Scripts.Data.Player;

namespace _Game.Scripts.Services.PersistantProgress
{
    public interface ISavedProgress : ISavedProgressReader
    {
        void UpdateProgress(PlayerProgress progress);
    }
}