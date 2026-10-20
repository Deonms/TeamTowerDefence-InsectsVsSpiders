using TMPro;
using UnityEngine;

public class BuyingAInsectorSpider : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private AmountOfSpidersAndInsects _insectsAndSpiders;
    [SerializeField] double _insectCost = 10;
    [SerializeField] double _insectAmount;
    [SerializeField] TMP_Text _costAntDisplay;
    [SerializeField]private GeldSyteem _moneygetting;
    private double _money = 0;
    [SerializeField] private string _TypeOfInsect;
    private string _intsectType;

    void Start()
    {
       _intsectType = $"_{_TypeOfInsect}amount";
    }

    // Update is called once per frame
    void Update()
    {
        _money = _moneygetting._amountOfMoneyInsect;
    }
    private void Awake()
    {
        //_moneygetting = GetComponent<GeldSyteem>();
    }
    public void BuyInsect()
    {
        if (_money >= _insectCost)
        {
            _money = _money - _insectCost;
            _insectCost = _insectCost + 10;
            _insectAmount = _insectAmount + 1;
            print("can buy");
        }
        else
        {
            print("can not buy");
        }

    }
}
