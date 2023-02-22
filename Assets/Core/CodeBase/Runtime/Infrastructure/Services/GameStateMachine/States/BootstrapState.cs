using UnityEngine;
using WC.Runtime.UI.Services;
using WC.Runtime.Infrastructure.AssetManagement;
using WC.Runtime.Infrastructure.Services;
using WC.Runtime.UI;

namespace WC.Runtime.Infrastructure.Services
{
  public class BootstrapState : IDefaultState
  {
    private const string InitSceneName = "BootScene";
    
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly AllServices _services;

    public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
    {
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
      _services = services;
      
      RegisterServices();
    }
    

    public void Enter()
    {
      _sceneLoader.Load(InitSceneName, onLoaded: EnterLoadLevel);
    }

    public void Exit()
    {
      
    }

    private void RegisterServices()
    {
      IInputService inputService = RegisterInputService();
      IAssetsProvider assetProviderProvider = RegisterAssetProvider();
      IStaticDataService staticDataService = RegisterStaticData();
      IPersistentProgressService progressService = new PersistentProgressService();
      IAPService iapService = RegisterIAPService(new IAPProvider(), progressService);
      IRandomService randomService = new RandomService();
      IAdsService adsService = RegisterAdsService();
      IUIFactory uiFactory = new UIFactory(assetProviderProvider, staticDataService, progressService, adsService, iapService);
      IWindowService windowService = new WindowService(uiFactory);
      IGameFactory gameFactory = new GameFactory(progressService, assetProviderProvider, staticDataService, randomService, windowService);
      ISaveLoadService saveLoadService = new SaveLoadService(progressService, gameFactory);
      
      _services.RegisterSingle<IGameStateMachine>(_stateMachine);
      _services.RegisterSingle(progressService);
      _services.RegisterSingle(randomService);
      _services.RegisterSingle(uiFactory);
      _services.RegisterSingle(windowService);
      _services.RegisterSingle(gameFactory);
      _services.RegisterSingle(saveLoadService);
    }

    private AssetProvider RegisterAssetProvider()
    {
      var assetProvider = new AssetProvider();
      assetProvider.Init();
      _services.RegisterSingle(assetProvider);
      
      return assetProvider;
    }

    private IStaticDataService RegisterStaticData()
    {
      IStaticDataService staticData = new StaticDataService();
      staticData.LoadData();
      _services.RegisterSingle(staticData);

      return staticData;
    }

    private AdsService RegisterAdsService()
    {
      var adsService = new AdsService();
      adsService.Init();
      _services.RegisterSingle<IAdsService>(adsService);
      
      return adsService;
    }
    
    private IAPService RegisterIAPService(IAPProvider iapProvider, IPersistentProgressService progressService)
    {
      var iapService = new IAPService(iapProvider, progressService);
      iapService.Init();
      _services.RegisterSingle<IIAPService>(iapService);
      
      return iapService;
    }

    private void EnterLoadLevel() => 
      _stateMachine.Enter<LoadProgressState>();

    private IInputService RegisterInputService()
    {
      IInputService inputService = Application.isEditor ? new StandaloneInputService() : new TouchInputService();
      _services.RegisterSingle(inputService);

      return inputService;
    }
  }
}