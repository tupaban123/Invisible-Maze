using InvisibleMaze.Configs;
using InvisibleMaze.GameConstants;
using InvisibleMaze.InputSystem;
using InvisibleMaze.Maze;
using InvisibleMaze.Player;
using InvisibleMaze.SceneLoading;
using UnityEngine;

namespace InvisibleMaze.CodeBase
{
    public class GameBootstrapper : MonoBehaviour
    {
        [SerializeField] private MazeView mazeView;
        [SerializeField] private PlayerView playerView;

        [SerializeField] private InputType inputType;
        [SerializeField] private Joystick playerJoystick;

        public void Start()
        {
#if UNITY_EDITOR
            if (inputType == InputType.Standalone)
            {
                IInputSystem standaloneInputSystem = new StandaloneInputSystem();
                ServiceLocator.Instance.Register<IInputSystem>(standaloneInputSystem);
                playerJoystick.gameObject.SetActive(false);
            }
            else
            {
                IInputSystem androidInputSystem = new AndroidInputSystem(playerJoystick);
                ServiceLocator.Instance.Register<IInputSystem>(androidInputSystem);
                playerJoystick.gameObject.SetActive(true);
            }
#elif PLATFORM_ANDROID
            IInputSystem androidInputSystem = new AndroidInputSystem(playerJoystick);
            ServiceLocator.Instance.Register<IInputSystem>(androidInputSystem);
            playerJoystick.gameObject.SetActive(true);            
#endif
            ServiceLocator.Instance.Get<ISceneLoader>().StartLoadScene += OnStartSceneLoad;

            int width = PlayerPrefs.GetInt(Constants.Width);
            int height = PlayerPrefs.GetInt(Constants.Height);

            MazeConfig mazeConfig = new MazeConfig(width, height);
            MazeType mazeType = (MazeType)PlayerPrefs.GetInt(Constants.MazeType);

            mazeView.Initialize(mazeConfig, mazeType);
            mazeView.SpawnMaze();

            playerView.Initialize();
        }

        private void OnDestroy()
        {
            ServiceLocator.Instance.Get<ISceneLoader>().StartLoadScene -= OnStartSceneLoad;
        }

        private void OnStartSceneLoad()
        {
            ServiceLocator.Instance.Unregister<IInputSystem>();
        }

        public void LoadMenu() => ServiceLocator.Instance.Get<ISceneLoader>().LoadMenu();
    }
}