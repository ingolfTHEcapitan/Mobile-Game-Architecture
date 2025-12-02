using _Game.Scripts.Data.Player;

namespace _Game.Scripts.Services.PersistantProgress
{
    public class PersistantProgressService : IPersistantProgressService
    {
        public PlayerProgress Progress {get; set;}
    }
}