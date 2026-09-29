using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Zorgt ervoor dat er maar één LevelManager gebruikt kan worden
    public static LevelManager Main;

    [SerializeField] private Transform _startPoint;
    public Transform[] Path;

    private void Awake()
    {
        // Zet deze LevelManager als de hoofd-LevelManager
        Main = this;
    }
}
