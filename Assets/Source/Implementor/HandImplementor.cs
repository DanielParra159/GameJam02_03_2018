using System;
using UnityEngine;

namespace RockPaperScissors.Implementor
{
    public class HandImplementor : MonoBehaviour, IHandComponent, IImplementor
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private HandSprites[] _handSprites;

        public Animations SetAnimationTrigger
        {
            set
            {
                _animator.enabled = true;
                _animator.SetTrigger(value.ToString());
            }
        }

        public UserMovement SetMovementSprite
        {
            set
            {
                _animator.enabled = false;
                
                //TODO: Quickly hack
                int index = (int) value-1;
                _spriteRenderer.sprite = _handSprites[index].Sprite;
            }
        }
    }
    
    //TODO: move to json
    [Serializable]
    public class HandSprites
    {
        public Sprite Sprite;
        public UserMovement UserMovement;
    }
}