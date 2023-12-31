using InvisibleMaze.Configs;
using System.Collections.Generic;
using UnityEngine;

namespace InvisibleMaze.Maze.Algorithms
{
    public class PrimsAlgorithmMazeGenerator : MazeGenerator
    {
        public override void GenerateMaze(MazeGeneratorCell[,] maze, MazeConfig config)
        {
            int Width = config.Width;
            int Height = config.Height;

            int randomStartX = Random.Range(0, Width);
            int randomStartY = Random.Range(0, Height);

            MazeGeneratorCell startCell = maze[randomStartX, randomStartY];
            startCell.Visited = true;

            List<MazeGeneratorCell> list = new List<MazeGeneratorCell>();

            MazeGeneratorCell current = startCell;

            do
            {
                if (list.Count > 0)
                    current = list[Random.Range(0, list.Count)];

                int x = current.X;
                int y = current.Y;

                if (x > 0 && !maze[x - 1, y].Visited && !list.Contains(maze[x - 1, y])) list.Add(maze[x - 1, y]);
                if (y > 0 && !maze[x, y - 1].Visited && !list.Contains(maze[x, y - 1])) list.Add(maze[x, y - 1]);
                if (x < Width - 2 && !maze[x + 1, y].Visited && !list.Contains(maze[x + 1, y])) list.Add(maze[x + 1, y]);
                if (y < Height - 2 && !maze[x, y + 1].Visited && !list.Contains(maze[x, y + 1])) list.Add(maze[x, y + 1]);

                current.Visited = true;
                list.Remove(current);
                current.DistanceFromStart = list.Count;

                List<MazeGeneratorCell> chosenCellVisitedNeighbours = new List<MazeGeneratorCell>();

                if (x > 0 && maze[x - 1, y].Visited) chosenCellVisitedNeighbours.Add(maze[x - 1, y]);
                if (y > 0 && maze[x, y - 1].Visited) chosenCellVisitedNeighbours.Add(maze[x, y - 1]);
                if (x < Width - 2 && maze[x + 1, y].Visited) chosenCellVisitedNeighbours.Add(maze[x + 1, y]);
                if (y < Height - 2 && maze[x, y + 1].Visited) chosenCellVisitedNeighbours.Add(maze[x, y + 1]);

                Debug.Log(chosenCellVisitedNeighbours.Count);

                if (chosenCellVisitedNeighbours.Count == 0)
                    continue;

                var randomCellNeighbour = chosenCellVisitedNeighbours[/*Random.Range(*/0/*, chosenCellVisitedNeighbours.Count)*/];

                RemoveWall(current, randomCellNeighbour);

            } while (list.Count > 0);
        }
    }
}