using System.IO;
using UnityEngine;

namespace RockPaperScissors.DataSources
{
    [ExecuteInEditMode]
    public class SceneData : MonoBehaviour
    {
        static private bool serializedOnce;

        void Awake()
        {
            if (serializedOnce == false)
            {
                SerializeData();
            }
        }
        public void SerializeData()
        {
            serializedOnce = true;
            SceneDataSource[] data = GetComponents<SceneDataSource>();

            JSonSceneData[] spawningdata = new JSonSceneData[data.Length];

            for (int i = 0; i < data.Length; i++)
                spawningdata[i] = new JSonSceneData(data[i].SceneConfiguration);

            var json = JsonHelper.arrayToJson(spawningdata);

            Utility.Console.Log(json);

            File.WriteAllText(DataConstants.DataPaths.SceneDataPath, json);
            Debug.Log(DataConstants.DataPaths.SceneDataPath);
        }
    }
}