using _Game._Scripts.Data.Player;

namespace _Game._Scripts.Infrastructure.Services.SaveLoad
{
    public interface ISaveLoadService: IService
    {
        void SaveProgress();
        PlayerProgress LoadProgress();
    }
}