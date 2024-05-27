using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    private Rigidbody2D rb;
    private float inputH;
    [Header("Sistema de Movimiento")]
    [SerializeField] private Transform pies;
    [SerializeField] private float velocidadMovimiento;
    [SerializeField] private float fuerzaSalto;
    [SerializeField] private float distanciaDeteccionSuelo;
    [SerializeField] private LayerMask queEsSaltable;
    
    [Header("Sistema de combate")]
    [SerializeField] private Transform puntoAtaque;
    [SerializeField] private float radioAtaque;
    [SerializeField] private float danhoAtaque;
    [SerializeField] private LayerMask queEsDanhable;

    private Animator anim;
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        Movimiento();

        Saltar();

        LanzarAtaque();
    }

    private void LanzarAtaque() {
        if (Input.GetMouseButtonDown(0)) {
            anim.SetTrigger("attack");
        }
    }
    // Se ejecuta desde la animación.
    private void Ataque() {
        Collider2D[] collidersTocados = Physics2D.OverlapCircleAll(puntoAtaque.position, radioAtaque, queEsDanhable);
        foreach (Collider2D item in collidersTocados) {
            SistemaVidas sistemaVidas = item.gameObject.GetComponent<SistemaVidas>();
            sistemaVidas.RecibirDanho(danhoAtaque);
        }
    }

    private void Saltar() {
        if (Input.GetKeyDown(KeyCode.Space) && estoyEnSuelo()) {
            rb.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
            anim.SetTrigger("jump");
        }
    }

    private bool estoyEnSuelo() {
        return Physics2D.Raycast(pies.position, Vector3.down, distanciaDeteccionSuelo, queEsSaltable);
    }

    private void Movimiento() {
        inputH = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(inputH * velocidadMovimiento, rb.velocity.y);
        if (inputH != 0) {
            anim.SetBool("running", true);
            if (inputH > 0) {
                transform.eulerAngles = Vector3.zero;
            } else {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
        } else {
            anim.SetBool("running", false);
        }
    }

    /*
     * Esto sirve para dibujar algo que no se va a ver en el juego pero te ayuda a ubicarte
     */
    private void OnDrawGizmos() {
        Gizmos.DrawSphere(puntoAtaque.position, radioAtaque);
    }
}
