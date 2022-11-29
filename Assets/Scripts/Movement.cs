using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
    private Animator anima;

	void Start ()
    {
        anima = GetComponent<Animator>();
	}
	
	void Update ()
    {
        //play this music if player dies
        if(GameManager.aS.clip == GameManager.audios.audios[1] && !anima.GetBool("Death"))
        {
            GameManager.aS.PlayOneShot(GameManager.audios.sounds[1]);
            anima.SetBool("Death", true);
        }
        //gameover music
        if (GameManager.aS.clip == GameManager.audios.audios[2])
        {
            anima.SetBool("Death", false);
        }
        //movement to the player
        if(!GameManager.death)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                transform.position = new Vector3(transform.position.x - 3, transform.position.y, transform.position.z);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                transform.position = new Vector3(transform.position.x + 3, transform.position.y, transform.position.z);
            }
            //screen limit
            if (transform.position.x < -6)
                transform.position = new Vector3(-6, transform.position.y, transform.position.z);
            if (transform.position.x > 6)
                transform.position = new Vector3(6, transform.position.y, transform.position.z);
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        //collision to the right answer
        if (col.gameObject.tag == "Certa")
        {
            GameManager.aS.PlayOneShot(GameManager.audios.sounds[2]);
            GameManager.points += 1 * GameManager.factor;
            if (GameManager.points > 999)
                GameManager.points = 999;
            GameManager.cure++;
            GameManager.focus++;
            GameManager.next = true;
        }
        //collision to the wrong answer
        if (col.gameObject.tag == "Errada")
        {
            GameManager.aS.PlayOneShot(GameManager.audios.sounds[0]);
            GameManager.life--;
            GameManager.cure = 0;
            GameManager.focus = 0;
            GameManager.next = true;
        }
    }
}
