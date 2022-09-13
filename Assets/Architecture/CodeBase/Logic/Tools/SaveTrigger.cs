using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Logic.Tools
{
  public class SaveTrigger : MonoBehaviour
  {
    [SerializeField] private CapsuleCollider _collider;
    
    private ISaveLoadService _saveLoadService;

    private void Awake()
    {
      _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
    }

    private void OnTriggerEnter(Collider other)
    {
      _saveLoadService.SaveProgress();
      Debug.Log("Игра сохранена");
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