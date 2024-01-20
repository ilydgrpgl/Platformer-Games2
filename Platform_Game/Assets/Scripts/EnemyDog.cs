using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDog : MonoBehaviour
{
    public Transform[] pozisyonlar;
    int kacinciPoz;
    public float speed;
    public float beklemeSuresi;
    float beklemeSayac;
    Animator animator;
    Transform player;
    float health;
    public static EnemyDog enemydog;
    SpriteRenderer sr;
    public BoxCollider2D attackCollider;
    public bool yonSagMi;
    public Slider healthBar;
    public GameObject enemyCanvas;
    private void Awake()
    {
        foreach (var p in pozisyonlar)
        {
            p.parent = null;
        }
        health = 100f;
        animator = GetComponent<Animator>();
        sr= GetComponent<SpriteRenderer>();
        player = GameObject.FindWithTag("Player").transform;
        enemydog = this;
        
    }

    void HareketEt()
    {
        if (beklemeSayac > 0)
        {
            beklemeSayac -= Time.deltaTime;
            animator.SetBool("hareketEtsinMi", false);
        }
        else
        {
            animator.SetBool("hareketEtsinMi", true);
            if (transform.position.x > pozisyonlar[kacinciPoz].position.x)
            {

                transform.localScale = new Vector3(5f, 5f, 1f);
                yonSagMi = false;
            }
            else
            {
                transform.localScale = new Vector3(-5f, 5f, 1f);
                yonSagMi = true;
            }
            transform.position = Vector3.MoveTowards(transform.position, pozisyonlar[kacinciPoz].position, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, pozisyonlar[kacinciPoz].position) < .1f)
            {
                beklemeSayac = beklemeSuresi;
                kacinciPoz++;
                if (kacinciPoz > pozisyonlar.Length - 1)
                {
                    kacinciPoz = 0;
                }
            }
        }
    }
    private void Update()
    {
        
        HareketEt();
        
    }
public void CoroutineBaslat(float darbe)
    {
        StartCoroutine(CanAzalt(darbe));
    }
  public  IEnumerator CanAzalt(float darbe)
    {
        health -= darbe;
        sr.color = new Color(255f,0f,0f,255f);
        enemyCanvas.SetActive(true);
        healthBar.value = health;
        if (health <= 0)
        {
            enemyCanvas.SetActive(false);
            yield return new  WaitForSeconds(.3f);
            sr.color = new Color(255f, 255f, 255f, 255f);
            animator.SetBool("olduMu", true);
            yield return new WaitForSeconds(.7f);
            gameObject.SetActive(false);
        }
        else if(health > 0)
        {
            yield return new WaitForSeconds(.5f) ;
            sr.color = new Color(255f, 255f, 255f, 255f);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerKontrol.playerKontrol.GeriTepki();
            GameManager.gameManager.CanAzalt(15f);
        }
    
        
    }
}
