using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Special : MonoBehaviour
{
    private Animator anima;

	void Start ()
    {
        anima = GetComponent<Animator>();
	}
	
	void Update ()
    {
        //special animation
		if(anima.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            GameManager.aS.PlayOneShot(GameManager.audios.sounds[3]);
            this.transform.parent.gameObject.SetActive(false);
            Destroy(gameObject);
        }
	}
}
