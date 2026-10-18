using Unity.Cinemachine;
using UnityEngine;

public class CameraManager1 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        CameraManager.Register(GetComponent<CinemachineCamera>());
    }

    private void OnDisable()
    {
        CameraManager.UnRegister(GetComponent<CinemachineCamera>());
    }
}
