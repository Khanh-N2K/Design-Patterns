using UnityEngine;
using UnityEngine.UI;

namespace UI_Example
{
    public class Popup1 : PopupBase
    {
        [SerializeField] private Button closeBtn;
        [SerializeField] private Button openPopup2Btn;

        public override void Show()
        {
            base.Show();
            closeBtn.onClick.AddListener(() => UIManager.Instance.HidePopup(this));
            openPopup2Btn.onClick.AddListener(() => UIManager.Instance.ShowPopup(PopupType.Popup2));
        }

        public override void Hide()
        {
            base.Hide();
            closeBtn.onClick.RemoveAllListeners();
            openPopup2Btn.onClick.RemoveAllListeners();
        }
    }
}