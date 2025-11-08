using N2K;
using UnityEngine;

namespace UI_Example
{
    public class Initializer : MonoBehaviour
    {
        [SerializeField] private ObjectPoolAtlas _objectPoolAtlas;
        [SerializeField] private UIManager _uiManager;

        private void Awake()
        {
            _objectPoolAtlas.Initialize();
            _uiManager.Initialize();
        }

        private void Start()
        {
            _uiManager.ShowScreen(ScreenType.Screen1);
        }
    }
}