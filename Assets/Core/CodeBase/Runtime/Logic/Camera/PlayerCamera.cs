using Cinemachine;
using UnityEngine;
using WC.Runtime.Data.Characters;
using WC.Runtime.Infrastructure.Services;

namespace WC.Runtime.Logic.Camera
{
  public class PlayerCamera : MonoBehaviour,
    ILoaderProgress
  {
    [SerializeField] private CinemachineFreeLook _camera;

    
    public void Follow(GameObject target) => _camera.Follow = target.transform;
    public void LookAt(GameObject target) => _camera.LookAt = target.transform;
    
    
    public void LoadProgress(PlayerProgressData progressData)
    {
      // TODO В зависимости от текущего здоровья игрока включать тряску
    }
  }
}