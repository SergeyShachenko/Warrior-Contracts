using WC.Runtime.Data;

namespace WC.Runtime.Logic.Characters
{
  public interface IMovement : ILogicComponent
  {
    void Warp(Vector3Data to);
    void WarpToSavedPos();
  }
}