using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Math : MonoBehaviour
{
    private int x;
    private int y;
    private int z;
    private int resultado;
    private string values;
    public char[] op = { '+', '-', '*', '/' };

    private float tempo = 0;

    //public Transform[] respostas;
    public Text[] text;
    public Text[] hud;

    void Start()
    {
        values = "";
        text = new Text[5];
        text = GameObject.Find("Values").GetComponentsInChildren<Text>();
        hud = new Text[1];
        hud[0] = GameObject.Find("Equation").GetComponent<Text>();
        GameManager.difficulty = 0;
        Randomize(text);
    }
	void Update ()
    {
        for (int i = 0; i < text.Length; i++)
        {
            text[i].transform.position = new Vector2(AlternativesMovement.enemies[i].transform.position.x, AlternativesMovement.enemies[i].transform.position.y + .5f);
        }
        tempo += Time.deltaTime;
        if (GameManager.next)
        {
            for (int i = 0; i < AlternativesMovement.enemies.Length; i++)
            {
                AlternativesMovement.enemies[i].gameObject.SetActive(true);
            }
            tempo = 0;
            Randomize(text);
            GameManager.next = false;
        }
        if (tempo >= 11f)
            GameManager.next = true;
	}
    private int Operation(int x, int y, int z = 0)
    {
        int resultado = 0;
        if (GameManager.operation == 1)
        {
            if(GameManager.difficulty % 2 == 0)
                resultado = x + y;
            else
                resultado = x + y + z;
        }
        else if (GameManager.operation == 2)
        {
            if (GameManager.difficulty % 2 == 0)
                resultado = x - y;
            else
                resultado = x - y - z;
        }
        else if (GameManager.operation == 3)
            resultado = x * y;
        else if (GameManager.operation == 4 && y != 0)
            resultado = x / y;
        return resultado;
    }
    private Text[] Randomize(Text[] text)
    {
        switch(GameManager.difficulty / 2)
        {
            case 0:
                x = Random.Range(0, 10);
                y = Random.Range(0, 10);
                z = Random.Range(0, 10);
                break;
            case 1:
                x = Random.Range(0, 10);
                y = Random.Range(0, 100);
                z = Random.Range(0, 100);
                break;
            case 2:
                x = Random.Range(0, 100);
                y = Random.Range(0, 500);
                z = Random.Range(0, 500);
                break;
            case 3:
                x = Random.Range(0, 500);
                y = Random.Range(0, 1000);
                z = Random.Range(0, 1000);
                break;
            case 4:
                x = Random.Range(0, 5000);
                y = Random.Range(0, 5000);
                z = Random.Range(0, 5000);
                break;
        }
        if (GameManager.difficulty % 2 == 0)
        {
            hud[0].text = "" + x + " " + op[GameManager.operation-1] + " " + y;
        }
        else
        {
            int a = Random.Range(0, 4);
            switch (a)
            {
                case 0:
                    hud[0].text = "" + x + " " + op[GameManager.operation - 1] + " " + y + " " + op[GameManager.operation - 1] + " " + z;
                    break;
                case 1:
                    hud[0].text = "(" + x + " " + op[GameManager.operation - 1] + " " + y + ") " + op[GameManager.operation - 1] + " " + z;
                    break;
                case 2:
                    hud[0].text = "" + x + " " + op[GameManager.operation - 1] + " (" + y + " " + op[GameManager.operation - 1] + " " + z + ")";
                    break;
                case 3:
                    hud[0].text = "(" + x + " " + op[GameManager.operation - 1] + " " + y + " " + op[GameManager.operation - 1] + " " + z + ")";
                    break;
            }
        }
        if (GameManager.difficulty % 2 == 0)
            resultado = Operation(x, y);
        else
            resultado = Operation(x, y, z);
        int r = Random.Range(0, 5);
        for (int i = 0; i < 5; i++)
        {
            if (i == r)
            {
                AlternativesMovement.enemies[i].tag = "Certa";
                text[i].text = "" + resultado;
            }
            else
            {
                AlternativesMovement.enemies[i].tag = "Errada";
                int a = 0;

                if (GameManager.operation == 1 || GameManager.operation == 2)
                {
                    switch(GameManager.difficulty /2)
                    {
                        case 0:
                            a = Random.Range(resultado - 5, resultado + 5);
                            while (a == resultado || a < 0 || values.Contains(a.ToString()))
                                a = Random.Range(resultado - 5, resultado + 5);
                            break;
                        case 1:
                            a = Random.Range(resultado - 10, resultado + 10);
                            while (a == resultado || a < 0 || values.Contains(a.ToString()))
                                a = Random.Range(resultado - 10, resultado + 10);
                            break;
                        case 2:
                            a = Random.Range(resultado - 50, resultado + 50);
                            while (a == resultado || a < 0 || values.Contains(a.ToString()))
                                a = Random.Range(resultado - 50, resultado + 50);
                            break;
                        case 3:
                            a = Random.Range(resultado - 100, resultado + 100);
                            while (a == resultado || a < 0 || values.Contains(a.ToString()))
                                a = Random.Range(resultado - 100, resultado + 100);
                            break;
                        case 4:
                            a = Random.Range(resultado - 500, resultado + 500);
                            while (a == resultado || a < 0 || values.Contains(a.ToString()))
                                a = Random.Range(resultado - 500, resultado + 500);
                            break;
                    }
                }
                text[i].text = "" + a;
                values += a.ToString();
            }
        }
        values = "";
        return text;
    }
}
