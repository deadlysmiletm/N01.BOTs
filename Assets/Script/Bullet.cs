using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed;
    public float Timer;
    public bool BossBullet;

    //Shoot direction.
    public float dir = 1;

	void Start ()
    {
        Destroy(this.gameObject, Timer);
	}
	
	void Update ()
    {
        transform.position += transform.right * Speed * dir * Time.deltaTime;
	}

    public void OnTriggerEnter2D (Collider2D muerte)
    {
        if(muerte.gameObject.layer == 9)
        {
            if(this.gameObject.layer == 12)
            {
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
        if(muerte.gameObject.layer == 8)
        {
            if (this.gameObject.layer == 12)
            {
                Destroy(this.gameObject);
            }
        }
        if (BossBullet == false)
        {
            if (muerte.gameObject.layer == 14 || muerte.gameObject.layer == 15)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
