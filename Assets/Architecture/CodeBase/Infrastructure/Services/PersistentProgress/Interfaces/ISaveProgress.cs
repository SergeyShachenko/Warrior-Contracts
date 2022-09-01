﻿using CodeBase.Data;

namespace CodeBase.Infrastructure.Services.PersistentProgress
{
  public interface ISaveProgress : IReadProgress
  {
    void SaveProgress(PlayerProgress progress);
  }
}