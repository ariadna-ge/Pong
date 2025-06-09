using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugadores : MonoBehaviour
{
    public KeyCode teclaArriba, teclaAbajo;
    private Rigidbody2D rb2d;

    // NUEVA VARIABLE
    private bool esControlableJugador = true;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        // NUEVA LÓGICA: Determinar si este jugador es controlable por el humano
        ConfigurarControlJugador();
    }

    void Update()
    {
        // CORRECCIÓN: Permitir control basado en el tipo de juego
        // Modo 1: Un jugador vs IA - solo el jugador seleccionado se mueve
        // Modo 2: Dos jugadores - ambos jugadores se mueven
        bool puedeMoverse = false;

        if (Configuracion.tipojuego == 1) // Un jugador vs IA
        {
            puedeMoverse = esControlableJugador;
        }
        else if (Configuracion.tipojuego == 2) // Dos jugadores
        {
            puedeMoverse = true; // Ambos jugadores pueden moverse
        }

        if (puedeMoverse && Pelota.numToques <= 20)
        {
            if (Input.GetKey(teclaArriba))
            {
                rb2d.MovePosition(rb2d.position + (Vector2.up * Time.deltaTime * JUego.velJugador) + new Vector2(0, Pelota.numToques / 100.0f));
            }

            if (Input.GetKey(teclaAbajo))
            {
                rb2d.MovePosition(rb2d.position + (Vector2.down * Time.deltaTime * JUego.velJugador) - new Vector2(0, Pelota.numToques / 100.0f));
            }
        }
    }

    // NUEVA FUNCIÓN: Configura qué jugador será controlable por el humano
    void ConfigurarControlJugador()
    {
        if (Menu.lado == 1 && gameObject.name == "JugadorIzq")
        {
            esControlableJugador = true;
        }
        else if (Menu.lado == 2 && gameObject.name == "JugadorDrh")
        {
            esControlableJugador = true;
        }
        else
        {
            esControlableJugador = false;
        }

        Debug.Log(gameObject.name + " - Controlable por jugador: " + esControlableJugador +
                  " - Tipo de juego: " + Configuracion.tipojuego);
    }
}