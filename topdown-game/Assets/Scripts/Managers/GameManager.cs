using topdownGame.Utils;

namespace topdownGame.Managers
{

    public class GameManager : Singleton<GameManager>
    {

        public QueuedEventDispatcher GlobalDispatcher = new QueuedEventDispatcher();

        private void Awake()
        {
            //GlobalDispatcher.Emit(new OnGameStart());
            //UIManager.Show();
        }

        private void Update()
        {
            GlobalDispatcher.DispatchAll();
        }
    }
}