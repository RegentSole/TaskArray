// Unit.cs
using UnityEngine;
using TMPro;

public class Unit : MonoBehaviour
{
    public string unitName;
    public int health;
    public int level;

    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI levelText;

    public void Initialize(string name, int hp, int lvl)
    {
        unitName = name;
        health = hp;
        level = lvl;

        nameText.text = unitName;
        healthText.text = "Health: " + health.ToString();
        levelText.text = "Level: " + level.ToString();
    }
}