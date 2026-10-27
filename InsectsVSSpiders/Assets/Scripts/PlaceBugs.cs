using UnityEngine;

public class PlaceBugs : MonoBehaviour
{
    
    private bool _Draging;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!_Draging) return;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePos.x,mousePos.y,-2);
    }

    private void OnMouseDown()
    {
        _Draging = true;
    }
    private void OnMouseUp()
    {
        _Draging = false;
        
    }
}
