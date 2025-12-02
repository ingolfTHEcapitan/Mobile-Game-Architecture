using _Game.Scripts.Data.Player;

namespace _Game.Scripts.Services.PersistantProgress
{
    public interface IPersistantProgressService: IService
    { 
        PlayerProgress Progress {get; set;}
    }
}