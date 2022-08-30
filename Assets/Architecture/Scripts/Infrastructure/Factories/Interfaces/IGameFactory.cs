using UnityEngine;

namespace Infrastructure.Factories
{
  public interface IGameFactory
  {
    GameObject CreateHero(GameObject at);
    void CreateHUD();
  }
}