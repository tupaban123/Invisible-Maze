using UnityEngine;

namespace InvisibleMaze.CodeBase
{
    public class BootstrapperMain : MonoBehaviour, ICoroutineRunner
    {
        private Game _game;

        private void Awake()
        {
            _game = new Game(this);
            _game.RegisterServices();
            _game.LoadMenuScene();

            DontDestroyOnLoad(this);
        }
    }
}