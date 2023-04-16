using UnityEngine;
using System.Collections;

public class EndDoors : MonoBehaviour
{
    public GameObject PuertasFinales;
    public GameObject Cámara;
    public GameObject Boss;
    public Camera CámaraC;
    public Main CámaraScript;
    public First_Boss Jefe;

    public BoxCollider2D bc;

	// Use this for initialization
	void Start ()
    {
        CámaraScript = Cámara.gameObject.GetComponent<Main>();
        Jefe = Boss.gameObject.GetComponent<First_Boss>();
        CámaraC = Cámara.gameObject.GetComponent<Camera>();
        bc = GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(Jefe.Vida <= 0)
        {
            Destroy(Boss.gameObject);
            PuertasFinales.gameObject.SetActive(false);
            CámaraScript.Puntaje += 10000;
            Jefe.Vida = 1;
            CámaraC.orthographicSize = 7.30198f;
            CámaraScript.Posición = new Vector3(CámaraScript.Posición.x, 3, CámaraScript.Posición.z);
        }
	
	}

    public void OnTriggerEnter2D(Collider2D Player)
    {
        if(Player.gameObject.layer == 8)
        {
                PuertasFinales.gameObject.SetActive(true);
                CámaraC.orthographicSize = 24.63986f;
                CámaraScript.Posición = new Vector3(CámaraScript.Posición.x, 10, CámaraScript.Posición.z);
                Boss.gameObject.SetActive(true);
                bc.enabled = false;
        }
    }
}
