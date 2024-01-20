using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Mathematics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class CamereController : MonoBehaviour
{
    Camera camera;
    Transform player;
    public BoxCollider2D cameraCollider;
    float cameraYukseklik;
    float cameraUzunluk;

    private void Awake()
    {
        camera =GetComponent<Camera>();
        player = GameObject.FindWithTag("Player").transform;
        cameraYukseklik = Camera.main.orthographicSize;
        cameraUzunluk = cameraYukseklik * Camera.main.aspect;

    }


    private void Update()
    {
        // Kamera ile box collider hareket sýnýrlandýrmasý 
        if (player != null)
        {
            transform.position = new Vector3(Mathf.Clamp(player.position.x,cameraCollider.bounds.min.x+cameraUzunluk,cameraCollider.bounds.max.x-cameraUzunluk),
                Mathf.Clamp(player.position.y,cameraCollider.bounds.min.y+cameraYukseklik,cameraCollider.bounds.max.y-cameraYukseklik),
                transform.position.z);
        }
    }
}
