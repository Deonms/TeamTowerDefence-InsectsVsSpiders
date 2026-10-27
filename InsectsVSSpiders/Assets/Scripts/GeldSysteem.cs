using TMPro;
using UnityEngine;

public class GeldSyteem : MonoBehaviour
{
    [Header("Money of each Team")]
    [SerializeField] private double _amountOfMoneySpider = 0;
    [SerializeField] private TMP_Text _amountOfSpiderDisplay;
    [SerializeField] private TMP_Text _amountOfSpiderDisplayshop;
    [SerializeField] public double _amountOfMoneyInsect = 0;
    [SerializeField] private TMP_Text _amountOfInsectDisplay;
    [SerializeField] private TMP_Text _amountOfInsectDisplayshop;
    [SerializeField] private int _timer = 0;
    [SerializeField] private int _moneyEarendThroughHoney = 1;
    [SerializeField] private int _earnMoneyTimerMAX = 600;
    [SerializeField] private double _amountOfBees;
    [SerializeField] private BuyingAInsectorSpider _beesBought;
    


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
        _beesBought._insectAmount = _amountOfBees;

        _timer = _timer ++;
        if (_timer >= _earnMoneyTimerMAX)
        {
            _beesBought._insectAmount = _amountOfBees;
            _amountOfMoneyInsect = _amountOfMoneyInsect + (_moneyEarendThroughHoney * _amountOfBees);
            _timer = 0;
        };


    }
    
}
