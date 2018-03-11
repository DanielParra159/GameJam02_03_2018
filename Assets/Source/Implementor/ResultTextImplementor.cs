using System.IO;
using RockPaperScissors.DataSources;
using Source.Component;
using TMPro;
using UnityEngine;
using DG.Tweening;

namespace RockPaperScissors.Implementor
{
    public class ResultTextImplementor : MonoBehaviour, ILerpAlphaColorComponent, ITextComponent, IImplementor
    {
        public bool SetVisibleAndDisappearWhenFinished
        {
            set
            {
                _text.DOColor(new Color(0, 0, 0, 1.0f), _jSonGlobalData.AlphaDurationOnTextResult)
                    .SetEase(Ease.OutQuart)
                    .OnComplete(() => { _text.color = Color.clear; });
            }
        }

        public string SetText
        {
            set { _text.SetText(value); }
        }

        [SerializeField] private TextMeshProUGUI _text;
        private JSonGlobalData _jSonGlobalData;

        private void Start()
        {
            _jSonGlobalData = ReadSceneData();
        }

        static JSonGlobalData ReadSceneData()
        {
            string json = File.ReadAllText(DataConstants.DataPaths.GlobalDataPath);

            JSonGlobalData[] globalData = JsonHelper.getJsonArray<JSonGlobalData>(json);

            return globalData[0];
        }
    }
}