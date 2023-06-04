using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] GameObject _gunGameobject;
    [SerializeField] GameObject _bulletGameobject;
    [SerializeField] Transform _bulletPos;
    [SerializeField] private SpriteRenderer _spritePlayer;
    [SerializeField] private float _moveForce;

    private float _health;

    private Rigidbody2D _rigidBody;
    private Camera _cameraMain;


    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _cameraMain = Camera.main;
        _health = 100;
    }

    private void Update()
    {
        Vector3 positionMouse = _cameraMain.ScreenToWorldPoint(Input.mousePosition);
        positionMouse.z = 0;
        Vector3 lookAtPosition = positionMouse - transform.position;
        _gunGameobject.transform.right = lookAtPosition;

        var moveHorizontal = Input.GetAxis("Horizontal");
        var moveVertical = Input.GetAxis("Vertical");
        _rigidBody.AddForce(new Vector2(moveHorizontal, moveVertical) * _moveForce * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Instantiate(_bulletGameobject, _bulletPos.position, _gunGameobject.transform.rotation);
        }

        if (positionMouse.x - transform.position.x < 0)
        {
            _spritePlayer.sortingOrder = 15;
        }
        else
        {
            _spritePlayer.sortingOrder = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BulletEnemy"))
        {
            _health -= 11;
            StartCoroutine(Damage());
            GameManager.Instance.Damage();
            if (_health <= 0)
            {
                SceneManager.LoadScene("GameOver");
                Destroy(gameObject);
            }
        }
    }

    private IEnumerator Damage()
    {
        _spritePlayer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        _spritePlayer.color = Color.white;
    }
}
