using InvisibleMaze.CodeBase;
using InvisibleMaze.Configs;
using InvisibleMaze.InputSystem;
using UnityEngine;

namespace InvisibleMaze.Player
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private PlayerConfig playerConfig;
        [SerializeField] private Rigidbody2D rb;

        private PlayerModel _playerModel;
        private PlayerPresenter _playerPresenter;

        private IInputSystem _inputSystem;

        public void Initialize()
        {
            _playerModel = new PlayerModel(this, playerConfig);
            _playerPresenter = new PlayerPresenter(_playerModel);

            _inputSystem = ServiceLocator.Instance.Get<IInputSystem>();
        }

        private void Update()
        {
            _playerPresenter.OnMoveInput(_inputSystem.Horizontal, _inputSystem.Vertical);
        }

        public void SetVelocity(Vector2 velocity)
        {
            rb.velocity = velocity;
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            _playerPresenter.OnTriggerEnter(collision);
        }
    }
}