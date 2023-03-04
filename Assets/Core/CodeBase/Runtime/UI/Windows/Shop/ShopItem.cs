using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WC.Runtime.Infrastructure.AssetManagement;
using WC.Runtime.Infrastructure.Services;

namespace WC.Runtime.UI
{
  public class ShopItem : MonoBehaviour
  {
    [SerializeField] private Image _icon;
    [SerializeField] private Button _buttonBuy;
    [SerializeField] private TextMeshProUGUI _textAmount;
    [SerializeField] private TextMeshProUGUI _textAvailablePurchasesLeft;
    [SerializeField] private TextMeshProUGUI _textPrice;

    private ProductDescription _description;
    private IIAPService _iapService;
    private IAssetsProvider _assetsProvider;

    public void Construct(IIAPService iapService, IAssetsProvider assetsProvider, ProductDescription description)
    {
      _iapService = iapService;
      _assetsProvider = assetsProvider;
      _description = description;
    }

    public async void Init()
    {
      _buttonBuy.onClick.AddListener(OnBuyClick);

      _textPrice.text = _description.Config.Price;
      _textAmount.text = _description.Config.Amount.ToString();
      _textAvailablePurchasesLeft.text = _description.AvailablePurchasesLeft.ToString();
      _icon.sprite = await _assetsProvider.Load<Sprite>(_description.Config.IconAddress);
    }

    private void OnBuyClick() => 
      _iapService.StartPurchase(_description.ID);
  }
}