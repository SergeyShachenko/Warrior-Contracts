using UnityEngine;
using WC.Runtime.Gameplay.Logic;

namespace WC.Runtime.UI.Character
{
  public class CharacterHUD : UIObjectBase
  {
    [SerializeField] private HPBar _hpBar;
    [SerializeField] private CharacterBase _character;


    protected override void Init() => Show();
    protected override void SubscribeUpdates() => _character.Health.Changed += RefreshHealthBar;
    protected override void UnsubscribeUpdates() => _character.Health.Changed -= RefreshHealthBar;
    protected override void Refresh() => RefreshHealthBar();


    private void RefreshHealthBar() => _hpBar.SetProgress(_character.Health.Current, _character.Health.Max);
  }
}