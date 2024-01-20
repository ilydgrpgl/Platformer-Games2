using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeControl : MonoBehaviour
{
    Transform parent;

    private void Awake()
    {
        parent = GameObject.FindWithTag("Player").transform;
    }
    private void Update()
    {
        if(Vector3.Distance(transform.position, parent.position)>5f)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
      if(other.CompareTag("EnemyDog"))
        {
            EnemyDog.enemydog.CoroutineBaslat(5f);
            gameObject.SetActive(false);
        }
    }
}
