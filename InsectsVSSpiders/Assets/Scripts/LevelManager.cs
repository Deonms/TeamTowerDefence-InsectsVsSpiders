using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Main;

    [SerializeField] private Transform _startPoint;
    public Transform[] _path;

    private void Awake()
    {
        Main = this;
    }
}
