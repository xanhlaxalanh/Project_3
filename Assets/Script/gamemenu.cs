using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class gamemenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Play(){
        LevelHandler.level = 1;
        LevelHandler.money = 0;
        LevelHandler.avg_rate = 0;
        LevelHandler.guest_num = 0;
        SceneManager.LoadScene("Assets/Scenes/SampleScene.unity",LoadSceneMode.Single);
    }
    public void exit(){
        Application.Quit();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
