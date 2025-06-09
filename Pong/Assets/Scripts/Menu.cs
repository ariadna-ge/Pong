using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Text nivel1, nivel2, nivel3, lado1, lado2;
    public static int dificultad = 1; // 1 = Fácil, 2 = Normal, 3 = Difícil
    public static int lado = 1; // 1 = Izquierda, 2 = Derecha

    // NUEVA VARIABLE: Determina qué lado controlará la IA
    public static int ladoIA = 2; // Por defecto IA en derecha

    void Awake()
    {
        // Mostrar nivel fácil y lado izquierdo por defecto
        BorraSubrayado();
        nivel1.gameObject.SetActive(true);
        lado1.gameObject.SetActive(true);
        // Configurar IA en lado contrario
        ActualizarLadoIA();
    }

    void Update()
    {
        // Selección de dificultad
        if (Input.GetKey(KeyCode.Alpha1))
        {
            BorraSubrayado();
            nivel1.gameObject.SetActive(true);
            dificultad = 1; // Fácil
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            BorraSubrayado();
            nivel2.gameObject.SetActive(true);
            dificultad = 2; // Normal
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            BorraSubrayado();
            nivel3.gameObject.SetActive(true);
            dificultad = 3; // Difícil
        }

        // Selección del lado
        if (Input.GetKey(KeyCode.I)) // Letra I para Izquierda
        {
            BorraLados();
            lado1.gameObject.SetActive(true);
            lado = 1; // Izquierda
            ActualizarLadoIA(); // NUEVA FUNCIÓN
        }
        if (Input.GetKey(KeyCode.D)) // Letra D para Derecha
        {
            BorraLados();
            lado2.gameObject.SetActive(true);
            lado = 2; // Derecha
            ActualizarLadoIA(); // NUEVA FUNCIÓN
        }

        // Iniciar juego
        if (Input.GetKey(KeyCode.Space))
        {
            SceneManager.LoadScene("Main");
        }
    }

    // NUEVA FUNCIÓN: Actualiza el lado de la IA según la selección del jugador
    void ActualizarLadoIA()
    {
        ladoIA = (lado == 1) ? 2 : 1; // Si jugador elige izquierda, IA va a derecha y viceversa
        Debug.Log("Jugador en lado: " + (lado == 1 ? "Izquierda" : "Derecha") +
                  ", IA en lado: " + (ladoIA == 1 ? "Izquierda" : "Derecha"));
    }

    public void BorraSubrayado()
    {
        nivel1.gameObject.SetActive(false);
        nivel2.gameObject.SetActive(false);
        nivel3.gameObject.SetActive(false);
        BorraLados();
    }

    public void BorraLados()
    {
        lado1.gameObject.SetActive(false);
        lado2.gameObject.SetActive(false);
    }
}