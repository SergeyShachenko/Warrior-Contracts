using WC.Runtime.Data;
using WC.Runtime.Data.Characters;
using WC.Runtime.StaticData;
using Zenject;

namespace WC.Runtime.Infrastructure.Services
{
  public class PersistentProgressService : IPersistentProgressService
  {
    public PlayerProgressData Player { get; set; }
    
    private IStaticDataService _staticDataService;
    private IConfigService _configService;

    [Inject]
    private void Construct(IStaticDataService staticDataService, IConfigService configService)
    {
      _staticDataService = staticDataService;
      _configService = configService;
    }


    public void ResetProgress()
    {
      var defaultProgress = _configService.Load<DefaultProgressConfigWrapper>();
      PlayerStaticData playerData = _staticDataService.Players[defaultProgress.Player];
      
      playerData.Stats.Life.CurrentHealth = playerData.Stats.Life.MaxHealth;
      playerData.Stats.Life.CurrentArmor = playerData.Stats.Life.MaxArmor;

      Player = new PlayerProgressData(playerData.ID, playerData.Stats, defaultProgress.Level);
    }
  }
}