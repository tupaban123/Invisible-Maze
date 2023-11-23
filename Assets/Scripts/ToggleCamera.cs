using UnityEngine;
using Cinemachine;

public class ToggleCamera : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cinemachineCam;
    [SerializeField] private CinemachineBrain cinemachineBrain;

    [SerializeField] private Camera cam;

    [SerializeField] private float cameraSizeAtPlayerMode;
    [SerializeField] private float cameraSizeAtAllMode;

    [SerializeField] private Vector3 cameraPosAtAllMode;

    private bool isPlayerMode = true;

    public void ToggleCameraMode()
    {
        isPlayerMode = !isPlayerMode;

        if(isPlayerMode)
        {
            cinemachineBrain.enabled = true;
            cinemachineCam.m_Lens.OrthographicSize = cameraSizeAtPlayerMode;
            cinemachineCam.gameObject.SetActive(true);

            cam.orthographicSize = cameraSizeAtPlayerMode;
        }
        else
        {
            cinemachineBrain.enabled = false;
            cinemachineCam.m_Lens.OrthographicSize = cameraSizeAtAllMode;
            cinemachineCam.gameObject.SetActive(false);

            cam.transform.position = cameraPosAtAllMode;
            cam.orthographicSize = cameraSizeAtAllMode;
        }
    }
}
