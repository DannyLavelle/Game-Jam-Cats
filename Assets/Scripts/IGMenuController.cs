using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class IGMenuController : MonoBehaviour
{
    public GameObject otherPanel;
    public GameObject cat;
    public CatData mice;
    public CatData catnip;
    public CatData Worker;
    public void Exit()
    {
        Application.Quit();
    }
    public void switchPanel()
    {
        otherPanel.SetActive(true);
        gameObject.SetActive(false);
    }
    public void BuyWorker()
    {
        if(Worker.amount == 0 )
        {
            mice.amount = 0;
            catnip.amount = 0;
            
            Instantiate(cat);
        }
    
    }
}
