using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerKontrol : MonoBehaviour
{
    private Rigidbody2D rg;
    public float hareketHizi;
    public float ziplamaGucu;
    public bool zemindeMi;
    public Transform zeminKontrolNoktasi;
    private bool ciftZiplama;
    private const int ZeminTabaka = 3;
    Animator animator;
    public float knifebeklemeSuresi;
    float icerdenKnifebeklemeSuresi;
    public Transform knifePosition;
    float geritepkiSuresi;
    float geritepkiGucu;
    public static PlayerKontrol playerKontrol;
    
    
    
    
    SpriteRenderer sr;


    private void Start()
    {
        rg = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerKontrol = this;
        sr= GetComponent<SpriteRenderer>();
    }

    public void GeriTepki()
    {
        if (!zemindeMi)
        {
            return;
        }
        geritepkiSuresi = .5f;
        geritepkiGucu=1.7f;
    }

    private void Update()
    {
        
        knife();
        PlayerHasar();
        
        
    }
    private void PlayerHasar()
    {
        if (geritepkiSuresi <= 0)
        {
            HareketEt();
            Zipla();
            sr.color = new Color(255f, 255f, 255f, 255f);
        }
        else
        {
            sr.color = new Color(255f, 0f, 0f, 125f);
            
            if (EnemyDog.enemydog.yonSagMi)
            {
                rg.velocity = new Vector2(geritepkiGucu, rg.velocity.y);
            }
            else if (!EnemyDog.enemydog.yonSagMi)
            {
                rg.velocity = new Vector2(-geritepkiGucu, rg.velocity.y);
            }

            geritepkiSuresi -= Time.deltaTime;
        }
    }
    void knife()
    {
        if (Input.GetKeyDown(KeyCode.F) && Time.time > icerdenKnifebeklemeSuresi)
        {
            KnifeObjePool.knifeObjePool.KnifeBul(knifePosition, gameObject.transform);
            icerdenKnifebeklemeSuresi = knifebeklemeSuresi + Time.time;

        }
    }

    private void HareketEt()
    {
        float h = Input.GetAxis("Horizontal");
        rg.velocity = new Vector2(h * hareketHizi, rg.velocity.y);


        animator.SetFloat("hareketHizi",Mathf.Abs(rg.velocity.x));
        if(rg.velocity.x > 0 )
        {
            transform.localScale = new Vector2(5f, 5f);
           

        }
        else if (rg.velocity.x < 0)
        {
            transform.localScale = new Vector2(-5f, 5f);
            
        }
    }

    private void Zipla()
    {
        zemindeMi = ZeminiKontrolEt();
        animator.SetBool("zemindeMi", zemindeMi);


        if (Input.GetButtonDown("Jump") && (zemindeMi || ciftZiplama))
        {
            if (zemindeMi)
            {
                ciftZiplama = true;
            }
            else if (ciftZiplama)
            {
                ciftZiplama = false;
            }
            rg.velocity = new Vector2(rg.velocity.x, ziplamaGucu);
        }
    }

    private bool ZeminiKontrolEt()
    {
        int tabakaMaskesi = 1 << ZeminTabaka;
        return Physics2D.OverlapCircle(zeminKontrolNoktasi.position, .2f, tabakaMaskesi);
    }
    
}

