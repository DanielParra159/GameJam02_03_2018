using System.IO;
using UnityEditor;
using UnityEngine;

namespace RockPaperScissors.DataSources.Editor
{
    [CustomEditor(typeof(GlobalData))]
    public class GlobalDataEditor : UnityEditor.Editor
    {
        private SerializedProperty _data;
        private SerializedProperty _handSpritesData;

        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(_data, true);

            serializedObject.ApplyModifiedProperties();

            if (GUILayout.Button("Serialize"))
            {
                Serialize();
            }
        }

        private void Serialize()
        {
            GlobalData globalData = (GlobalData) target;

            JSonGlobalData[] spawningdata = new JSonGlobalData[1];

            spawningdata[0] = new JSonGlobalData(globalData);

            string json = JsonHelper.arrayToJson(spawningdata);

            Utility.Console.Log(json);

            File.WriteAllText(DataConstants.GlobalDataPath, json);
            Debug.Log(DataConstants.GlobalDataPath);
        }

        private void OnEnable()
        {
            _data = serializedObject.FindProperty("data");
        }
    }
}