using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour
{

	void Start ()
    {
	}
	
	void Update ()
    {
	}

    public void OnTriggerEnter2D(Collider2D bala)
    {
        if(bala.gameObject.layer == 12)
        {
            bala.gameObject.layer = 8;
            Destroy(bala.gameObject);
        }
    }
}
