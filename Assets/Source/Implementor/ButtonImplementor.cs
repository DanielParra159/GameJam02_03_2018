using UnityEngine;
using UnityEngine.UI;

namespace RockPaperScissors.Implementor
{
    [RequireComponent(typeof(Button))]
    public class ButtonImplementor : MonoBehaviour, IButtonComponent, IImplementor
    {
        [SerializeField] private Button _button;
        public bool IsPressed { get; private set; }
        public bool Reset
        {
            set { IsPressed = false; }
        }

        private void Start()
        {
            _button.onClick.AddListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            IsPressed = true;
            Debug.Log("Click");
        }
    }
}