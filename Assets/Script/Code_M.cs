using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Code_M : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("GAME");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
