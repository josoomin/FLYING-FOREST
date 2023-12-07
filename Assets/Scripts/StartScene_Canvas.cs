using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene_Canvas : MonoBehaviour
{
    GameObject _HowChan;

    private void Start()
    {
        _HowChan = GameObject.Find("1_UI_HowToPlay");
        _HowChan.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("FlyForest");
    }

    public void ShowHowToPlay()
    {
        _HowChan.SetActive(true);
    }

    public void CloseHowToPlay()
    {
        _HowChan.SetActive(false);
    }

}
