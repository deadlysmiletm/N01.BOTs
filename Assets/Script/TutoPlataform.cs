using UnityEngine;
using System.Collections;

public class TutoPlataform : MonoBehaviour
{
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

    public void OnTriggerStay2D(Collider2D Player)
    {
        if(Player.gameObject.layer == 8)
        {
            CámaraScript.TutoPlataforma.gameObject.SetActive(true);
            if(Input.GetKeyDown(KeyCode.E))
            {
                CámaraScript.TutoPlataforma.gameObject.SetActive(false);
                Destroy(this.gameObject);
            }
        }
    }

    public void OnTriggerExit2D(Collider2D Player)
    {
        if(Player.gameObject.layer == 8)
        {
            CámaraScript.TutoPlataforma.gameObject.SetActive(false);
        }
    }
}
