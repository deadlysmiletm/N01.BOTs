using UnityEngine;
using System.Collections;

public class OutMap : MonoBehaviour
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

    public void OnTriggerEnter2D(Collider2D muerte)
    {
        if(muerte.gameObject)
        {
            Destroy(muerte.gameObject, 0.5f);
            if (muerte.gameObject.layer == 8)
            {
                CámaraScript.Vidas = 0;
            }
        }
    }

}
