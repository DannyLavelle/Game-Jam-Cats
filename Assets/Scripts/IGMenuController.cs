using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IGMenuController : MonoBehaviour
{
    public GameObject otherPanel;
   public void Exit()
    {
        Application.Quit();
    }
    public void switchPanel()
    {
        otherPanel.SetActive(true);
        gameObject.SetActive(false);
    }
}
