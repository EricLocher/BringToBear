using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MamaBearScript : MonoBehaviour
{
    public Animator animator;
    float offScreenPos = -200;
    float collectionPos = -160;


    [SerializeField] MamaBearBehaviour state = MamaBearBehaviour.Absent;

    void Start()
    {
        transform.position = new Vector2(0, offScreenPos);
        animator.SetBool("Collect", false);
        StartCoroutine(Timer(1));
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
        transform.position = Vector2.Lerp(transform.position, new Vector2(0, offScreenPos), 0.003f);

    }

    void Arriving()
    {
        transform.position = Vector2.Lerp(transform.position, new Vector2(0, collectionPos), 0.003f);
    }

    void Collecting()
    {
        animator.SetBool("Collect", true);
    }

    void Closing()
    {
        animator.SetBool("Collect", false);
    }

    IEnumerator Timer(float time)
    {
        yield return new WaitForSeconds(time);

        switch (state)
        {
            case MamaBearBehaviour.Absent:
                state = MamaBearBehaviour.Arriving;
                StartCoroutine(Timer(10));
                break;
            case MamaBearBehaviour.Arriving:
                state = MamaBearBehaviour.Collecting;
                StartCoroutine(Timer(3));
                break;
            case MamaBearBehaviour.Collecting:
                state = MamaBearBehaviour.Closing;
                StartCoroutine(Timer(5));
                break;
            case MamaBearBehaviour.Closing:
                state = MamaBearBehaviour.Absent;
                StartCoroutine(Timer(3));
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