using TMPro;
using UnityEngine;

public class GeldSyteem : MonoBehaviour
{
    [Header("Money of each Team")]
    [SerializeField] private double _amountOfMoneySpider = 0;
    [SerializeField] private TMP_Text _amountOfSpiderDisplay;
    [SerializeField] private TMP_Text _amountOfSpiderDisplayshop;
    [SerializeField] private double _amountOfMoneyInsect = 0;
    [SerializeField] private TMP_Text _amountOfInsectDisplay;
    [SerializeField] private TMP_Text _amountOfInsectDisplayshop;
    [SerializeField] private int _timer = 0;
    [SerializeField] private int _moneyEarendThroughHoney = 1;
    [SerializeField] private int _earnMoneyTimerMAX = 600;
    private int _amountOfBees = 0;
    private AmountOfSpidersAndInsects _insectsAndSpiders;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _amountOfInsectDisplay.text = $"{_amountOfMoneyInsect}";
        _amountOfSpiderDisplay.text = $"{_amountOfMoneySpider}";
        _amountOfSpiderDisplayshop.text = $"{_amountOfMoneySpider}";
        _amountOfInsectDisplayshop.text = $"{_amountOfMoneyInsect}";
        _amountOfBees = _insectsAndSpiders._bee;
        _timer = _timer + 1;
        if (_timer >= _earnMoneyTimerMAX)
        {
           _amountOfMoneyInsect = _amountOfMoneyInsect + (_moneyEarendThroughHoney * _amountOfBees);
            _timer = 0;
        }

       
    }
}
