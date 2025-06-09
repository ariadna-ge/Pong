using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAComputadora : MonoBehaviour
{
    public GameObject miPelota;
    Vector3 posicionPelota;
    float velocidad = 6.0f;
    private GameObject Jugador1, Jugador2;

    // NUEVAS VARIABLES
    private bool esJugadorIA = false; // Determina si este objeto es controlado por IA
    private float posicionXObjetivo; // Posición X donde debe estar la IA

    void Start()
    {
        Jugador1 = GameObject.Find("JugadorIzq").gameObject;
        Jugador2 = GameObject.Find("JugadorDrh").gameObject;
        AjustarVelocidadIA();

        // NUEVA LÓGICA: Determinar si este GameObject es la IA
        ConfigurarControlIA();
    }

    void Update()
    {
        if (Configuracion.tipojuego == 1 && esJugadorIA)
        {
            MovimientoIA();
        }

        // Resetear posiciones cuando la pelota sale del área de juego
        if (Mathf.Abs(posicionPelota.x) > 9)
        {
            ResetearPosiciones();
        }
    }

    // NUEVA FUNCIÓN: Configura qué jugador será controlado por IA
    void ConfigurarControlIA()
    {
        if (Menu.ladoIA == 1 && gameObject.name == "JugadorIzq")
        {
            esJugadorIA = true;
            posicionXObjetivo = -8f;
        }
        else if (Menu.ladoIA == 2 && gameObject.name == "JugadorDrh")
        {
            esJugadorIA = true;
            posicionXObjetivo = 8f;
        }
        else
        {
            esJugadorIA = false;
        }

        Debug.Log(gameObject.name + " - Es IA: " + esJugadorIA);
    }

    // NUEVA FUNCIÓN: Lógica de movimiento de la IA
    void MovimientoIA()
    {
        posicionPelota = miPelota.gameObject.transform.position;
        float deltaY = velocidad * Time.deltaTime + (float)Pelota.numToques / 500.0f;

        // Solo seguir la pelota si está en el área de juego
        if (posicionPelota.x >= -9 && posicionPelota.x <= 9)
        {
            Vector3 posicionObjetivo = new Vector3(posicionXObjetivo, posicionPelota.y, 0);
            transform.position = Vector3.MoveTowards(transform.position, posicionObjetivo, deltaY);
        }
    }

    // NUEVA FUNCIÓN: Resetea las posiciones de ambos jugadores
    void ResetearPosiciones()
    {
        Jugador1.transform.position = new Vector3(-8, 0, 0);
        Jugador2.transform.position = new Vector3(8, 0, 0);
    }

    void AjustarVelocidadIA()
    {
        if (Menu.dificultad == 1)
        {
            velocidad = 4.0f; // Modo Fácil - IA más lenta
        }
        else if (Menu.dificultad == 2)
        {
            velocidad = 6.0f; // Modo Normal - Velocidad estándar
        }
        else if (Menu.dificultad == 3)
        {
            velocidad = 8.0f; // Modo Difícil - IA más rápida
        }

        Debug.Log("Dificultad: " + Menu.dificultad + " - Velocidad IA: " + velocidad);
    }
}