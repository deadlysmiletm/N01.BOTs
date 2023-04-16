using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Main : MonoBehaviour
{
    public GameObject Player;
    public GameObject Salida;
    public GameObject GameEnd;
    public Vector3 Posición;
    public PlayerController PlayerScript;

    public Text Puntos;
    public Text Puertas;
    public Text Win;
    public Text Lose;
    public Text Pause;
    public Text TutoObjDes;
    public Text TutoPausa;
    public Text SinLlave;
    public Text VidaMáxima;
    public Text TutoPlataforma;
    public Text Objetivos;
    public Image Vida1;
    public Image Vida2;
    public Image Vida3;
    public Image Herida1;
    public Image Herida2;
    public Image Herida3;
    public Image Herida4;
    public Image Herida5;
    public Image ImageLlave;

    public Sprite Corazón;
    public Color Transparente;
    public Color Normal;

    public int PuertasObjetivo;
    public int Vidas;
    public int Puntaje = 0;
    public float Respawn;
    public float RespawnDefault;
    public bool pausa = false;
    public bool Llave = false;
    public int Nivel;

	void Start ()
    {
        Player = GameObject.Find("Chara");
        GameEnd = GameObject.Find("GameEnd");
        RespawnDefault = Respawn;
        PlayerScript = Player.GetComponent<PlayerController>();
    }
	
	void Update ()
    {
        Cámara();
        ObjetivoCompleto();
        VidasPersonaje();
        UIPuertas();
        UIPlayerHeridas();
        UIPLayerVidas();
        UIPuntaje();
        UIPause();
        UITutorial();
        UILlave();

        if(PlayerScript.end == true)
        {
            UIWin();
        }
	}

    public void ObjetivoCompleto()
    {
        if (PuertasObjetivo == 0)
        {
            Destroy(Salida.gameObject);
        }
    }

    public void VidasPersonaje()
    {
        if (Player.gameObject.activeInHierarchy == false)
        {
            Respawn -= Time.deltaTime;
            if (Respawn <= 0)
            {
                Player.SetActive(true);
                Respawn = RespawnDefault;
                Vidas--;
                PlayerScript.Vidas = 5;
            }
        }

        if (Vidas == 0)
        {
            PlayerScript.Vidas = 0;
            Destroy(Player.gameObject);
        }
    }

    public void Cámara()
    {
        if (PlayerScript.end == false)
        {
            transform.position = Player.transform.position + Posición;
        }
    }

    public void UIPuertas()
    {
        Puertas.text = "Doors: " + PuertasObjetivo;
    }

    public void UIPlayerHeridas()
    {
        if(PlayerScript.Vidas == 4)
        {
            Herida5.color = Transparente;
        }
        if (PlayerScript.Vidas == 3)
        {
            Herida4.color = Transparente;
        }
        if (PlayerScript.Vidas == 2)
        {
            Herida3.color = Transparente;
        }
        if (PlayerScript.Vidas == 1)
        {
            Herida2.color = Transparente;
        }
        if (PlayerScript.Vidas == 0)
        {
            Herida1.color = Transparente;
            Herida2.color = Transparente;
            Herida3.color = Transparente;
            Herida4.color = Transparente;
            Herida5.color = Transparente;
        }
        else if(PlayerScript.Vidas == 5)
        {
            Herida1.color = Normal;
            Herida2.color = Normal;
            Herida3.color = Normal;
            Herida4.color = Normal;
            Herida5.color = Normal;
        }
    }

    public void UIPLayerVidas()
    {
        if(Vidas == 3)
        {
            Vida3.color = Normal;
            Vida2.color = Normal;
            Vida1.color = Normal;
        }

        if (Vidas == 2)
        {
            Vida3.color = Transparente;
            Vida2.color = Normal;
            Vida1.color = Normal;
        }
        if(Vidas == 1)
        {
            Vida2.color = Transparente;
            Vida1.color = Normal;
            Respawn = 0;
        }
        if(Vidas == 0)
        {
            Vida3.color = Transparente;
            Vida2.color = Transparente;
            Vida1.color = Transparente;
            UILose();
        }
    }

    public void UIPuntaje()
    {
        Puntos.text = "Score: " + Puntaje;
    }

    public void UIWin()
    {
        Win.gameObject.SetActive(true);
    }

    public void UILose()
    {
        if(Lose.gameObject.activeInHierarchy == false)
        {
            Lose.gameObject.SetActive(true);
        }
    }

    public void UIPause()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (pausa == false)
            {
                pausa = true;
            }
            else
            {
                pausa = false;
            }
        }
        MenúPausa();
    }

    public void MenúPausa()
    {
        if(pausa == true)
        {
            Pause.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        if(pausa == false)
        {
            Pause.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void UITutorial()
    {
            if (PlayerScript.ObjDes == true)
            {
                TutoObjDes.gameObject.SetActive(true);
            }
            else if (PlayerScript.ObjDes == false)
            {
                TutoObjDes.gameObject.SetActive(false);
            }
            if (PlayerScript.Tutopausa == true)
            {
                TutoPausa.gameObject.SetActive(true);
            }
            else if (PlayerScript.Tutopausa == false)
            {
                TutoPausa.gameObject.SetActive(false);
        }

    }

    public void UILlave()
    {
        if(Llave == true)
        {
            ImageLlave.gameObject.SetActive(true);
        }
        else
        {
            ImageLlave.gameObject.SetActive(false);
        }
    }
}
