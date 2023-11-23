using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MazeViewCell : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private EdgeCollider2D edgeCollider;

    public void DisableWalls(bool leftWall, bool bottomWall)
    {
        if (leftWall && bottomWall)
            return;
        else if (!leftWall && !bottomWall)
            Destroy(gameObject);



        Vector3[] positionsArr = new Vector3[3];
        lineRenderer.GetPositions(positionsArr);

        List<Vector3> positionsList = positionsArr.ToList();

        if (!leftWall)
            positionsList.RemoveAt(0);
        else if (!bottomWall)
            positionsList.RemoveAt(positionsList.Count - 1);
        
        lineRenderer.positionCount = positionsList.Count;
        lineRenderer.SetPositions(positionsList.ToArray());

        edgeCollider.SetPoints(Vector3ListToVector2List(positionsList));
    }

    public void ChangeColor(Color color)
    {
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
    }

    private List<Vector2> Vector3ListToVector2List(List<Vector3> vector3List)
    {
        List<Vector2> vector2List = new List<Vector2>();

        foreach(var vector3 in vector3List)
        {
            vector2List.Add(vector3);
        }

        return vector2List;
    }
}