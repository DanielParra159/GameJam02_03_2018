using System;
using UnityEngine;

namespace RockPaperScissors.DataSources
{
    [CreateAssetMenu(fileName = "GlobalConfiguration", menuName = "Data/GlobalConfiguration", order = 1)]
    public class GlobalData : ScriptableObject
    {
        public GlobalConfiguration data;
    }

    [Serializable]
    public class JSonGlobalData
    {
        public GlobalConfiguration.HandSprites[] HandSpritesData;
        public float AlphaDurationOnTextResult;

        public JSonGlobalData(GlobalData globalData)
        {
            HandSpritesData = new GlobalConfiguration.HandSprites[globalData.data.HandSpritesData.Length];
            for (int i = 0; i < globalData.data.HandSpritesData.Length; ++i)
            {
                HandSpritesData[i].Sprite = globalData.data.HandSpritesData[i].Sprite;
                HandSpritesData[i].UserMovement = globalData.data.HandSpritesData[i].UserMovement;
            }

            AlphaDurationOnTextResult = globalData.data.AlphaDurationOnTextResult;
        }
    }

    [Serializable]
    public class GlobalConfiguration
    {
        public HandSprites[] HandSpritesData;
        public float AlphaDurationOnTextResult;

        [Serializable]
        public struct HandSprites
        {
            public Sprite Sprite;
            public UserMovement UserMovement;
        }
    }
}