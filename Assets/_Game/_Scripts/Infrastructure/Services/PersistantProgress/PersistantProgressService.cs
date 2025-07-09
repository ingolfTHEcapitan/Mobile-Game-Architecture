using _Game._Scripts.Data;

namespace _Game._Scripts.Infrastructure.Services.PersistantProgress
{
    public class PersistantProgressService : IPersistantProgressService
    {
        public PlayerProgress Progress {get; set;}
    }
}