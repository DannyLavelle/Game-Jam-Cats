using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuContoller : MonoBehaviour
{
    public void playButton()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void exitButton()
    {
        Application.Quit();
    }

}
