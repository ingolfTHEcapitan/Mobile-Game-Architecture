using _Game.Scripts.Data.Player;

namespace _Game.Scripts.Services.SaveLoad
{
    public interface ISaveLoadService: IService
    {
        void SaveProgress();
        PlayerProgress LoadProgress();
    }
}