using TMPro;
using UnityEngine;

public class KillEnemy : MonoBehaviour, IBonus
{
   [SerializeField] private TMP_Text _Frag;

    public int _frag { get; set; }
    
    public void AddBonus(int bonus)
    {
        _frag += bonus;
        UpdateUI();
    }

    public void Remove(int bonus)
    {
    }

    public void UpdateUI()
    {
        _Frag.text = _frag.ToString();
    }
}
