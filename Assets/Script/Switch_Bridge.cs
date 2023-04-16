using UnityEngine;
using System.Collections;

public class Switch_Bridge : MonoBehaviour
{
    public GameObject TopePuente;

	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
	}

    public void OnTriggerEnter2D(Collider2D condición)
    {
        if(condición.gameObject.layer == 8)
        {
            Destroy(TopePuente.gameObject);
            Destroy(this.gameObject);
        }
    }
}
