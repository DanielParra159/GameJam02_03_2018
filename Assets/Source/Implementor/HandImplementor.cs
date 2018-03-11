using System.IO;
using RockPaperScissors.DataSources;
using UnityEngine;

namespace RockPaperScissors.Implementor
{
    public class HandImplementor : MonoBehaviour, IHandComponent, IImplementor
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        private JSonGlobalData _jSonGlobalData;

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

                int index = (int) value;

                _spriteRenderer.sprite = _jSonGlobalData.HandSpritesData[index].Sprite;
            }
        }

        private void Start()
        {
            _jSonGlobalData = ReadGlobalData();
        }

        static JSonGlobalData ReadGlobalData()
        {
            string json = File.ReadAllText(DataConstants.DataPaths.GlobalDataPath);

            JSonGlobalData[] globalData = JsonHelper.getJsonArray<JSonGlobalData>(json);

            return globalData[0];
        }
    }
}