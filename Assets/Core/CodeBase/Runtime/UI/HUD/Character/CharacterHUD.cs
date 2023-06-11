using UnityEngine;
using WC.Runtime.Logic.Characters;

namespace WC.Runtime.UI.Character
{
  public class CharacterHUD : MonoBehaviour
  {
    [SerializeField] private HPBar _hpBar;
    [SerializeField] private CharacterBase _character;

    private void Awake() => 
      _character.Initialized += OnCharacterInit;

    
    private void OnCharacterInit()
    {
      UpdateView();
      _character.Health.Changed += UpdateView;
    }

    private void OnDestroy() => 
      _character.Health.Changed -= UpdateView;

    private void UpdateView() => 
      _hpBar.SetProgress(_character.Health.Current, _character.Health.Max);
  }
}