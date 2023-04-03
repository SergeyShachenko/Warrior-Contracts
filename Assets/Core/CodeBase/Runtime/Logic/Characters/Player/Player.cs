using UnityEngine.SceneManagement;
using WC.Runtime.Data;
using WC.Runtime.Data.Characters;
using WC.Runtime.Extensions;
using WC.Runtime.Infrastructure.Services;
using Zenject;

namespace WC.Runtime.Logic.Characters
{
  public class Player : CharacterBase
  {
    //TODO Переписать создание
    public WarriorID ID { get; private set; }
    
    private IInputService _inputService;

    [Inject]
    private void Construct(IInputService inputService) => _inputService = inputService;

    protected override void Init()
    {
      Health = new PlayerHealth(p_Progress);
      Death = new PlayerDeath();
      Attack = new PlayerAttack(_inputService, p_Progress, _controller, transform);
      Animator = new PlayerAnimator(_controller, _animator);
      Movement = new PlayerMovement(_inputService, p_Progress, _controller, transform);

      base.Init();
    }


    public override void LoadProgress(PlayerProgressData progressData)
    {
      base.LoadProgress(progressData);
      
      Init();
    }

    public override void SaveProgress(PlayerProgressData progressData)
    {
      base.SaveProgress(progressData);
      
      progressData.State.CurrentHP = Health.Current;
      progressData.State.MaxHP = Health.Max;
      progressData.World.LevelPos = new LevelPositionData(SceneManager.GetActiveScene().name, transform.position.ToVector3Data());
    }
  }
}