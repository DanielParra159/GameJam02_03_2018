using UnityEngine;

namespace RockPaperScissors.Implementor
{
    public class HandImplementor : MonoBehaviour, IHandComponent, IImplementor
    {
        [SerializeField] private Animator _animator;

        public Animations SetAnimationTrigger
        {
            set { _animator.SetTrigger(value.ToString()); }
        }

    }
}