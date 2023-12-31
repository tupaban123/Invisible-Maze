using InvisibleMaze.Configs;
using System.Collections.Generic;
using UnityEngine;

namespace InvisibleMaze.Maze.Algorithms
{
    public class GrowingTreeRandomMazeGenerator : MazeGenerator
    {
        public override void GenerateMaze(MazeGeneratorCell[,] maze, MazeConfig config)
        {
            int width = config.Width;
            int height = config.Height;

            MazeGeneratorCell current = maze[0, 0];
            current.Visited = true;
            current.DistanceFromStart = 0;

            List<MazeGeneratorCell> list = new List<MazeGeneratorCell>();

            do
            {
                List<MazeGeneratorCell> unvisitedNeighbours = new List<MazeGeneratorCell>();

                int x = current.X;
                int y = current.Y;

                if (x > 0 && !maze[x - 1, y].Visited) unvisitedNeighbours.Add(maze[x - 1, y]);
                if (y > 0 && !maze[x, y - 1].Visited) unvisitedNeighbours.Add(maze[x, y - 1]);
                if (x < width - 2 && !maze[x + 1, y].Visited) unvisitedNeighbours.Add(maze[x + 1, y]);
                if (y < height - 2 && !maze[x, y + 1].Visited) unvisitedNeighbours.Add(maze[x, y + 1]);

                if (unvisitedNeighbours.Count > 0)
                {
                    MazeGeneratorCell chosen = unvisitedNeighbours[Random.Range(0, unvisitedNeighbours.Count)];
                    RemoveWall(current, chosen);

                    chosen.Visited = true;
                    list.Add(chosen);
                    current = chosen;
                    chosen.DistanceFromStart = list.Count;
                }
                else
                {
                    var indexToGet = Random.Range(0, list.Count);
                    current = list[indexToGet];
                    list.RemoveAt(indexToGet);
                }
            }
            while (list.Count > 0);
        }
    }
}