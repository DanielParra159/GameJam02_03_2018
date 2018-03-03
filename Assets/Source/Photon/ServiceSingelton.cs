using Boo.Lang;
using Photon;
using UnityEngine;

namespace Source.Photon
{
    public sealed partial class ServiceSingelton
    {
        private static ServiceSingelton _instance;

        public static ServiceSingelton Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ServiceSingelton();
                }

                return _instance;
            }

            private set { _instance = value; }
        }

        ServiceSingelton()
        {
        }

        public IRoom CreatePun(float turnDuration)
        {
            var go = new GameObject(typeof(ServiceSingelton).Name);
            var component = go.AddComponent<PhotonServiceMonoBehaviour>();
            component.Construct(turnDuration);
            return component;
        }
    }
}