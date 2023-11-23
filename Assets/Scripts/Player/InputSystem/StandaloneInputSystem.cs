using InvisibleMaze.InputSystem;
using UnityEngine;

namespace InvisibleMaze.InputSystem
{
    public class StandaloneInputSystem : IInputSystem
    {
        public float Horizontal => Input.GetAxis("Horizontal");

        public float Vertical => Input.GetAxis("Vertical");
    }
}