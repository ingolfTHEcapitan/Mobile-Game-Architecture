using _Game._Scripts.Data.Player;

namespace _Game._Scripts.Infrastructure.Services.PersistantProgress
{
    public interface IPersistantProgressService: IService
    { 
        PlayerProgress Progress {get; set;}
    }
}