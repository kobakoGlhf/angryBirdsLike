using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.U2D;

public class CameraController : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera _cinemachineCamera;
    [SerializeField] float _dragSpeed;
    [SerializeField] float _scrollSpeed;
    [SerializeField] float _maxCameraScale;
    [SerializeField] float _minCameraScale;
    [SerializeField]Camera _camera;
    // Update is called once per frame
    private void Start()
    {
    }
    void Update()
    {
        Vector3 camPos = _camera.transform.position;
        if (Input.GetMouseButton(2))
        {
            camPos.x -= Input.GetAxis("Mouse X") * _dragSpeed * Time.deltaTime;
            camPos.y -= Input.GetAxis("Mouse Y") * _dragSpeed * Time.deltaTime;
        }
        _cinemachineCamera.transform.position = camPos;
        var nextChangeScale= Input.mouseScrollDelta.y*_scrollSpeed;
        if(_cinemachineCamera.m_Lens.OrthographicSize-nextChangeScale > _maxCameraScale|| 
            _cinemachineCamera.m_Lens.OrthographicSize-nextChangeScale < _minCameraScale)
        {return;
        }
        else _cinemachineCamera.m_Lens.OrthographicSize -= nextChangeScale;
    }
}
