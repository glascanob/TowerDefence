using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    Camera mainCamera;

    private void Start()
    {
        instance = this;
        mainCamera = GetComponent<Camera>();
    }

    public void ToggleCamera(int wave)
    {
        switch (wave)
        {
            case 1:
            case 2:
                mainCamera.orthographicSize = 6.5f;
                break;

            case 3:
            case 4:
                mainCamera.orthographicSize = 8.5f;
                break;

            case 5:
            case 6:
                mainCamera.orthographicSize = 10.5f;
                break;

            default:
                mainCamera.orthographicSize = 6.5f;
                break;
        }
    }

    //temporary!!! for testing purposes
    private void Update()
    {
        ToggleCamera(SpawnController.instance.wave);
    }
}
