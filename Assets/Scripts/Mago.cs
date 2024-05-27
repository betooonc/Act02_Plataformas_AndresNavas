using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mago : MonoBehaviour {
    [SerializeField] private GameObject bolaFuego;
    [SerializeField] private Transform puntoSpawn;
    [SerializeField] private float tiempoAtaques;
    [SerializeField] private float danhoAtaque;
    private Animator anim;
    void Start() {
        anim = GetComponent<Animator>();
        StartCoroutine(RutinaAtaque());
    }

    void Update() {
        
    }

    IEnumerator RutinaAtaque() {
        while (true) {
            anim.SetTrigger("atacar");
            yield return new WaitForSeconds(tiempoAtaques);
        }
    }
    private void LanzarBola() {
        Instantiate(bolaFuego, puntoSpawn.position, transform.rotation);
    }
}
