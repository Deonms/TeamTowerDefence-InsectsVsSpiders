using UnityEngine;
using System;
using Unity.Cinemachine;

public class CameraMover : MonoBehaviour
{
    private float _speed = 1.67f; // six seven lol gottem
    public CinemachineCamera cam1;
    public CinemachineCamera cam2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        print("movement script is loaded in, this is going to shift the industry!");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            print("up.. damn good movie that was");

            Vector3 position = new Vector3(0, 0, 0);
            transform.position += (new Vector3(0, _speed, 0) * Time.deltaTime * _speed);
        }

        if (Input.GetKey(KeyCode.A))
        {
            print("looking left cuz this aint right");

            Vector3 position = new Vector3(0, 0, 0);
            transform.position += (new Vector3(-_speed, 0, 0) * Time.deltaTime * _speed);
        }

        if (Input.GetKey(KeyCode.S))
        {
            print("you down?");

            Vector3 position = new Vector3(0, 0, 0);
            transform.position += (new Vector3(0, -_speed, 0) * Time.deltaTime * _speed);
        }

        if (Input.GetKey(KeyCode.D))
        {
            print("yeah right");

            Vector3 position = new Vector3(0, 0, 0);
            transform.position += (new Vector3(_speed, 0, 0) * Time.deltaTime * _speed);
        }

        // camera switching test, temporary

        if (Input.GetKey(KeyCode.Alpha1))
        {
            CameraManager.SwitchCamera(cam1);
        }

        if (Input.GetKey(KeyCode.Alpha2))
        {
            CameraManager.SwitchCamera(cam1);
        }
    }
}
