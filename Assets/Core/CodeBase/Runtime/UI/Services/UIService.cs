﻿using UnityEngine;

namespace WC.Runtime.UI.Services
{
  public class UIService : IUIService
  {
    public void SetCursorVisible(bool isVisible)
    {
      Cursor.visible = isVisible;
      Cursor.lockState = isVisible ? CursorLockMode.Confined : CursorLockMode.Locked;
    }
  }
}