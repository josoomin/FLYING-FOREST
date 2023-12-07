using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class GameManager : MonoBehaviour
{
    LogMaker _lG;

    public float _speed = -0.1f;

    public GameObject _playUI; // 게임 플레이 화면(현재 스코어)
    public GameObject _gameoverUI; // 게임 오버 화면(게임 오버, 현재 스코어)
    public Rigidbody2D _playerRigid; // 플레이어 리지드바디
    public Animator _playerAni; // 플레이어 애니메이션

    public bool _isGameover; // 게임오버 유무 확인
    bool _speedUP;

    public Text _scoreNumberText; // 점수 표시 텍스트
    public Text _bestScoreNumberText; // 최고 점수 표시 텍스트
    public float _score; // 현재 점수

    float _bestScore;

    void Start()
    {
        Application.targetFrameRate = 60;
        LoadData();
        _bestScoreNumberText.text = Mathf.FloorToInt(_bestScore).ToString();
        _lG = GameObject.Find("LogMaker").GetComponent<LogMaker>();
        _score = 0;
        _playUI.SetActive(true);
        _gameoverUI.SetActive(false);

        _isGameover = false;
        _speedUP = false;
        _playerRigid.simulated = true;
        _playerAni.StopPlayback();
    }

    void Update()
    {
        if (!_isGameover)
        {
            _score += Time.deltaTime;
            _scoreNumberText.text = Mathf.FloorToInt(_score).ToString();
        }

        if (_score != 0 && Mathf.FloorToInt(_score) % 10 == 0 && _speedUP)
        {
            _speedUP = false;
            _speed -= 0.02f;
            _lG._makeTime += 0.001f;

        }
        
        if(_score != 0 && Mathf.FloorToInt(_score) % 10 != 0)
        {
            _speedUP = true;
        }
    }

    void SetBest()
    {
        _bestScore = _score;
        SaveData();
        _bestScoreNumberText.text = Mathf.FloorToInt(_bestScore).ToString();
    }

    public void GameOver()
    {
        _playUI.SetActive(false);
        _gameoverUI.SetActive(true);

        _playerRigid.simulated = false;
        _playerAni.StartPlayback();
        _isGameover = true;

        if (_bestScore <= _score)
        {
            SetBest();
        }
    }

    public void OnClick_Retry()
    {
        SceneManager.LoadScene("FlyForest");
    }

    void LoadData()
    {
        _bestScore = ES3.Load<float>("_bestScore", 0);
    }

    void SaveData()
    {
        ES3.Save<float>("_bestScore", _bestScore);
    }
}