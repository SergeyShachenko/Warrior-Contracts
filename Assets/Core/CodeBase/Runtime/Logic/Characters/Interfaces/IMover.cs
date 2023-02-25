using WC.Runtime.Data;

namespace WC.Runtime.Logic.Characters
{
  public interface IMover : ILogicComponent
  {
    void Warp(Vector3Data to);
    void WarpToSavedPos();
  }
}