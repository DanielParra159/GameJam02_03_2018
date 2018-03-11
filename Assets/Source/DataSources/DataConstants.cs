using UnityEngine;

namespace RockPaperScissors.DataSources
{
    public static class DataConstants
    {
        public static class DataPaths
        {
            public static readonly string DataPath = Application.persistentDataPath;
            public static readonly string SceneDataPath = DataPath + "/SceneData.json";
            public static readonly string GlobalDataPath = DataPath + "/GlobalData.json";
        }

        public static class ResultTexts
        {
            public static readonly string Win = "YOU WIN";
            public static readonly string Lose = "YOU LOSE";
            public static readonly string Draw = "DRAW";
        }
    }
}