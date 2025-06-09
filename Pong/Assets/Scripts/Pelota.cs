using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pelota : MonoBehaviour
{
    JUego miJuego;
    private AudioSource audio;
    public AudioClip snd1, snd2, sndGol, sndPared;
    public static int numToques = 0, golesJugadorIzq = 0, golesJugadoreDrh = 0;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        miJuego = GameObject.Find("Juego").gameObject.GetComponent<JUego>();
    }


    private void OnTriggerEnter2D(Collider2D colision)
    {
        float compX = 0, compY = 0;

        if (colision.CompareTag("Gol")){
            audio.clip = sndGol;
            audio.Play();
            numToques = 0;

            GameObject nombrePorteria = colision.gameObject;
            if (nombrePorteria.name == "PorteriaIzq")
            {
                golesJugadoreDrh++;
            }else if (nombrePorteria.name == "PorteriaDrh"){
                golesJugadorIzq++;
            }

            miJuego.EscribeMarcador();
        }

        if (colision.CompareTag("JugadorIzq")){
            audio.clip = snd1;
            audio.Play();
            numToques++;

            float alturaColisionIzq = GameObject.Find("JugadorIzq").gameObject.transform.position.y - transform.position.y;
            compX = Mathf.Cos(alturaColisionIzq);
            compY = Mathf.Sin(alturaColisionIzq);

            if (alturaColisionIzq >= 0)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(compX * JUego.velBola + numToques, compY * (JUego.velBola * -1) - (float) numToques/2);
            }else{
                GetComponent<Rigidbody2D>().velocity = new Vector2(compX * JUego.velBola + numToques, compY * (JUego.velBola * -1) + (float)numToques / 2);
            }

        }

        if (colision.CompareTag("JugadorDrh")){
            audio.clip = snd1;
            audio.Play();
            numToques++;

            float alturaColisionDrh = GameObject.Find("JugadorDrh").gameObject.transform.position.y - transform.position.y;
            compX = Mathf.Cos(alturaColisionDrh);
            compY = Mathf.Sin(alturaColisionDrh);

            if (alturaColisionDrh >= 0){
                GetComponent<Rigidbody2D>().velocity = new Vector2(compX * (JUego.velBola * -1)  - numToques, compY * (JUego.velBola * -1) - (float)numToques / 2);
            }else {
                GetComponent<Rigidbody2D>().velocity = new Vector2(compX * (JUego.velBola * -1) - numToques, compY * (JUego.velBola * -1) + (float)numToques / 2);
            }

        }



        if (colision.CompareTag("Pared")){
            audio.clip = sndPared;
            audio.Play();
        }
    }
}