using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgScroll : MonoBehaviour
{
    GameManager _gm;

    public float _leftPos = -25.0f;
    public float _rightPos = 26.0f;

    void Start()
    {
        _gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }


    void Update()
    {
        if (!_gm._isGameover)
        {
            float speed = _gm._speed;

            if (gameObject.name == "BackGround_1")
                speed += 0.05f;

            transform.Translate(speed, 0, 0);

            // 현재 x좌표가 왼쪽 기준점을 벗어나면
            if (transform.position.x < _leftPos)
            {
                //transform.position.x = _rightPos;

                //오른쪽 기준점으로 강제 이동시킨다.
                Vector3 pos = transform.position;
                pos.x = _rightPos;
                transform.position = pos;
            }
        }
    }
}
