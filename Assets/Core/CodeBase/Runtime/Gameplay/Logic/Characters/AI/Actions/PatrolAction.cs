namespace WC.Runtime.Gameplay.Logic
{
  public class PatrolAction : AIActionBase
  {
    private readonly CharacterBase _character;

    public PatrolAction(CharacterBase character) => _character = character;
  }
}