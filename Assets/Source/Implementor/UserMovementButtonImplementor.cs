using System;
using Svelto.ECS;
using UnityEngine;
using UnityEngine.UI;

namespace RockPaperScissors.Implementor
{
    [RequireComponent(typeof(Button))]
    public class UserMovementButtonImplementor : MonoBehaviour, IUserMovementButtonComponent, IImplementor
    {
        [SerializeField] private Button _button;
        [SerializeField] private UserMovement _movement;
        public DispatchOnSet<UserMovementInfo> OnPressed { get; private set; }

        public bool IsInteractable
        {
            get { return _button.interactable;}
            set { _button.interactable = value; }
        }

        private void Awake()
        {
            OnPressed = new DispatchOnSet<UserMovementInfo>();
            IsInteractable = _button.interactable;
            _button.onClick.AddListener(OnButtonClick);
        }
        
        private void OnButtonClick()
        {
            OnPressed.value = new UserMovementInfo(_movement);
        }
    }
}