using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Configuracion : MonoBehaviour

{
    public Text op1, op2;
    public static int tipojuego = 1;
    

    
    // Start is called before the first frame update
    void Awake(){
        tipojuego = 1;
        op1.gameObject.SetActive(true);
        op2.gameObject.SetActive(false);
        
    }

    // Update is called once per frame
    void Update(){
        if (Input.GetKey(KeyCode.Alpha1)){
            BorraSubrayado();
            op1.gameObject.SetActive(true);
            tipojuego = 1;
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            BorraSubrayado();
            op2.gameObject.SetActive(true);
            tipojuego = 2;
        }
        if (Input.GetKey(KeyCode.Space))

        {
            BorraSubrayado();

            
            if (tipojuego == 1)
            {
                SceneManager.LoadScene("Menu"); // Cargar la escena Menu si es tipojuego 1
            }
            else
            {
                SceneManager.LoadScene("Main"); // Cargar la escena Main si es tipojuego 2
            }

        }

    }

    public void BorraSubrayado()
    {
        op1.gameObject.SetActive(false);
        op2.gameObject.SetActive(false);
    }

   
}
