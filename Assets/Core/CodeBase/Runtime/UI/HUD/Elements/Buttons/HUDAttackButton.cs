using WC.Runtime.Gameplay.Services;
using WC.Runtime.Logic.Characters;
using WC.Runtime.UI.Elements;
using Zenject;

namespace WC.Runtime.UI.Elements
{
  public class HUDAttackButton : UIButtonBase
  {
    private Player _player;

    [Inject]
    private void Construct(ICharacterFactory characterFactory) => 
      _player = characterFactory.Registry.Player;

    
    protected override void OnPressed()
    {
      base.OnPressed();
      
      _player.Animator.PlayAttack(id: 1);
    }
  }
}