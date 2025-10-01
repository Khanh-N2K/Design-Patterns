using UnityEngine;
using UnityEngine.UI;

namespace UI_Example
{
    public class Popup2 : PopupBase
    {
        [SerializeField] private Button closeBtn;

        public override void Show()
        {
            base.Show();
            closeBtn.onClick.AddListener(() => UIManager.Instance.HidePopup(this));
        }

        public override void Hide()
        {
            base.Hide();
            closeBtn.onClick.RemoveAllListeners();
        }
    }
}