using InvisibleMaze.Configs;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InvisibleMaze.Maze
{
    public class MazeView : MonoBehaviour
    {
        [Header("Prefabs")]
        [SerializeField] private GameObject cellPrefab;
        [SerializeField] private GameObject winZone;

        [Header("Colors")]
        [SerializeField] private Color visibleColor;
        [SerializeField] private Color invisibleColor;
        [SerializeField] private float speed;
        [SerializeField] private float waitTimeInSeconds;

        private MazeModel _mazeModel;
        private MazePresenter _mazePresenter;

        private List<MazeViewCell> cells = new List<MazeViewCell>();
		
		public static event Action<Transform> OnMazeCreated;

        public void Initialize(MazeConfig config, MazeType mazeType)
        {
            _mazeModel = new MazeModel(this, config, mazeType);
            _mazePresenter = new MazePresenter(_mazeModel);
        }

        public void SpawnMaze()
        {
            _mazePresenter.GenerateMaze();

            var config = _mazeModel.GetMazeConfig();

            Vector2 furthestCellCoords = _mazeModel.GetFurthestCellCoords();
            var maze = _mazeModel.GetMaze();

            for (int x = 0; x < maze.GetLength(0); x++)
            {
                for (int y = 0; y < maze.GetLength(1); y++)
                {
                    MazeViewCell c = Instantiate(cellPrefab, new Vector2(x, y) * 1.5f, Quaternion.identity).GetComponent<MazeViewCell>();

                    c.transform.SetParent(transform);

                    if (!maze[x, y].WallLeft && !maze[x, y].WallBottom)
                    {
                        Destroy(c.gameObject);
                        continue;
                    }

                    c.DisableWalls(maze[x, y].WallLeft, maze[x, y].WallBottom);
                    cells.Add(c);
                }
            }

            Vector2 winZoneCoord = furthestCellCoords * 1.5f;

            if (furthestCellCoords.y == 0)
            {
                winZoneCoord += new Vector2(0.75f, -0.8f);
            }
            else if (furthestCellCoords.y == config.Height - 2)
            {
                winZoneCoord += new Vector2(0.75f, 0.8f * 3);
            }
            else if (furthestCellCoords.x == 0)
            {
                winZoneCoord += new Vector2(-0.75f, 0.8f);
            }
            else if (furthestCellCoords.x == config.Width - 2)
            {
                winZoneCoord += new Vector2(0.8f * 3, 0.75f);
            }

            var winZoneObject = Instantiate(winZone, winZoneCoord, Quaternion.identity);
			OnMazeCreated?.Invoke(winZoneObject.transform);
			
            StartCoroutine(ColorChanging());
        }

        private IEnumerator ColorChanging()
        {
            while (true)
            {
                float t = 0;
                var currentColor = visibleColor;
                yield return new WaitForSeconds(waitTimeInSeconds);

                while (t <= 1)
                {
                    t += Time.deltaTime * speed;

                    currentColor = Color.Lerp(visibleColor, invisibleColor, t);

                    foreach (var cell in cells)
                        cell.ChangeColor(currentColor);

                    yield return null;
                }

                t = 0;
                yield return new WaitForSeconds(waitTimeInSeconds);

                while (t <= 1)
                {
                    t += Time.deltaTime * speed;

                    currentColor = Color.Lerp(invisibleColor, visibleColor, t);

                    foreach (var cell in cells)
                        cell.ChangeColor(currentColor);

                    yield return null;
                }

                yield return null;
            }
        }
    }
}