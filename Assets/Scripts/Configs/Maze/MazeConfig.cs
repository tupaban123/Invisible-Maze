﻿using UnityEngine;

namespace InvisibleMaze.Configs
{
    public class MazeConfig
    {
        public MazeConfig(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public int Width { get; private set; }
        public int Height { get; private set; }
    }
}