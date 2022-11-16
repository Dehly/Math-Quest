using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class Explanation : MonoBehaviour
{
    public static Text explanation;
    private string[] addition;
    private string[] subtraction;
    private string path;
    public static int x;
    public string[] aux;
    public static int[] qtd;
    private int[] max;
    private int[] values;
    private Text[] valuesT;
    private GameObject value;
    private GameObject hero;
    public static GameObject[] slimes;
    public static GameObject[] bats;
    public static GameObject surprise;
    private Button left;
    private Button right;
    private Button play;
    private float time;
    public static float timeT;
    public static float timeLetters;
    public static int index;
    public static bool stop;
    public static bool change;

	void Start ()
    {
        timeLetters = 0.05f;
        GameManager.operation = 0;
        values = new int[3];
        valuesT = new Text[3];
        value = GameObject.Find("Values");
        valuesT[0] = GameObject.Find("Value1/Text").GetComponent<Text>();
        valuesT[1] = GameObject.Find("Result/Text").GetComponent<Text>();
        valuesT[2] = GameObject.Find("Value2/Text").GetComponent<Text>();
        explanation = GameObject.Find("TextExplanation").GetComponent<Text>();
        path = "Assets/Resources/MathQuestAddition.txt";
        addition = File.ReadAllText(path).Split(';');
        path = "Assets/Resources/MathQuestSubtraction.txt";
        subtraction = File.ReadAllText(path).Split(';');
        qtd = new int[addition.Length];
        max = new int[addition.Length];
        aux = new string[addition.Length];
        value.SetActive(false);
        hero = GameObject.Find("HeroExplain");
        hero.SetActive(false);
        surprise = GameObject.Find("!");
        surprise.SetActive(false);
        slimes = new GameObject[5];
        for (int i = 0; i < slimes.Length; i++)
        {
            slimes[i] = GameObject.Find("enemy" + i);
            slimes[i].SetActive(false);
        }
        bats = new GameObject[5];
        for (int i = 0; i < bats.Length; i++)
        {
            bats[i] = GameObject.Find("enemy" + (i + 5));
            bats[i].SetActive(false);
        }
        left = GameObject.Find("Left").GetComponent<Button>();
        right = GameObject.Find("Right").GetComponent<Button>();
        play = GameObject.Find("Play").GetComponent<Button>();
        time = 0;
        left.gameObject.SetActive(false);
        right.gameObject.SetActive(false);
        play.gameObject.SetActive(false);

    }

    void Update()
    {
        if(change)
        {
            if (GameManager.operation == 1)
            {
                values[0] = 32;
                values[1] = 0;
                values[2] = 45;
                max[1] = 3;
                max[2] = 2;
                max[3] = 5;
                for (int i = 0; i < aux.Length; i++)
                {
                    aux[i] = "is 0 slime.";
                }
            }
            else if (GameManager.operation == 2)
            {
                values[0] = 27;
                values[1] = 0;
                values[2] = 15;
                max[1] = 5;
                max[2] = 3;
                max[3] = 3;
                for (int i = 0; i < aux.Length; i++)
                {
                    aux[i] = "is 0 bat.";
                }
                aux[2] = "are 5 bats.";
            }
            change = false;
        }
        if (x == 0)
            left.interactable = false;
        else
            left.interactable = true;
        if (x == addition.Length - 1)
            right.interactable = false;
        else
            right.interactable = true;

        if (x > 0 && x < 4)
        {
            hero.SetActive(true);
            if (stop)
                time += Time.deltaTime;
            else
                time = 0;
            if(GameManager.operation == 1)
            {
                if (x != 2)
                {
                    for (int i = 0; i < slimes.Length; i++)
                    {
                        if (i < qtd[x])
                            slimes[i].SetActive(true);
                        else
                            slimes[i].SetActive(false);
                    }
                }
                else
                {
                    for (int i = 3; i < slimes.Length; i++)
                    {
                        if (i < qtd[x] + 3)
                            slimes[i].SetActive(true);
                        else
                            slimes[i].SetActive(false);
                    }
                }
                if (time > 0.75f)
                {
                    if (qtd[x] < max[x])
                    {
                        surprise.SetActive(true);
                        if (addition[x].Contains(aux[x]))
                        {
                            int a = addition[x].IndexOf(aux[x]);

                            qtd[x]++;
                            addition[x] = addition[x].Remove(a, aux[x].Length);
                            explanation.text = addition[x];
                            index -= aux[x].Length;
                            if (qtd[x] < 2)
                                aux[x] = "is " + qtd[x] + " slime.";
                            else
                                aux[x] = "are " + qtd[x] + " slimes.";
                            addition[x] = addition[x].Insert(a, aux[x]);
                        }
                        time = 0;
                    }
                    else
                    {
                        surprise.SetActive(false);
                    }
                }
            }
            if(GameManager.operation == 2)
            {
                if (x != 2)
                {
                    for (int i = 0; i < bats.Length; i++)
                    {
                        if (i < qtd[x])
                            bats[i].SetActive(true);
                        else
                            bats[i].SetActive(false);
                    }
                }
                else
                {
                    for (int i = bats.Length - 1; i >= 0; i--)
                    {
                        if (i >= qtd[x])
                            bats[i].SetActive(false);
                        else
                            bats[i].SetActive(true);
                    }
                }
                if (time > 0.75f)
                {
                    if(x != 2)
                    {
                        if (qtd[x] < max[x])
                        {
                            surprise.SetActive(true);
                            if (subtraction[x].Contains(aux[x]))
                            {
                                int a = subtraction[x].IndexOf(aux[x]);
                                qtd[x]++;
                                subtraction[x] = subtraction[x].Remove(a, aux[x].Length);
                                explanation.text = subtraction[x];
                                index -= aux[x].Length;
                                if (qtd[x] < 2)
                                    aux[x] = "is " + qtd[x] + " bat.";
                                else
                                    aux[x] = "are " + qtd[x] + " bats.";
                                subtraction[x] = subtraction[x].Insert(a, aux[x]);
                            }
                            time = 0;
                        }
                        else
                        {
                            surprise.SetActive(false);
                        }
                    }
                    else
                    {
                        if (qtd[x] > max[x])
                        {
                            surprise.SetActive(true);
                            if (subtraction[x].Contains(aux[x]))
                            {
                                int a = subtraction[x].IndexOf(aux[x]);
                                qtd[x]--;
                                subtraction[x] = subtraction[x].Remove(a, aux[x].Length);
                                explanation.text = subtraction[x];
                                index -= aux[x].Length;
                                if (qtd[x] < 2)
                                    aux[x] = "is " + qtd[x] + " bat.";
                                else
                                    aux[x] = "are " + qtd[x] + " bats.";
                                subtraction[x] = subtraction[x].Insert(a, aux[x]);
                            }
                            time = 0;
                        }
                        else
                        {
                            surprise.SetActive(false);
                        }
                    }
                }
            }
        }
        else
        {
            hero.SetActive(false);
            for (int i = 0; i < slimes.Length; i++)
            {
                slimes[i].SetActive(false);
            }
        }
        if (x == 4)
        {
            if(stop)
            {
                value.SetActive(true);
                time += Time.deltaTime;
                if (time > 0.2f)
                {
                    if (GameManager.operation == 1)
                    {
                        if (values[0] > 0)
                        {
                            values[0]--;
                            values[1]++;
                        }
                        else if (values[2] > 0)
                        {
                            values[2]--;
                            values[1]++;
                        }
                    }
                    else if (GameManager.operation == 2)
                    {
                        if (values[0] > 0)
                        {
                            values[0]--;
                            values[1]++;
                        }
                        else if (values[2] > 0)
                        {
                            values[2]--;
                            values[1]--;
                        }
                    }
                    time = 0;
                }
            }
        }
        else
        {
            value.SetActive(false);
        }
        for (int i = 0; i < valuesT.Length; i++)
        {
            valuesT[i].text = "" + values[i];
        }
        if(GameManager.operation == 1)
        {
            if (index < addition[x].Length)
            {
                if (timeT > timeLetters)
                {
                    explanation.text += addition[x].Substring(index, 1);
                    index++;
                    timeT = 0;
                }
            }
            else
            {
                stop = true;
            }
        }
        if(GameManager.operation == 2)
        {
            if (index < subtraction[x].Length)
            {
                if (timeT > timeLetters)
                {
                    explanation.text += subtraction[x].Substring(index, 1);
                    index++;
                    timeT = 0;
                }
            }
            else
            {
                stop = true;
            }
        }
        if(GameManager.operation > 0)
        {
            timeT += Time.deltaTime;
            left.gameObject.SetActive(true);
            right.gameObject.SetActive(true);
            play.gameObject.SetActive(true);
        }
        if(GameManager.operation == 0)
        {
            explanation.text = "";
        }
    }
}
