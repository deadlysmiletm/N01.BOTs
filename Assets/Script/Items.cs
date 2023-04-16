using UnityEngine;
using System.Collections;

public class Items : MonoBehaviour
{

    public bool Llave;
    public bool Bonus;
    public bool Arma;
    public bool Vida;

    public GameObject Cámara;
    public Main CámaraScript;

	// Use this for initialization
	void Start ()
    {
        CámaraScript = Cámara.GetComponent<Main>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	}

    public void OnTriggerEnter2D (Collider2D player)
    {
        if (player.gameObject.layer == 8)
        {
            if (Llave == true)
            {
                CámaraScript.Llave = true;
                Destroy(this.gameObject);
            }
            else if (Bonus == true)
            {
                CámaraScript.Puntaje += 2000;
                Destroy(this.gameObject);
            }
            else if (Vida == true)
            {
                if (CámaraScript.Vidas < 3)
                {
                    CámaraScript.Vidas += 1;
                    CámaraScript.Respawn += 1;
                    Destroy(this.gameObject);
                }
                else
                {
                    CámaraScript.VidaMáxima.gameObject.SetActive(true);
                }
            }
        }

    }

    public void OnTriggerExit2D(Collider2D player)
    {
        if(player.gameObject.layer == 8)
        {
            if(CámaraScript.VidaMáxima.isActiveAndEnabled == true)
            {
                CámaraScript.VidaMáxima.gameObject.SetActive(false);
            }
        }
    }

}
