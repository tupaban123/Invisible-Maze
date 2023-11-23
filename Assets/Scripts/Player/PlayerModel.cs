using InvisibleMaze.CodeBase;
using InvisibleMaze.Configs;
using InvisibleMaze.SceneLoading;
using UnityEngine;

namespace InvisibleMaze.Player
{
    public class PlayerModel
    {
        private PlayerConfig _playerConfig;

        private PlayerView _playerView;

        public PlayerModel(PlayerView playerView, PlayerConfig playerConfig)
        {
            _playerView = playerView;
            _playerConfig = playerConfig;
        }

        public void OnMoveInput(Vector2 direction)
        {
            Vector2 velocity = direction * _playerConfig.PlayerSpeed;
            _playerView.SetVelocity(velocity);
        }
        
        public void OnMoveInput(float horizontal, float vertical)
        {
            Vector2 direction = new Vector2(horizontal, vertical);

            OnMoveInput(direction);
        }

        public void TriggerEnter(Collider2D collision)
        {
            if (collision.CompareTag("WinZone"))
                ServiceLocator.Instance.Get<ISceneLoader>().Restart();
        }
    }
}