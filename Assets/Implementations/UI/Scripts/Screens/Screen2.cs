using N2K;
using UnityEngine;
using UnityEngine.UI;

namespace UI_Example
{
    public class Screen2 : ScreenBase
    {
        [Header("=== SCREEN2 ===")]

        [SerializeField] 
        private Button _goToScreen1Btn;
        
        [SerializeField] 
        private Button _openPopup1Btn;

        protected override void Initialize()
        {
            base.Initialize();

            _goToScreen1Btn.onClick.AddListener(() => UIManager.Instance.ShowScreen(ScreenType.Screen1));
            _openPopup1Btn.onClick.AddListener(() => UIManager.Instance.ShowPopup(PopupType.Popup1));
        }
    }
}