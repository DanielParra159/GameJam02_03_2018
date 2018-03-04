using System;
using UnityEngine;
using UnityEngine.UI;

namespace RockPaperScissors.Implementor
{
    [RequireComponent(typeof(Button))]
    public class UserMovementButtonImplementor : MonoBehaviour, IUserMovementButtonComponent, IImplementor
    {
        [SerializeField] private Button _button;
        [SerializeField] private UserMovement _movement;
        public Action<UserMovementInfo> OnPressed { get; set; }

        public bool IsInteractable
        {
            get { return _button.interactable;}
            set { _button.interactable = value; }
        }

        private void Start()
        {
            IsInteractable = _button.interactable;
            _button.onClick.AddListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            if (OnPressed != null)
            {
                OnPressed(new UserMovementInfo(_movement));
            }
        }
    }
}