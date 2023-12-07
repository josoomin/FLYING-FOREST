using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogMaker : MonoBehaviour
{
    GameManager _gm;

    public GameObject _log;
    public GameObject _logParent;
    public GameObject _logSpawnPoint;
    public List<GameObject> _logPrefaps;

    bool _onTime;
    bool _onRandom;

    [SerializeField] float _nowTime;
    [SerializeField] int _randomTime;

    float _logTime;

    [SerializeField] int _activeLog;
    [SerializeField] float _randomLog;
    public float _makeTime;

    void Start()
    {
        _gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        for (int i = 0; i < 1000; i++)
        {
            GameObject _logins = Instantiate(_log);
            _logPrefaps.Add(_logins);
            _logins.transform.parent = _logParent.transform;
            _logins.transform.position = _logParent.transform.position;
            _logins.SetActive(false);
        }

        SetRandom();
        _onTime = true;
        _onRandom = false;
        _makeTime = 0.18f;
    }

    void Update()
    {
        if (!_gm._isGameover)
        {
            if (_onTime)
            {
                _nowTime += Time.deltaTime;
            }

            if (_randomTime == Mathf.FloorToInt(_nowTime))
            {
                _onTime = false;
                if (_activeLog < _randomLog)
                {
                    _logTime += Time.deltaTime;
                    if (_logTime >= _makeTime)
                    {
                        _logTime = 0;
                        ThrowLog();
                    }
                }
            }

            if (_activeLog >= _randomLog)
            {
                _nowTime = 0;
                _activeLog = 0;
                _logTime = 0;
                _onTime = true;
                SetRandom();
            }
        }
    }

    void ThrowLog()
    {
        _logPrefaps[0].SetActive(true);
        _logPrefaps[0].transform.position = _logSpawnPoint.transform.position;
        _logPrefaps.RemoveAt(0);
        _activeLog++;
    }

    void SetRandom()
    {
        _randomTime = Random.Range(1, 4);
        _randomLog = Random.Range(1, 10);
    }
}
