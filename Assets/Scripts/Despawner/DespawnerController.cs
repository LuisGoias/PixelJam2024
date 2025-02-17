using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnerController : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("decoration"))
        {
            Destroy(collision.gameObject);
        }
    }
}