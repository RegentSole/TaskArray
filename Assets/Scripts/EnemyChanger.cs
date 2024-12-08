// EnemyChanger.cs
using UnityEngine;
using TMPro;

public class EnemyChanger : MonoBehaviour
{
    public TMP_InputField inputField;
    public GameObject[] units;

    void Start()
    {
        units = GameObject.FindGameObjectsWithTag("Unit");
    }

    public void ShowUnitsWithHealthGreaterThan()
    {
        int threshold;
        if (int.TryParse(inputField.text, out threshold))
        {
            foreach (var unit in units)
            {
                Unit unitScript = unit.GetComponent<Unit>();
                unit.SetActive(unitScript.health > threshold);
            }
        }
    }

    public void ShowUnitsWithSpecificLevel()
    {
        int level;
        if (int.TryParse(inputField.text, out level))
        {
            foreach (var unit in units)
            {
                Unit unitScript = unit.GetComponent<Unit>();
                unit.SetActive(unitScript.level == level);
            }
        }
    }

    public void ResetVisibility()
    {
        foreach (var unit in units)
        {
            unit.SetActive(true);
        }
    }

    public void ChangeNamesAndStats()
    {
        string targetName = inputField.text;
        foreach (var unit in units)
        {
            Unit unitScript = unit.GetComponent<Unit>();
            if (unitScript.unitName == targetName)
            {
                unitScript.Initialize("Boss", unitScript.health * 3, unitScript.level + 1);
            }
        }
    }

    public void OnButtonClick(int buttonIndex)
    {
        switch (buttonIndex)
        {
            case 1:
                ShowUnitsWithHealthGreaterThan();
                break;
            case 2:
                ShowUnitsWithSpecificLevel();
                break;
            case 3:
                ResetVisibility();
                break;
            case 4:
                ChangeNamesAndStats();
                break;
        }
    }
}