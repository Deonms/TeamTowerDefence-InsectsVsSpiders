using TMPro;
using UnityEngine;

public class BuyingAInsectorSpider : MonoBehaviour
{
    [Header("Insect price")]
    [SerializeField] private double _insectCost = 10;
    [SerializeField] private double _insectCostIncrease = 0;
    [SerializeField] public double _insectAmount;

    [Header("UI")]
    [SerializeField] private TMP_Text _costAntDisplay;

    [Header("Money")]
    [SerializeField] private GeldSyteem _moneygetting;

    [Header("Insect")]
    [SerializeField] private GameObject _TypeOfInsect;

    [Header("Placement")]
    [SerializeField] private InsectPlacementSystem _placementSystem;

    private bool _purchasePending = false;

    private void Start()
    {
        UpdateCostDisplay();
    }

    public void BuyInsect()
    {
        if (_purchasePending)
            return;

        if (_moneygetting == null ||
            _TypeOfInsect == null ||
            _placementSystem == null)
        {
            Debug.LogError("Buying system is not fully configured!");
            return;
        }

        double currentMoney =
            _moneygetting._amountOfMoneyInsect;

        if (currentMoney < _insectCost)
        {
            Debug.Log("Not enough money!");
            return;
        }

        // Start placement first.
        bool started = _placementSystem.StartPlacement(
            _TypeOfInsect,
            OnPlacementFinished
        );

        if (!started)
            return;

        // Reserve the money.
        _moneygetting._amountOfMoneyInsect =
            currentMoney - _insectCost;

        _purchasePending = true;

        Debug.Log("Choose a location for your insect!");
    }

    private void OnPlacementFinished(bool placed)
    {
        _purchasePending = false;

        if (placed)
        {
            // Purchase completed.
            _insectAmount++;

            // Increase price for next purchase.
            _insectCost += _insectCostIncrease;

            Debug.Log("Insect successfully placed");
        }
        else
        {
            // Refund money if placement was cancelled.
            if (_moneygetting != null)
            {
                _moneygetting._amountOfMoneyInsect += _insectCost;
            }

            Debug.Log("plaatsen geanuleerd. geld terug gegeven");
        }

        UpdateCostDisplay();
    }

    private void UpdateCostDisplay()
    {
        if (_costAntDisplay != null)
        {
            _costAntDisplay.text =
                _insectCost.ToString("0.##");
        }
    }
}