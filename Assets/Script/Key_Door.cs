using UnityEngine;
using System.Collections;

public class Key_Door : MonoBehaviour
{
    public GameObject Puerta;
    public GameObject Cámara;
    public Main CámaraScript;


	// Use this for initialization
	void Start ()
    {
        CámaraScript = Cámara.gameObject.GetComponent<Main>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	}

    public void OnTriggerEnter2D(Collider2D Player)
    {
        if(Player.gameObject.layer == 8)
        {
            if(CámaraScript.Llave == true)
            {
                CámaraScript.Llave = false;
                Destroy(Puerta.gameObject);
            }
            else
            {
                CámaraScript.SinLlave.gameObject.SetActive(true);
            }
        }
    }

    public void OnTriggerExit2D(Collider2D Player)
    {
        if(Player.gameObject.layer == 8)
        {
            CámaraScript.SinLlave.gameObject.SetActive(false);
        }
    }
}
