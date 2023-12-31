using UnityEngine;
using InvisibleMaze.Maze;

public class WinZoneMarker : MonoBehaviour
{
	[SerializeField] private float rotationSpeed = 1f; 
    private Transform _winZone;
	
	private void Start() => MazeView.OnMazeCreated += InitializeWinZone;
	
	private void OnDestroy() => MazeView.OnMazeCreated -= InitializeWinZone;
	
	public void InitializeWinZone(Transform winZone) => _winZone = winZone;
	
    void Update()
    {
        if(_winZone != null)
		{
			Vector2 direction = _winZone.position - transform.position;
			float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90f;
			Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
			transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
		}
	}
}
