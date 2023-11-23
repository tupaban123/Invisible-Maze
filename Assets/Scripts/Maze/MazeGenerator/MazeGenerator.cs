using InvisibleMaze.Configs;

public abstract class MazeGenerator
{
    public abstract void GenerateMaze(MazeGeneratorCell[,] maze, MazeConfig config);

    protected virtual void RemoveWall(MazeGeneratorCell a, MazeGeneratorCell b)
    {
        if(a.X == b.X)
        {
            if (a.Y > b.Y)
                a.WallBottom = false;
            else
                b.WallBottom = false;
        }
        else
        {
            if (a.X > b.X)
                a.WallLeft = false;
            else
                b.WallLeft = false;
        }
    }
}
