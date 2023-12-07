using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    public GameManager _gm;

    Rigidbody2D _rigid;
    Animator _animator;

    public AudioSource _playerSound;

    public AudioClip _jumpClip;
    public AudioClip _fiyClip;

    [SerializeField] bool _ground;
    [SerializeField] bool _flyReady;
    [SerializeField] float animTime;
    [SerializeField] float _jumpForce = 500.0f;

    //[SerializeField] float _jumpTime = 0f;

    float _normalGravity = 1.0f;
    float _flyGravity = 0.3f;

    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _flyReady = false;
    }

    void Update()
    {
        if (!_gm._isGameover)
        {
            if (Input.GetMouseButtonDown(0) && _ground)
            {
                Jump();
            }

            if (Input.GetMouseButton(0) && _flyReady)
            {
                Fly();
            }

            if (Input.GetMouseButtonUp(0) && !_ground)
            {
                _animator.SetBool("Fly", false);
                _animator.SetBool("Jump", true);
                _rigid.gravityScale = _normalGravity;
            }

            if (gameObject.transform.position.y >= 2.4f)
            {
                _flyReady = true;
            }
        }
    }

    void PlaySound(string action)
    {
        switch (action)
        {
            case "JUMP":
                _playerSound.clip = _jumpClip;
                break;
            case "FLY":
                _playerSound.clip = _fiyClip;
                break;
        }
        _playerSound.Play();
    }
    void Ground(bool check)
    {
        _animator.SetBool("Ground", check);
        _animator.SetBool("Jump", !check);
    }

    void Jump()
    {
        _rigid.AddForce(new Vector3(0, _jumpForce, 0));
        PlaySound("JUMP");
        _animator.SetBool("Jump", true);
    }

    void Fly()
    {
        _rigid.gravityScale = _flyGravity;
        _animator.SetBool("Fly", true);
        _animator.SetBool("Jump", false);
    }

    public void FlySound()
    {
        PlaySound("FLY");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Log")
        {
            _gm.GameOver();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        CheckGround(collision, true);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        CheckGround(collision, false);
    }

    void CheckGround(Collision2D collision, bool Check)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _ground = Check;
            Ground(Check);

            if(Check)
            {
                _flyReady = false;
                _rigid.gravityScale = _normalGravity;
            }
        }
    }
}
