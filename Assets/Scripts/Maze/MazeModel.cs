using InvisibleMaze.Configs;
using System;
using System.Collections.Generic;
using UnityEngine;
using InvisibleMaze.Maze.Algorithms;

namespace InvisibleMaze.Maze
{
    public class MazeModel
    {
        private MazeConfig _config;
        private MazeView _mazeView;
        private MazeType _mazeType;

        private MazeGeneratorCell[,] _maze;
        private Vector2 _furthestCellCoords;

        public MazeModel(MazeView mazeView, MazeConfig config, MazeType mazeType)
        {
            _mazeView = mazeView;
            _config = config;
            _mazeType = mazeType;
        }

        public void GenerateMaze()
        {
            int width = _config.Width;
            int height = _config.Height;

            var generator = CreateMazeGenerator();
            _maze = new MazeGeneratorCell[width, height];

            for (int x = 0; x < _maze.GetLength(0); x++)
            {
                for (int y = 0; y < _maze.GetLength(1); y++)
                {
                    _maze[x, y] = new MazeGeneratorCell { X = x, Y = y };
                }
            }

            for (int x = 0; x < _maze.GetLength(0); x++)
            {
                _maze[x, height - 1].WallLeft = false;
            }

            for (int y = 0; y < _maze.GetLength(1); y++)
            {
                _maze[width - 1, y].WallBottom = false;
            }

            generator.GenerateMaze(_maze, _config);
            _furthestCellCoords = PlaceMazeExit();
        }

        private Vector2 PlaceMazeExit()
        {
            int width = _config.Width;
            int height = _config.Height;

            MazeGeneratorCell furthest = _maze[0, 0];

            for (int x = 0; x < _maze.GetLength(0); x++)
            {
                if (_maze[x, height - 2].DistanceFromStart > furthest.DistanceFromStart) furthest = _maze[x, height - 2];
                if (_maze[x, 0].DistanceFromStart > furthest.DistanceFromStart) furthest = _maze[x, 0];
            }

            for (int y = 0; y < _maze.GetLength(1); y++)
            {
                if (_maze[width - 2, y].DistanceFromStart > furthest.DistanceFromStart) furthest = _maze[width - 2, y];
                if (_maze[0, y].DistanceFromStart > furthest.DistanceFromStart) furthest = _maze[0, y];
            }

            if (furthest.X == 0)
                furthest.WallLeft = false;
            else if (furthest.Y == 0)
                furthest.WallBottom = false;
            else if (furthest.X == width - 2)
                _maze[furthest.X + 1, furthest.Y].WallLeft = false;
            else if (furthest.Y == height - 2)
                _maze[furthest.X, furthest.Y + 1].WallBottom = false;

            return new Vector2(furthest.X, furthest.Y);
        }

        private MazeGenerator CreateMazeGenerator()
        {
            if (_mazeType == MazeType.RecursiveBacktracker)
                return new RecursiveBacktrackerMazeGenerator();
            else if (_mazeType == MazeType.GrowingTreeRandom)
                return new GrowingTreeRandomMazeGenerator();
            else if (_mazeType == MazeType.GrowingTreeOldest)
                return new GrowingTreeOldestMazeGenerator();
            else if(_mazeType == MazeType.PrimsAlgorithm)
                return new PrimsAlgorithmMazeGenerator();

            return null;
        }

        public Vector2 GetFurthestCellCoords() => _furthestCellCoords;

        public MazeGeneratorCell[,] GetMaze() => _maze;

        public MazeConfig GetMazeConfig() => _config;
    }
}