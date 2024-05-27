using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolaFuego : MonoBehaviour {
    Rigidbody2D rb;
    [SerializeField] private float impulsoDisparo;
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        /*
         *  transform.right     Eje X (Rojo)
         *  transform.forward   Eje Z (Azul)
         *  transform.up        Eje Y (Verde)
         */
        rb.AddForce(transform.right * impulsoDisparo, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update() {
        
    }
}
