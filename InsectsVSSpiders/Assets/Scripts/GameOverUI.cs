using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _wavesText;

    private void Start()
    {
        int waves = GameManager.Main.WavesSurvived;

        _wavesText.text = "Waves survived: " + waves;
    }
}
