using UnityEngine;
using DG.Tweening;
public class CameraFollow : MonoBehaviour
{
    private static Vector3 cameraPos;

    private Camera _camera;
    private GameObject _shapeSpawner;

    private void Awake()
    {
        _camera = Camera.main;
        _shapeSpawner = GameObject.FindGameObjectWithTag("ShapeSpawner");
    }

    private void Start()
    {
        SetCameraStartingPosition();
    }

    private void LateUpdate()
    {
        CameraMove();
    }
    
    private void CameraMove()
    {
        Vector3 endPos;
        if (!Ball.isMoving)
        {
             endPos = new Vector3(0, GetCameraYPos(), -10);
            _camera.transform.DOMove(endPos,0.5f);
        }
        else
        {
            endPos = new Vector3(0, Ball.GetBallPos().y+2.5f, -10);
            _camera.transform.DOMove(endPos, 0.25f);
        }
        
    }

    private float GetCameraYPos()
    {
        cameraPos.y = _shapeSpawner.gameObject.transform.childCount * 0.5f +3;
        return cameraPos.y;
    }

    private void SetCameraStartingPosition()
    {
        cameraPos = new Vector3(0, GetCameraYPos(), -10);
        _camera.transform.position = cameraPos;
    }
    
    
}
