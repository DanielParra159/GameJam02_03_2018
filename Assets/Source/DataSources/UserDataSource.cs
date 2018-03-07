using System;
using RockPaperScissors.Implementor;
using UnityEngine;

namespace RockPaperScissors.DataSources {
    public class UserDataSource : MonoBehaviour
    {
        public UserConfiguration data;
    }

    [Serializable]
    public class JSonUserData
    {
        public HandImplementor player1HandImplementor;
        public HandImplementor player2HandImplementor;

        public JSonUserData(UserConfiguration spawnData)
        {
            player1HandImplementor = spawnData.player1HandImplementor;
            player2HandImplementor = spawnData.player2HandImplementor;
        }
    }
    
    [Serializable]
    public class UserConfiguration
    {
        public HandImplementor player1HandImplementor;
        public HandImplementor player2HandImplementor;
    }
}