using UnityEngine;
using UnityEngine.SceneManagement;
using WC.Runtime.Data;
using WC.Runtime.Data.Characters;
using WC.Runtime.Extensions;
using WC.Runtime.Infrastructure.Services;
using Zenject;

namespace WC.Runtime.Logic.Characters
{
  public class Player : CharacterBase,
    ISaverProgress
  {
    public PlayerID ID { get; private set; }

    [SerializeField] private CharacterController _charController;
    
    private IInputService _inputService;
    private PlayerProgressData _progress;

    [Inject]
    private void Construct(IInputService inputService) => _inputService = inputService;


    protected override void Init()
    {
      Health = new PlayerHealth(_progress.Stats.Life);
      Death = new PlayerDeath();
      Attack = new PlayerAttack(_progress.Stats.Combat, _inputService, _charController, transform);
      Animator = new PlayerAnimator(_charController, p_Animator);
      Movement = new PlayerMovement(_progress.Stats.Movement, _progress.World, _inputService, _charController, transform);
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