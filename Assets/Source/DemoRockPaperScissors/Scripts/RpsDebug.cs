using UnityEngine;
using UnityEngine.UI;

namespace Source.DemoRockPaperScissors.Scripts
{
    public class RpsDebug : MonoBehaviour
    {
        [SerializeField] Button _debug;
        [SerializeField] bool _showConnectionDebug;

        public void ToggleConnectionDebug()
        {
            _showConnectionDebug = !_showConnectionDebug;
        }

        void Update()
        {
            if (_showConnectionDebug)
            {
                _debug.GetComponentInChildren<Text>().text = PhotonNetwork.connectionStateDetailed.ToString();
                return;
            }

            _debug.GetComponentInChildren<Text>().text = "";
        }
    }
}