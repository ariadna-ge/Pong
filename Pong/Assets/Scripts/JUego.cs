using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class JUego : MonoBehaviour
{
    public AudioSource audio;
    public AudioClip sndSilbato, sndGameOver;
    public Text GameOver;

    private GameObject Marcador;
    private GameObject pelota;

    public static float velBola = 6.0f, velJugador = 7.5f;
    private int signoX, signoY, velocidad = 5;

    // Start is called before the first frame update
    void Start()
    {
        // IMPORTANTE: Reiniciar los goles al inicio del juego
        Pelota.golesJugadoreDrh = 0;
        Pelota.golesJugadorIzq = 0;
        
        GameOver.gameObject.SetActive(false);
        audio = GetComponent<AudioSource>();
        pelota = GameObject.Find("Pelota");
        Marcador = GameObject.Find("Marcador");
        
        // Actualizar el marcador inicial
        ActualizarMarcador();
        
        AjustarVelocidad();

        // CORRECCIÓN: Usar float para Random.Range
        if (Random.Range(0f, 1f) > 0.5f) {
            signoX = 1;
        }
        else{
            signoX = -1;
        }

        StartCoroutine(ArbitroPitaInicio());
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("Inicio");
        }

        if (Pelota.golesJugadoreDrh == 3 || Pelota.golesJugadorIzq == 3)
        {
            if (Input.anyKey)
            {
                Pelota.golesJugadoreDrh = 0;
                Pelota.golesJugadorIzq = 0;
                SceneManager.LoadScene("Inicio");
            }
        }
    }

    // Método separado para actualizar el marcador
    private void ActualizarMarcador()
    {
        Marcador.GetComponent<Text>().text = Pelota.golesJugadorIzq.ToString() + " - " + Pelota.golesJugadoreDrh.ToString();
    }

    public void EscribeMarcador()
    {
        // Validar que los goles no sean negativos
        if (Pelota.golesJugadorIzq < 0) Pelota.golesJugadorIzq = 0;
        if (Pelota.golesJugadoreDrh < 0) Pelota.golesJugadoreDrh = 0;
        
        ActualizarMarcador();
        
        if (Pelota.golesJugadoreDrh == 3 || Pelota.golesJugadorIzq == 3)
        {
            GameOver.gameObject.SetActive(true);
            audio.clip = sndGameOver;
            audio.Play();
        }
        else 
        {
            StartCoroutine(ArbitroPitaInicio());
        }
    }

    IEnumerator ArbitroPitaInicio()
    {
        yield return new WaitForSeconds(1.0f);
        LanzaPelota();
    }

    public void LanzaPelota()
    {
        audio.clip = sndSilbato;
        audio.Play();
        pelota.transform.position = new Vector3(0, 0, 0);
        
        // CORRECCIÓN: Usar float y simplificar la lógica
        signoY = Random.Range(0f, 1f) > 0.5f ? 1 : -1;
        
        pelota.GetComponent<Rigidbody2D>().velocity = new Vector2(signoX * velocidad, signoY * velocidad);
        
        // Alternar dirección X para el próximo lanzamiento
        signoX *= -1;
    }

    public void AjustarVelocidad()
    {
        if (Menu.dificultad == 1)
        {
            velBola = 3.5f;  // Bola más lenta
            velJugador = 8.0f;  // Jugador más rápido
            Debug.Log("Nivel: Facil - Velocidad Bola: " + velBola + ", Jugador: " + velJugador);
        }
        else if (Menu.dificultad == 2)
        {
            velBola = 5.0f;  // Velocidad media
            velJugador = 7.5f;
            Debug.Log("Nivel: Normal - Velocidad Bola: " + velBola + ", Jugador: " + velJugador);
        }
        else if (Menu.dificultad == 3)
        {
            velBola = 9.5f;  // Bola más rápida
            velJugador = 6.5f;  // Jugador más lento
            Debug.Log("Nivel: Difícil - Velocidad Bola: " + velBola + ", Jugador: " + velJugador);
        }
    }

    // Método adicional para depuración
    void OnApplicationPause(bool pauseStatus)
    {
        if (!pauseStatus)
        {
            Debug.Log($"Marcador actual: {Pelota.golesJugadorIzq} - {Pelota.golesJugadoreDrh}");
        }
    }
}