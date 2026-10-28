using TMPro;
using UnityEngine;

public class BuyingAInsectorSpider : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    [SerializeField] double _insectCost = 10;
    [SerializeField] public double _insectAmount;
    [SerializeField] TMP_Text _costAntDisplay;
    [SerializeField] private GeldSyteem _moneygetting;
    private double _money;
    [SerializeField] private double _insectCostIncrease;
    [SerializeField] private GameObject _TypeOfInsect;
    private string _intsectType;
    [SerializeField] private Transform _Unitcashe;
    

    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        _costAntDisplay.text = $"{_insectCost}";
        _money = _moneygetting._amountOfMoneyInsect;
    }
    
    public void BuyInsect()
    {
        if (_money >= _insectCost)
        {
            _moneygetting._amountOfMoneyInsect = _money - _insectCost;
            _insectCost = _insectCost + _insectCostIncrease;
            _insectAmount = _insectAmount++;
            Instantiate(_TypeOfInsect, _Unitcashe.position, Quaternion.identity);

            print("can buy");
        }
        else
        {
            print("can not buy");
        }

    }
}
