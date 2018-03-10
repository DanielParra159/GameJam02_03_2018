using System.IO;
using UnityEngine;

namespace RockPaperScissors.DataSources
{
    [ExecuteInEditMode]
    public class UserData : MonoBehaviour
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
            UserDataSource[] data = GetComponents<UserDataSource>();

            JSonUserData[] spawningdata = new JSonUserData[data.Length];

            for (int i = 0; i < data.Length; i++)
                spawningdata[i] = new JSonUserData(data[i].data);

            var json = JsonHelper.arrayToJson(spawningdata);

            Utility.Console.Log(json);

            File.WriteAllText(DataConstants.UserDataPath, json);
            Debug.Log(DataConstants.UserDataPath);
        }
    }
}