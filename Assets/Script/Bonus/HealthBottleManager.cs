using TMPro;
using UnityEngine;

public class HealtBottleManeger : MonoBehaviour, IBonus
{
    [SerializeField] private TMP_Text _HealBottle;

    private int _healBottle;
    public int TotalHealBottle { get;set; }
    
    public void AddBonus(int bonus)
    {
        TotalHealBottle += bonus;
        UpdateUI();
    }

    public void Remove(int bonus)
    {
        TotalHealBottle -= bonus;
        UpdateUI();
    }

    public void UpdateUI()
    {
        _HealBottle.text = TotalHealBottle.ToString();
    }
}
