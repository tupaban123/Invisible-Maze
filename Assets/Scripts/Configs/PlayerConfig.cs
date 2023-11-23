using UnityEngine;

namespace InvisibleMaze.Configs
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/Player Config")]
    public class PlayerConfig : ScriptableObject
    {
        [field: SerializeField] public float PlayerSpeed { get; private set; }
    }
}