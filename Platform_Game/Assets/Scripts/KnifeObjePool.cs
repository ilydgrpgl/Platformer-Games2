using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class KnifeObjePool : MonoBehaviour
{
   public  GameObject[] knifes;
   public static KnifeObjePool knifeObjePool;

    private void Start()
    {
            knifeObjePool= this;
    }

    
    // Knifelarýmýzýn sayýsýný sýnýrlandýrarak performans iyileþtirlmesi yapýldý.(Object Pool)
    public  void KnifeBul(Transform olusmaNoktasi,Transform parent)
    {
        for (int i = 0; i < knifes.Length; i++)
        {
            if (!knifes[i].activeInHierarchy)
            {
                knifes[i].SetActive(true);
                knifes[i].transform.position = olusmaNoktasi.transform.position;
                if (parent.localScale.x > 0)
                {
                    knifes[i].transform.localScale = new Vector2(0.1f, knifes[i].transform.localScale.y);
                    knifes[i].transform.rotation = Quaternion.Euler(knifes[i].transform.rotation.x, knifes[i].transform.rotation.y, -65f);
                    knifes[i].GetComponent<Rigidbody2D>().AddForce(Vector2.right * 500f);
                        
                }
                else
                {
                    knifes[i].transform.localScale= new Vector2(-0.1f, knifes[i].transform.localScale.y);
                    knifes[i].transform.rotation = Quaternion.Euler(knifes[i].transform.rotation.x, knifes[i].transform.rotation.y, 65f);
                    knifes[i].GetComponent<Rigidbody2D>().AddForce(Vector2.left * 500f);
                        
                }
                return;
            }
            
        }
    }
}
