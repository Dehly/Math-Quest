using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audios : MonoBehaviour
{
    public AudioClip[] audios; //musics to the game
    public AudioClip[] sounds; //sounds to the game

	void Start ()
    {
        DontDestroyOnLoad(this);
	}
}
