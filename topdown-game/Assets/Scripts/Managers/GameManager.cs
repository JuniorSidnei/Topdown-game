using topdownGame.Utils;
using UnityEngine.SceneManagement;

namespace topdownGame.Managers
{

    public class GameManager : Singleton<GameManager>
    {

        public QueuedEventDispatcher GlobalDispatcher = new QueuedEventDispatcher();

        private void Awake() {
            //GlobalDispatcher.Emit(new OnGameStart());
            SceneManager.LoadScene("HUD", LoadSceneMode.Additive);
        }

        private void Update()
        {
            GlobalDispatcher.DispatchAll();
        }
    }
}