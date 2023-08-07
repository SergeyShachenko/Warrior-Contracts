using UnityEngine;
using WC.Runtime.Gameplay.Logic;

namespace WC.Runtime.UI.Character
{
  public class CharacterHUD : UIObjectBase
  {
    [SerializeField] private HPBar _hpBar;
    [SerializeField] private CharacterBase _character;


    protected override void Init() => Show();
    protected override void SubscribeUpdates() => _character.Health.Changed += RefreshHP;
    protected override void UnsubscribeUpdates() => _character.Health.Changed -= RefreshHP;
    protected override void Refresh() => RefreshHP();

    
    private void RefreshHP() => _hpBar.SetProgress(_character.Health.Current, _character.Health.Max);
  }
}