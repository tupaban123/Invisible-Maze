using UnityEngine;

namespace InvisibleMaze.Player
{
    public class PlayerPresenter
    {
        private PlayerModel _playerModel;

        public PlayerPresenter(PlayerModel playerModel)
        {
            _playerModel = playerModel;
        }

        public void OnMoveInput(Vector2 direction)
        {
            _playerModel.OnMoveInput(direction);
        }
        
        public void OnMoveInput(float horizontal, float vertical)
        {
            _playerModel.OnMoveInput(horizontal, vertical);
        }

        public void OnTriggerEnter(Collider2D collision)
        {
            _playerModel.TriggerEnter(collision);
        }
    }
}