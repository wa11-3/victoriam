using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScript : MonoBehaviour
{
    private void Start()
    {
        Invoke("DeleteParticle", 4);
    }

    private void DeleteParticle()
    {
        Destroy(gameObject);
    }
}
