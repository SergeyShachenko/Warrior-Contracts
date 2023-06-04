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
    private IInputService _inputService;

    [Inject]
    private void Construct(IInputService inputService) => _inputService = inputService;
    

    private void InitComponents(PlayerProgressData progressData)
    {
      Health = new PlayerHealth(progressData);
      Death = new PlayerDeath();
      Attack = new PlayerAttack(_inputService, progressData, _controller, transform);
      Animator = new PlayerAnimator(_controller, _animator);
      Movement = new PlayerMovement(_inputService, progressData, _controller, transform);
    }
    
    void ILoaderProgress.LoadProgress(PlayerProgressData progressData)
    {
      InitComponents(progressData);
      Init();
    }

    void ISaverProgress.SaveProgress(PlayerProgressData progressData)
    {
      progressData.State.CurrentHP = Health.Current;
      progressData.State.MaxHP = Health.Max;
      progressData.World.LevelPos = new LevelPositionData(SceneManager.GetActiveScene().name, transform.position.ToVector3Data());
    }
  }
}