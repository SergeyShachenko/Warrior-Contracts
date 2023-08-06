using UnityEngine;
using UnityEngine.SceneManagement;
using WC.Runtime.Data;
using WC.Runtime.Data.Characters;
using WC.Runtime.Extensions;
using WC.Runtime.Gameplay.Services;
using WC.Runtime.Infrastructure.Services;
using WC.Runtime.Logic.Camera;
using Zenject;

namespace WC.Runtime.Logic.Characters
{
  public class Player : CharacterBase,
    ISaverProgress
  {
    public PlayerID ID { get; private set; }

    public PlayerCamera Camera { get; private set; }
    
    [field: SerializeField] public CharacterController Controller { get; private set; }

    private IInputService _inputService;
    private PlayerProgressData _progress;

    [Inject]
    private void Construct(IInputService inputService, ILevelToolsFactory levelToolsFactory)
    {
      _inputService = inputService;
      Camera = levelToolsFactory.Registry.PlayerCamera;
    }


    protected override void Init()
    {
      Health = new PlayerHealth(_progress.Stats.Life);
      Death = new PlayerDeath();
      Attack = new PlayerAttack(this, _progress.Stats.Combat, _inputService);
      Animator = new PlayerAnimator(this, p_Animator);
      Movement = new PlayerMovement(this, _progress.Stats.Movement, _progress.World, _inputService);
      
      Movement.WarpToSavedPos();
    }

    
    void ILoaderProgress.LoadProgress(PlayerProgressData progress)
    {
      ID = progress.ID;
      _progress = progress;
    }

    void ISaverProgress.SaveProgress(PlayerProgressData progress)
    {
      progress.Stats.Life.CurrentHealth = Health.Current;
      progress.World.LevelPos = new LevelPositionData(SceneManager.GetActiveScene().name, transform.position.ToVector3Data());
    }
  }
}