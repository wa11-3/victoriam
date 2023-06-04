using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] private GameObject lifeBar;
    [SerializeField] GameObject _bulletGameobject;
    [SerializeField] Transform _bulletPos1;
    [SerializeField] Transform _bulletPos2;
    [SerializeField] private SpriteRenderer _sprite;
    private GameObject _player;
    private NavMeshAgent _agent;

    private bool _detectedTarget;
    private float _health;

    public Transform[] wayPoint;
    public int wayPointIndex;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        wayPointIndex = 0;
        _detectedTarget = false;
        _health = 100f;
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        var distance = Vector3.Distance(_player.transform.position, transform.position);

        if (Vector3.Distance(transform.position, wayPoint[wayPointIndex].position) < 1)
        {
            if (wayPointIndex == (wayPoint.Length - 1))
            {
                wayPointIndex = 0;
            }
            else
            {
                wayPointIndex++;
            }
        }

        if (distance < 5 && _detectedTarget == false)
        {
            _detectedTarget = true;
            StartCoroutine(Shoot());
        }

        if (_detectedTarget)
        {
            _agent.SetDestination(_player.transform.position);
            LookTarget(transform.position - _player.transform.position);
        }
        else
        {
            _agent.SetDestination(wayPoint[wayPointIndex].position);
            LookTarget(transform.position - wayPoint[wayPointIndex].position);
        }
    }

    private void LookTarget(Vector3 distance)
    {
        if (distance.x > 0)
        {
            _sprite.flipX = true;
            _sprite.transform.right = distance;

        }
        else
        {
            _sprite.flipX = false;
            _sprite.transform.right = distance * -1;
        }
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Debug.Log(collision.transform.name);
    //    Destroy(collision.gameObject);
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
        if (collision.CompareTag("Bullet"))
        {
            _health -= 11;
            StartCoroutine(Damage());
            if (_health <= 0)
            {
                GameManager.Instance.DestroyedEnemies(transform.position);
                Destroy(gameObject);
            }
            lifeBar.transform.localScale -= new Vector3(0.11f, 0, 0);
        }
    }

    private IEnumerator Damage()
    {
        _sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        _sprite.color = Color.white;
    }

    private IEnumerator Shoot()
    {
        yield return new WaitForSeconds(3f);
        while (true)
        {
            if (_sprite.flipX)
            {
                Instantiate(_bulletGameobject, _bulletPos2.position, _sprite.transform.rotation * Quaternion.Euler(0, 0, 190f));
            }
            else
            {
                Instantiate(_bulletGameobject, _bulletPos1.position, _sprite.transform.rotation * Quaternion.Euler(0, 0, -10f));
            }
            yield return new WaitForSeconds(1f);
            //Debug.Log(_sprite.transform.rotation);
        }
    }
}
