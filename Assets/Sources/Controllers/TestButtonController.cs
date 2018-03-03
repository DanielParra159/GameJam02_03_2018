using UnityEngine;
using UnityEngine.UI;

namespace Sources.Controllers
{
    public class TestButtonController : MonoBehaviour
    {
        [SerializeField]
        private Button b;
        void Start()
        {
           b.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
             Contexts.sharedInstance.game.CreateEntity().AddTestButtonConsume(10);
        }
    }
}