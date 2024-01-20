using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    float health;
    public Slider healthSlider;
    GameObject player;
    public GameObject[] hearts;
    public TextMeshProUGUI gemText;
    public TextMeshProUGUI cherryText;
    int gemAdet=0;
    int cherryAdet=0;
    public static GameManager gameManager;
    public BoxCollider2D hasarCollider;
    public ParticleSystem canYenileme;
    public ParticleSystem cherrytoplamaEffect;
    public ParticleSystem gemtoplamaEffect;


    private void Start()
    {
        health = 100;
        player = GameObject.FindWithTag("Player");
        gameManager = this;
    }
    public void CanAzalt(float DarbeGucu)
    {
        if(!PlayerKontrol.playerKontrol.zemindeMi)
        {
            return;
        }// zeminde deðilsek canýmýz azalmasýn
      if(health>100)
        {
            health = 100;
        }
        else if(health<0)
        {
            health = 0;
        }
        health -= DarbeGucu;
        healthSlider.value = health;
        if(health<=0) 
        {
            KalpleriKapat();
            hearts[2].SetActive(true);
            StartCoroutine(PlayerYokEt());
        }
        else if(health>50 && health<=100) 
        {
            KalpleriKapat();
            hearts[0].SetActive(true);

        }
        else if (health <= 50 && health > 0)
        {
            KalpleriKapat();
            hearts[1].SetActive(true);

        }

    }
    IEnumerator PlayerYokEt()
    {
        yield return new WaitForSeconds(2f);
        player.SetActive(false);
    }
    void KalpleriKapat()
    {
        foreach(GameObject heart in hearts) 
        {
            heart.SetActive(false);
        }
    }
    public void ItemGet(string name)
    {  
        if(hasarCollider.IsTouchingLayers(LayerMask.GetMask("Enemy")))
        {
            StartCoroutine(EnemyDog.enemydog.CanAzalt(10f));

        }
        else if(name=="gem")
        {
            gemAdet++;
            gemText.text=gemAdet.ToString();
            ParticleSystem effect = Instantiate(gemtoplamaEffect, player.transform.position,   Quaternion.identity);
            effect.Play();
        }
       else if (name == "cherry")
        {
            cherryAdet++;
           cherryText.text = cherryAdet.ToString();
            ParticleSystem effect = Instantiate(cherrytoplamaEffect, player.transform.position, Quaternion.identity);
            effect.Play();
        }


    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.H))
        {
            CherryCanYenileme();
        }
    }
    public void CherryCanYenileme()
    {
        if(cherryAdet>=5)
        {
            health += (health * 10 / 100);
            healthSlider.value = health;
           cherryAdet -= 5;
            cherryText.text=cherryAdet.ToString();
            ParticleSystem effect = Instantiate(canYenileme, player.transform.position, Quaternion.Euler(-90f,0f,0f));
            effect.Play();


        }
    }
}
