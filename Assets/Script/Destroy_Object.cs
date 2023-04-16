using UnityEngine;

public class Destroy_Object : MonoBehaviour
{
    public GameObject Objeto;
    public TargetJoint2D tj;

	void Start ()
    {
        tj = Objeto.gameObject.GetComponent<TargetJoint2D>();
	}

    public void OnTriggerEnter2D(Collider2D impacto)
    {
        if (impacto.gameObject.layer == 10)
        {
            Destroy(tj);
            Destroy(impacto.gameObject);
            Destroy(this.gameObject);
        }
    }
}
