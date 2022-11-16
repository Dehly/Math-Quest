using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    private float time;

	void Start ()
    {

    }
	
	void Update ()
    {
        if(gameObject.activeSelf)
        {
            time += Time.deltaTime;
            if(time > 1)
            {
                gameObject.SetActive(false);
                time = 0;
            }
        }
	}
}
