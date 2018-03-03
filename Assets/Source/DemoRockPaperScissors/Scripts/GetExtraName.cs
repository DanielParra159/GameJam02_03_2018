using UnityEngine;

namespace Source.DemoRockPaperScissors.Scripts
{
    public class GetExtraName : MonoBehaviour
    {
        void Start()
        {
            string yourName = "";

            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

            AndroidJavaObject intent = currentActivity.Call<AndroidJavaObject>("getIntent");
            bool hasExtra = intent.Call<bool>("hasExtra", "namestring");

            if (hasExtra)
            {
                AndroidJavaObject extras = intent.Call<AndroidJavaObject>("getExtras");
                yourName = extras.Call<string>("getString", "namestring");
            }
        }
    }
}