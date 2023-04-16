using UnityEngine;

public class Door : MonoBehaviour
{
    public SpriteRenderer sr;
    public Sprite Normal;
    public Sprite ObjectiveDoor;

    public GameObject Camera;
    public Main CameraScript;

    public int DoorId;
    public int IsSelected;
    public bool IsTargetDoor = false;

	void Start ()
    {
        DoorId = Random.Range(0, 10);
        IsSelected = Random.Range(0, 10);
        sr = GetComponent<SpriteRenderer>();

        Camera = GameObject.Find("Main Camera");
        CameraScript = Camera.GetComponent<Main>();

        if (DoorId == IsSelected)
        {
            sr.sprite = ObjectiveDoor;
            CameraScript.PuertasObjetivo ++;
        }
    }
	
	void Update ()
    {
        if(sr.sprite == ObjectiveDoor && IsTargetDoor == true)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                sr.sprite = Normal;
                CameraScript.PuertasObjetivo --;
                CameraScript.Puntaje += 500;
            }
        }
    }

    public void OnTriggerStay2D(Collider2D Jugador)
    {
        if(Jugador.gameObject.layer == 8)
        {
            IsTargetDoor = true;
        }
    }

    public void OnTriggerExit2D(Collider2D Jugador)
    {
        if(Jugador.gameObject.layer == 8)
        {
            IsTargetDoor = false;
        }
    }
}
