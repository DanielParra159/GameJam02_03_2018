using UnityEngine;

namespace RockPaperScissors.DataSources
{
    public static class DataConstants
    {
        public static readonly string DataPath = Application.persistentDataPath;
        public static readonly string UserDataPath = DataPath + "/UserData.json";
        public static readonly string GlobalDataPath = DataPath + "/GlobalData.json";
    }
}