using InvisibleMaze.Configs;
using System.Collections.Generic;
using UnityEngine;

namespace InvisibleMaze.Maze.Algorithms
{
    public class RecursiveBacktrackerMazeGenerator : MazeGenerator
    {
        public override void GenerateMaze(MazeGeneratorCell[,] maze, MazeConfig config)
        {
            int Width = config.Width;
            int Height = config.Height;

            MazeGeneratorCell current = maze[0, 0];
            current.Visited = true;
            current.DistanceFromStart = 0;

            Stack<MazeGeneratorCell> stack = new Stack<MazeGeneratorCell>();

            do
            {
                List<MazeGeneratorCell> unvisitedNeighbours = new List<MazeGeneratorCell>();

                int x = current.X;
                int y = current.Y;

                if (x > 0 && !maze[x - 1, y].Visited) unvisitedNeighbours.Add(maze[x - 1, y]);
                if (y > 0 && !maze[x, y - 1].Visited) unvisitedNeighbours.Add(maze[x, y - 1]);
                if (x < Width - 2 && !maze[x + 1, y].Visited) unvisitedNeighbours.Add(maze[x + 1, y]);
                if (y < Height - 2 && !maze[x, y + 1].Visited) unvisitedNeighbours.Add(maze[x, y + 1]);

                if (unvisitedNeighbours.Count > 0)
                {
                    MazeGeneratorCell chosen = unvisitedNeighbours[Random.Range(0, unvisitedNeighbours.Count)];
                    RemoveWall(current, chosen);

                    chosen.Visited = true;
                    stack.Push(chosen);
                    current = chosen;
                    chosen.DistanceFromStart = stack.Count;
                }
                else
                {
                    current = stack.Pop();
                }
            }
            while (stack.Count > 0);
        }
    }
}