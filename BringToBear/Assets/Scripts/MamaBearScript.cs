using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MamaBearScript : MonoBehaviour
{
    public GameObject mouth;
    public Animator animator;
    float offScreenPos = -300;
    float collectionPos = -120;
    public GameObject aproaching;
    public GameObject collecting;
    public GameObject leaving;


    [SerializeField] MamaBearBehaviour state = MamaBearBehaviour.Absent;

    void Start()
    {
        transform.position = new Vector2(0, offScreenPos);
        animator.SetBool("Collect", false);
        mouth.GetComponent<BoxCollider2D>().enabled = false;
        StartCoroutine(Timer(60));
        aproaching.GetComponent<SpriteRenderer>().enabled = false;
        collecting.GetComponent<SpriteRenderer>().enabled = false;
        leaving.GetComponent<SpriteRenderer>().enabled = false;
    }

    void Update()
    {

        switch (state)
        {
            case MamaBearBehaviour.Absent:
                Absent();
                break;
            case MamaBearBehaviour.Arriving:
                Arriving();
                break;
            case MamaBearBehaviour.Collecting:
                Collecting();
                break;
            case MamaBearBehaviour.Closing:
                Closing();
                break;
        }

    }

    void Absent()
    {
        leaving.GetComponent<SpriteRenderer>().enabled = false;
        transform.position = Vector2.Lerp(transform.position, new Vector2(0, offScreenPos), 0.0005f);

    }

    void Arriving()
    {
        transform.position = Vector2.Lerp(transform.position, new Vector2(0, collectionPos), 0.003f);
        GameController.ChangeGameState(GameStates.MamaBearAproaching);
        aproaching.GetComponent<SpriteRenderer>().enabled = true;
    }

    void Collecting()
    {
        aproaching.GetComponent<SpriteRenderer>().enabled = false;
        collecting.GetComponent<SpriteRenderer>().enabled = true;
        GameController.ChangeGameState(GameStates.Collecting);
        animator.SetBool("Collect", true);
        mouth.GetComponent<BoxCollider2D>().enabled = true;
    }

    void Closing()
    {
        collecting.GetComponent<SpriteRenderer>().enabled = false;
        leaving.GetComponent<SpriteRenderer>().enabled = true;
        animator.SetBool("Collect", false);
        mouth.GetComponent<BoxCollider2D>().enabled = false;
        GameController.ChangeGameState(GameStates.Playing);
       
    }

    IEnumerator Timer(float time)
    {
        yield return new WaitForSeconds(time);

        switch (state)
        {
            case MamaBearBehaviour.Absent:
                state = MamaBearBehaviour.Arriving;
                StartCoroutine(Timer(5));
                break;
            case MamaBearBehaviour.Arriving:
                state = MamaBearBehaviour.Collecting;
                StartCoroutine(Timer(20));
                break;
            case MamaBearBehaviour.Collecting:
                state = MamaBearBehaviour.Closing;
                StartCoroutine(Timer(5));
                break;
            case MamaBearBehaviour.Closing:
                state = MamaBearBehaviour.Absent;
                StartCoroutine(Timer(30));
                break;
        }

    }

}




public enum MamaBearBehaviour
{ 
    Absent,
    Arriving,
    Collecting,
    Closing
}