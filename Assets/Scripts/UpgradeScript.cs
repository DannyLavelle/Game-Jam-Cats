using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeScript : MonoBehaviour
{
    public Upgrades upgrade;
    public CatData MiceStash;
    public TMP_Text buttonText;
    public TMP_Text bonusText;
    private void Start()
    {
        updateTEXT();
    }
    public void upgradeClicked()
    {
        Debug.Log("Button clicked");
        Debug.Log("Mice stash " +MiceStash.amount);
        Debug.Log("Upgrade cost "+upgrade.cost);

        if (MiceStash.amount >= upgrade.cost)
        {
            MiceStash.amount -= upgrade.cost;
            upgrade.increaseLevel();
            updateTEXT();
        }

    }
    void updateTEXT()
    {
        updateBonusText();
        updateButtonText();
    }
    void updateBonusText() 
    {
        if(upgrade.Type == Upgrades.upgradeType.multiplicative)
        {
            bonusText.text = ("Current Bonus : *" + upgrade.GetMultiplier());
        }
        else
        {
            bonusText.text = ("Current Bonus : +" + upgrade.GetMultiplier());
        }
    }
    void updateButtonText()
    {
        buttonText.text = (upgrade.nameofupgrade + " (" + upgrade.cost + ")");
    }
}
