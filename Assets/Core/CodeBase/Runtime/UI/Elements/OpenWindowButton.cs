using UnityEngine;
using UnityEngine.UI;
using WC.Runtime.UI.Windows;
using WC.Runtime.UI.Services;

namespace WC.Runtime.UI.Elements
{
  public class OpenWindowButton : MonoBehaviour
  {
    [SerializeField] private WindowID _windowID;
    
    [Header("Links")]
    [SerializeField] private Button _openWindowButton;

    private IWindowService _windowService;

    public void Construct(IWindowService windowService) => 
      _windowService = windowService;


    private void Awake() => 
      _openWindowButton.onClick.AddListener(OpenWindow);


    private void OpenWindow() => 
      _windowService.Open(_windowID);
  }
}