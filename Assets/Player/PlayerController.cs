using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    /*[SerializeField]
    private Animator anim;*/

    [SerializeField]
    PlayerInput input;

    public float devant;
    public float courir;
    public float arriere;
    public float cote;

    void Start()
    {
        if(Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        GameObject.DontDestroyOnLoad(this.gameObject);

        Cursor.lockState = CursorLockMode.Locked;
        //anim = gameObject.GetComponent<Animator>();
    }

    void Update()
    {

        if (Input.GetKey(input.InputFront))
        {
            transform.Translate(0, 0, devant * Time.deltaTime);
        }

        if (Input.GetKey(input.InputBack))
        {
            transform.Translate(0, 0, -arriere * Time.deltaTime);
        }

        if (Input.GetKey(input.InputLeft))
        {
            transform.Translate(-cote * Time.deltaTime, 0, 0);
        }

        if (Input.GetKey(input.InputRight))
        {
            transform.Translate(cote * Time.deltaTime, 0, 0);
        }
    }
}
