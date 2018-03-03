using System;
using UnityEngine;
using UnityEngine.UI;

namespace RockPaperScissors.Implementor
{
    [RequireComponent(typeof(Button))]
    public class ButtonImplementor : MonoBehaviour, IButtonComponent, IImplementor
    {
        [SerializeField] private Button _button;
        public Action OnPressed { get; set; }

        private void Start()
        {
            _button.onClick.AddListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            if (OnPressed != null)
            {
                OnPressed();
            }
        }
    }
}