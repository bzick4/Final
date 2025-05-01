using TMPro;
using UnityEngine;

public class Points : MonoBehaviour,IBonus
{
   [SerializeField] private TMP_Text _Point;

    public int _point { get; set; }

    public void AddBonus(int bonus)
    {
        _point += bonus;
        UpdateUI();
    }

    public void Remove(int bonus)
    {
    }

    public void UpdateUI()
    {
        _Point.text = _point.ToString();
    }
}
