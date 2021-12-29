using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDisable : MonoBehaviour
{
    [SerializeField] float delay = 0;
    public float Timer = 0;

    void Start()
    {
        
    }

    private void Update()
    {
        Timer += Time.deltaTime;
        if (Timer > GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + delay)
        {
            Timer = 0;
            gameObject.SetActive(false);
        }
    }
}
