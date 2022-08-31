using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Infrastructure.Factories
{
  public interface IGameFactory : IService
  {
    GameObject CreateHero(GameObject at);
    void CreateHUD();
  }
}