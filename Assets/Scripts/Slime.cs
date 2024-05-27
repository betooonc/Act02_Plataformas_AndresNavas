using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour {
    [SerializeField] private Transform[] wayPoints;
    [SerializeField] private float velocidadPatrulla;
    [SerializeField] private float danhoAtaque;
    private Vector3 destinoActual;
    private int indiceActual = 0;
    // Start is called before the first frame update
    void Start() {
        destinoActual = wayPoints[indiceActual].position;
        StartCoroutine(Patrulla());
    }

    // Update is called once per frame
    void Update() {
        
    }
    IEnumerator Patrulla () {
        while (true) {
            while (transform.position != destinoActual) {
                transform.position = Vector3.MoveTowards(transform.position, destinoActual, velocidadPatrulla * Time.deltaTime);
                yield return null;
            }
            DefinirNuevoDestino();
        }
    }
    private void DefinirNuevoDestino() {
        indiceActual++;
        if (indiceActual >= wayPoints.Length) {
            indiceActual = 0;
        }
        destinoActual = wayPoints[indiceActual].position;
        EnfocarDestino();
    }

    private void EnfocarDestino() {
        if (destinoActual.x > transform.position.x) {
            transform.localScale = Vector3.one;
        } else {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    private void OnTriggerEnter2D(Collider2D elOtro) {
        if (elOtro.gameObject.CompareTag("PlayerDeteccion")) {
            Debug.Log("Visto!!");
        } else if (elOtro.gameObject.CompareTag("PlayerHitbox")) {
            SistemaVidas vidasPlayer = elOtro.gameObject.GetComponent<SistemaVidas>();
            vidasPlayer.RecibirDanho(danhoAtaque);
        }
    }
}
