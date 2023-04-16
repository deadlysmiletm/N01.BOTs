using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    public GameObject Cámara;
    public Main CámaraScript;

	void Start ()
    {
        Cámara = GameObject.Find("Main Camera");
        CámaraScript = Cámara.GetComponent<Main>();
	}
	
	void Update ()
    {
	}

    public void CambioEscena(string escena)
    {
        SceneManager.LoadScene(escena);
    }

    public void Pausa(bool EstadoPausa)
    {
        CámaraScript.pausa = EstadoPausa;
    }

    public void Quit(bool Salir = false)
    {
        if(Salir == true)
        {
            Application.Quit();
        }
    }
}
