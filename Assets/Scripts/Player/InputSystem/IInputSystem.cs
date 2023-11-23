using InvisibleMaze.CodeBase;

namespace InvisibleMaze.InputSystem
{
    public interface IInputSystem : IService
    {
        float Horizontal { get; }
        float Vertical { get; }
    }
}