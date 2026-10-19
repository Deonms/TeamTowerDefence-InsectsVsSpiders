using TMPro;
using UnityEngine;

public class BuyingAInsectorSpider : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private AmountOfSpidersAndInsects _insectsAndSpiders;
    [SerializeField] double _antCost = 10;
    [SerializeField] double _antAmount;
    [SerializeField] TMP_Text _costAntDisplay;
    private GeldSyteem _moneygetting;
    private double _money = 0;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        _money = _moneygetting._amountOfMoneyInsect; //idk why maar het werkt niet
    }
    void boughtpressed()
    {
        if (_money >= _antCost)
        {
            _money = _money - _antCost;
            _antCost = _antCost + 10;
            _antAmount = _antAmount + 1;
            print("can buy");
        }
        else
        {
            print("can not buy");
        }

    }
}
