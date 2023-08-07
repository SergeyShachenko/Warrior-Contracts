using UnityEngine;
using WC.Runtime.Infrastructure.Services;
using Zenject;

namespace WC.Runtime.Gameplay.Tools
{
  public class SaveTrigger : MonoBehaviour
  {
    [SerializeField] private CapsuleCollider _collider;
    
    private ISaveLoadService _saveLoadService;

    [Inject]
    private void Construct(ISaveLoadService saveLoadService) => 
      _saveLoadService = saveLoadService;
    

    private void OnTriggerEnter(Collider other)
    {
      _saveLoadService.SaveProgress();
      gameObject.SetActive(false);
    }

    // private void OnDrawGizmos()
    // {
    //   if (_collider == null) return;
    //
    //   
    //   Gizmos.color = new Color32(30, 200, 30, 130);
    //   Gizmos.DrawCube(transform.position + _collider.center, _collider.size);
    // }
  }
}