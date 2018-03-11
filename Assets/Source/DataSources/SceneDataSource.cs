using System;
using RockPaperScissors.Implementor;
using UnityEngine;

namespace RockPaperScissors.DataSources {
    public class SceneDataSource : MonoBehaviour
    {
        public SceneConfiguration SceneConfiguration;
        
    }

    [Serializable]
    public class JSonSceneData
    {
        public SceneConfiguration.UserConfiguration UserConfig;
        public SceneConfiguration.ResultTextConfiguration ResultTextConfig;

        public JSonSceneData(SceneConfiguration spawnData)
        {
            UserConfig.Player1HandImplementor = spawnData.UserConfig.Player1HandImplementor;
            UserConfig.Player2HandImplementor = spawnData.UserConfig.Player2HandImplementor;
            ResultTextConfig.ResultTextImplementor = spawnData.ResultTextConfig.ResultTextImplementor;
        }
    }
    
    [Serializable]
    public class SceneConfiguration
    {
        public UserConfiguration UserConfig;
        public ResultTextConfiguration ResultTextConfig;
        
        [Serializable]
        public struct UserConfiguration
        {
            public HandImplementor Player1HandImplementor;
            public HandImplementor Player2HandImplementor;
        }
        
        [Serializable]
        public struct ResultTextConfiguration
        {
            public ResultTextImplementor ResultTextImplementor;
        }
    }
    
}