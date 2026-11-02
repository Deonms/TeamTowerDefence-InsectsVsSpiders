using UnityEngine;

public class MeleeBug : BugAim
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (_Target != null)
            lastTargetPosition = _Target.position;
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }
}
