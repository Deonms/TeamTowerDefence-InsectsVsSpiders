// six seven lol gottem

using UnityEngine;

public class CameraMover : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            // print("up.. damn good movie that was");
            Vector3 position = new Vector3(0, 0, 0);
            transform.position += (new Vector3(0, _speed, 0) * Time.deltaTime * _speed);
            transform.rotation = Quaternion.Euler(0, 270, 0);
        }

        if (Input.GetKey(KeyCode.A))
        {
            // print("looking left cuz this aint right");
            Vector3 position = new Vector3(0, 0, 0);
            transform.position += (new Vector3(-_speed, 0, 0) * Time.deltaTime * _speed);
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        if (Input.GetKey(KeyCode.S))
        {
            // print("you down?");
            Vector3 position = new Vector3(0, 0, 0);
            transform.position += (new Vector3(0, -_speed, 0) * Time.deltaTime * _speed);
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }

        if (Input.GetKey(KeyCode.D))
        {
            // print("yeah right");
            Vector3 position = new Vector3(0, 0, 0);
            transform.position += (new Vector3(_speed, 0, 0) * Time.deltaTime * _speed);
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
