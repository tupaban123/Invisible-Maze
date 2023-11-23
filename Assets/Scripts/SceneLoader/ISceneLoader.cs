using System;

using InvisibleMaze.CodeBase;

namespace InvisibleMaze.SceneLoading
{
    public interface ISceneLoader : IService
    {
        event Action<float> ProgressChanged;
        event Action StartLoadScene;

        void LoadMenu();
        void LoadGame();
        void Restart();
    }
}