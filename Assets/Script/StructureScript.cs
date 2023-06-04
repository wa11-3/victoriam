using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureScript : MonoBehaviour
{
    [SerializeField] private GameObject lifeBar;
    [SerializeField] private SpriteRenderer _sprite;
    private float _health;

    private void Start()
    {
        _health = 200;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
        if (collision.CompareTag("Bullet"))
        {
            _health -= 11;
            StartCoroutine(Damage());
            if (_health <= 0)
            {
                GameManager.Instance.DestroyedStructure(transform.position);
                Destroy(gameObject);
            }
            lifeBar.transform.localScale -= new Vector3(0.11f/2, 0, 0);
        }
    }

    private IEnumerator Damage()
    {
        _sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        _sprite.color = Color.white;
    }
}
