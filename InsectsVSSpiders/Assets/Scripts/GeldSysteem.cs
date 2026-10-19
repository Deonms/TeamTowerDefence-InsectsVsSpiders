using TMPro;
using UnityEngine;

public class GeldSyteem : MonoBehaviour
{
    [Header("Money of each Team")]
    [SerializeField] private double _amountOfMoneySpider = 0;
    [SerializeField] private TMP_Text _amountOfSpiderDisplay;
    [SerializeField] private double _amountOfMoneyInsect = 0;
    [SerializeField] private TMP_Text _amountOfInsectDisplay;
    [SerializeField] private int _timer = 0;
    [SerializeField] private int _moneyEarendThroughHoney = 1;
    [SerializeField] private int _earnMoneyTimerMAX = 600;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _timer = _timer + 1;
        if (_timer >= _earnMoneyTimerMAX)
        {
           // _amountOfMoneyInsect = _amountOfMoneyInsect + (_moneyEarendThroughHoney * _bee);
            _timer = 0;
        }

        _amountOfInsectDisplay.text = $"{_amountOfMoneyInsect}";
        _amountOfSpiderDisplay.text = $"{_amountOfMoneySpider}";
    }
}
