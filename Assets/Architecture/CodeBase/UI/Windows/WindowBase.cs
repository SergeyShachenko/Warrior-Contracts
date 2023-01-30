using CodeBase.Data;
using CodeBase.Infrastructure.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
  public abstract class WindowBase : MonoBehaviour
  {
    [SerializeField] private Button _closeButton;

    protected PlayerProgressData p_PlayerProgress => p_ProgressService.Progress;
    
    protected IPersistentProgressService p_ProgressService;

    public void Construct(IPersistentProgressService progressService) => 
      p_ProgressService = progressService;


    private void Awake() => 
      OnAwake();

    private void Start()
    {
      Init();
      SubscribeUpdates();
    }

    private void OnDestroy() => 
      CleanUp();

    
    protected virtual void Init(){}
    protected virtual void SubscribeUpdates(){}
    protected virtual void CleanUp(){}

    protected virtual void OnAwake() => 
      _closeButton.onClick.AddListener(() => Destroy(gameObject));
  }
}