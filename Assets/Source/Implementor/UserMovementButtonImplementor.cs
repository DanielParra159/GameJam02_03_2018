using Svelto.ECS;
using UnityEngine;
using UnityEngine.UI;

namespace RockPaperScissors.Implementor
{
    [RequireComponent(typeof(Button))]
    public class UserMovementButtonImplementor : MonoBehaviour, IButtonComponent, IUserMovementButtonComponent, IImplementor
    {
        [SerializeField] private Button _button;
        [SerializeField] private UserMovement _movement;
        public DispatchOnSet<bool> OnPressed { get; private set; }
        public UserMovementInfo UserMovementInfo { get; private set; }

        public bool IsInteractable
        {
            get { return _button.interactable; }
            set { _button.interactable = value; }
        }

        private void Awake()
        {
            OnPressed = new DispatchOnSet<bool>();
            IsInteractable = _button.interactable;
            _button.onClick.AddListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            UserMovementInfo = new UserMovementInfo(_movement);
            OnPressed.value = true;
        }
    }
}