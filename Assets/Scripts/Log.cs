using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : MonoBehaviour
{
    LogMaker _lm;
    GameManager _gm;

    // Start is called before the first frame update
    void Start()
    {
        _lm = GameObject.Find("LogMaker").GetComponent<LogMaker>();
        _gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_gm._isGameover)
        {
            transform.Translate(_gm._speed, 0, 0);


            if (transform.position.x < -15.0f)
            {
                gameObject.SetActive(false);
                gameObject.transform.parent = _lm._logParent.transform;
                gameObject.transform.position = _lm._logParent.transform.position;
                _lm._logPrefaps.Add(gameObject);
            }
        }
    }
}
