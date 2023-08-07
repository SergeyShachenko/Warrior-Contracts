using System.Collections.Generic;
using WC.Runtime.Infrastructure.Services;
using WC.Runtime.Gameplay.Logic;

namespace WC.Runtime.Gameplay.Services
{
  public class LootRegistry : IRegistry
  {
    public IReadOnlyList<LootPiece> LootPieces => new List<LootPiece>(_lootPieces);

    private readonly List<LootPiece> _lootPieces = new();

    
    public void Register(LootPiece lootPiece) => _lootPieces.Add(lootPiece);
    public void Unregister(LootPiece lootPiece) => _lootPieces.Remove(lootPiece);
    
    public void CleanUp() => _lootPieces.Clear();
  }
}