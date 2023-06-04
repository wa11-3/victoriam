using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private float force;

    private Rigidbody2D _rigidBody;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        Invoke("KillBullet", 2);
    }

    private void Update()
    {
        _rigidBody.AddRelativeForce(Vector2.right * force * Time.deltaTime);
    }

    public void KillBullet()
    {
        Destroy(gameObject);
    }
}
