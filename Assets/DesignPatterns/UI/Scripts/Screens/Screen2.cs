using UnityEngine;
using UnityEngine.UI;

namespace UI_Example
{
    public class Screen2 : ScreenBase
    {
        [SerializeField] private Button _goToScreen1Btn;
        [SerializeField] private Button _openPopup1Btn;

        public override void Show()
        {
            base.Show();
            _goToScreen1Btn.onClick.AddListener(() => UIManager.Instance.ShowScreen(ScreenType.Screen1));
            _openPopup1Btn.onClick.AddListener(() => UIManager.Instance.ShowPopup(PopupType.Popup1));
        }

        public override void Hide() 
        {
            base.Hide();
            _goToScreen1Btn.onClick.RemoveAllListeners();
            _openPopup1Btn.onClick.RemoveAllListeners();
        }
    }
}