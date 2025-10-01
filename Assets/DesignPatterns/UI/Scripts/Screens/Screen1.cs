using UnityEngine;
using UnityEngine.UI;

namespace UI_Example
{
    public class Screen1 : ScreenBase
    {
        [SerializeField] private Button _goToScreen2Btn;
        [SerializeField] private Button _openPopup1Btn;

        public override void Show()
        {
            base.Show();
            _goToScreen2Btn.onClick.AddListener(() => UIManager.Instance.ShowScreen(ScreenType.Screen2));
            _openPopup1Btn.onClick.AddListener(() => UIManager.Instance.ShowPopup(PopupType.Popup1));
        }

        public override void Hide()
        {
            base.Hide();
            _goToScreen2Btn.onClick.RemoveAllListeners();
            _openPopup1Btn.onClick.RemoveAllListeners();
        }
    }
}