namespace InvisibleMaze.Maze
{
    public class MazePresenter
    {
        private MazeModel _mazeModel;

        public MazePresenter(MazeModel mazeModel)
        {
            _mazeModel = mazeModel;
        }

        public void GenerateMaze() => _mazeModel.GenerateMaze();
    }
}