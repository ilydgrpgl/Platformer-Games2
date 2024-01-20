using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ItemsControl : MonoBehaviour
{
  
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            GameManager.gameManager.ItemGet(gameObject.name);
            
        }
        

    }
}
