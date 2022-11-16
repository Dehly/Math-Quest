using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool startG;
    public static bool death;
    public static bool next;
    public static bool magic;
    public static bool cast;
    public static bool run;
    public static int points;
    public static int difficulty;
    public static int life;
    public static int sp;
    public static int rand;
    public static int focus;
    public static int cure;
    public static int factor;
    public static int operation;
    public static int ads;
    public static int spell;
    public static int[] spellValue;
    public static int[] statsLevel;
    public static int[] level;
    public static int[] statsValue;
    public static int[] mathValue;
    public static float timeD;
    public static float timeM;
    public static string[] infos;
    public static GameObject thunder;
    public static GameObject panelCredits;
    public static GameObject yesBtn;
    public static GameObject noBtn;
    public static GameObject miss;
    public static GameObject recover;
    public static Image special;
    public static Image spellImage;
    public static Math math;
    public static AudioSource aS;
    public static Audios audios;
    public static Text credits;
    public static Text[] timeT;

    private string[] spellNames;
    private string[] statsNames;
    private int[] statsLimits;
    private Image[] lifes;
    private Image[] stages;
    private Image[] status;
    private Image[] mathPoints;
    private Image[][] spellCosts;
    private Image[][] statsCosts;
    private Button[] spells;
    private Button[] stats;
    private Button[] adsButton;
    private GameObject retry;

    private Text versionTxt;
	void Start ()
    {
        credits = GameObject.Find("CreditsText").GetComponent<Text>();
        if (SceneManager.GetActiveScene().name == "Start")
        {
            panelCredits = GameObject.Find("PanelInfo");
            yesBtn = GameObject.Find("Yes");
            noBtn = GameObject.Find("No");
            yesBtn.SetActive(false);
            noBtn.SetActive(false);
            panelCredits.SetActive(false);
            versionTxt = GameObject.Find("Version_Txt").GetComponent<Text>();
            versionTxt.text = "Ver. " + Application.version;
        }
        GameDatas.Load();
        infos = File.ReadAllText("Assets/Resources/MathQuestInfo.txt").Split(';');
        DontDestroyOnLoad(this);
        spellValue = new int[5] { 25, 75, 80, 100, 200 };
        statsValue = new int[3] { 30, 50, 75 };
        mathPoints = new Image[3];
        spellNames = new string[5] { "Thunder", "Ice", "Rock", "Fire", "Heal" };
        statsNames = new string[3] { "Lives", "Special", "Stage" };
        statsLimits = new int[3] { 4, 9, 9 };
        spells = new Button[5];
        stats = new Button[3];
        stages = new Image[10];
        status = new Image[3];
        statsCosts = new Image[3][];
        for (int i = 0; i < statsCosts.Length; i++)
        {
            statsCosts[i] = new Image[3];
        }
        spellCosts = new Image[5][];
        for (int i = 0; i < spellCosts.Length; i++)
        {
            spellCosts[i] = new Image[3];
        }
        audios = FindObjectOfType<Audios>();
        lifes = new Image[5];
        if (SceneManager.GetActiveScene().name != "Start")
            startG = true;
        timeT = new Text[2];
        adsButton = new Button[2];
	}
	
	void Update ()
    {
        if(SceneManager.GetActiveScene().name == "Start" && startG)
        {
            panelCredits = GameObject.Find("PanelInfo");
            credits = GameObject.Find("CreditsText").GetComponent<Text>();
            if (panelCredits != null)
                panelCredits.SetActive(false);
            startG = false;
        }
        if(SceneManager.GetActiveScene().name == "Skills" && startG)
        {
            panelCredits = GameObject.Find("PanelInfo");
            credits = GameObject.Find("CreditsText").GetComponent<Text>();
            if (panelCredits != null)
                panelCredits.SetActive(false);
            startG = false;
        }
        if (SceneManager.GetActiveScene().name == "Game" && startG)
        {
            miss = GameObject.Find("PanelMiss");
            recover = GameObject.Find("Recover");
            miss.SetActive(false);
            recover.SetActive(false);
            for (int i = 0; i < lifes.Length; i++)
            {
                lifes[i] = GameObject.Find("HP" + i).GetComponent<Image>();
            }
            math = FindObjectOfType<Math>();
            aS = null;
            aS = FindObjectOfType<AudioSource>();
            retry = GameObject.Find("RetryPanel");
            thunder = Resources.Load<GameObject>("Thunder");
            special = null;
            special = GameObject.Find("Canvas/HUD/SP/Image").GetComponent<Image>();
            if(retry != null)
                retry.SetActive(false);
            life = statsLevel[0] + 1;
            aS.clip = audios.audios[0];
            mathPoints = GameObject.Find("Canvas/HUD/MP").GetComponentsInChildren<Image>();
            spellImage = GameObject.Find("Cast").GetComponent<Image>();
            //spellImage.gameObject.SetActive(false);
            panelCredits = GameObject.Find("PanelInfo");
            credits = GameObject.Find("CreditsText").GetComponent<Text>();
            if (panelCredits != null)
                panelCredits.SetActive(false);
            startG = false;
        }

        if (SceneManager.GetActiveScene().name == "Shop" && startG)
        {
            for (int i = 0; i < lifes.Length; i++)
            {
                lifes[i] = GameObject.Find("HP" + i).GetComponent<Image>();
            }
            aS = null;
            aS = FindObjectOfType<AudioSource>();
            mathPoints = GameObject.Find("Canvas/Info/Stats/MP").GetComponentsInChildren<Image>();
            special = GameObject.Find("Canvas/Info/Stats/SP/Image").GetComponent<Image>();

            status[0] = GameObject.Find("Lives/Images/Level").GetComponent<Image>();
            status[1] = GameObject.Find("Special/Images/Level").GetComponent<Image>();
            status[2] = GameObject.Find("Stage/Images/Level").GetComponent<Image>();

            adsButton[0] = GameObject.Find("Canvas/Double").GetComponent<Button>();
            adsButton[1] = GameObject.Find("Canvas/Magic").GetComponent<Button>();

            for (int i = 0; i < spellNames.Length; i++)
            {
                spells[i] = GameObject.Find(spellNames[i] + "/Ok").GetComponent<Button>();
                spellCosts[i] = GameObject.Find(spellNames[i]).GetComponentsInChildren<Image>();
            }

            for (int i = 0; i < statsCosts.Length; i++)
            {
                stats[i] = GameObject.Find(statsNames[i] + "/Ok").GetComponent<Button>();
                statsCosts[i] = GameObject.Find(statsNames[i] + "/Cost").GetComponentsInChildren<Image>();
            }

            for (int i = 0; i < spells.Length; i++)
            {
                if (i > 0)
                    spells[i].interactable = false;
            }
            for (int i = 0; i < stages.Length; i++)
            {
                stages[i] = GameObject.Find("Stages/" + (i + 1) + "/Image").GetComponent<Image>();
            }
            spellImage = GameObject.Find("Cast").GetComponent<Image>();
            //spellImage.gameObject.SetActive(cast);
            timeT[0] = GameObject.Find("Canvas/Double/Text").GetComponent<Text>();
            timeT[1] = GameObject.Find("Canvas/Magic/Text").GetComponent<Text>();
            panelCredits = GameObject.Find("PanelInfo");
            credits = GameObject.Find("CreditsText").GetComponent<Text>();
            if (panelCredits != null)
                panelCredits.SetActive(false);
            startG = false;
        }

        if (SceneManager.GetActiveScene().name == "Shop" && !startG && operation > 0)
        {
            status[0].sprite = Numbers.stages[statsLevel[0] + 1];
            status[1].sprite = Numbers.stages[statsLevel[1] + 1];
            if (level[operation-1] < 9)
                status[2].sprite = Numbers.stages[level[operation - 1] + 1];
            else
                status[2].sprite = Resources.Load<Sprite>("boss");
            for (int i = 0; i < spellNames.Length; i++)
            {
                int[] a = new int[3];
                a = Score(spellValue[i], a);
                spellCosts[i][1].sprite = Numbers.sprite[a[0]];
                spellCosts[i][2].sprite = Numbers.sprite[a[1]];
                spellCosts[i][3].sprite = Numbers.sprite[a[2]];
            }
            for (int i = 0; i < statsCosts.Length; i++)
            {
                int[] a = new int[3];
                if(i < 2)
                    a = Score((statsValue[i] * (statsLevel[i] + 1)), a);
                else
                    a = Score((statsValue[i] * (level[operation - 1] + 1)), a);
                statsCosts[i][0].sprite = Numbers.sprite[a[0]];
                statsCosts[i][1].sprite = Numbers.sprite[a[1]];
                statsCosts[i][2].sprite = Numbers.sprite[a[2]];
            }

            for (int i = 0; i < spells.Length; i++)
            {
                if (points >= spellValue[i])
                    spells[i].interactable = true;
                else
                    spells[i].interactable = false;
            }
            for (int i = 0; i < stages.Length; i++)
            {
                if (i < stages.Length - 1)
                {
                    if (i < level[operation - 1] + 1)
                        stages[i].sprite = Numbers.stages[i + 1];
                    else
                        stages[i].sprite = Resources.Load<Sprite>("block");
                }
                if (i < level[operation - 1] + 1)
                {
                    stages[i].GetComponentInParent<Button>().interactable = true;
                }
                else
                {
                    stages[i].GetComponentInParent<Button>().interactable = false;
                }
            }
            //if (magic && !cast)
            //    spells[0].interactable = true;
            //else
            //    spells[0].interactable = false;
            if (factor > 1)
            {
                if(timeD/60 == 0 && timeD%60 == 0)
                    timeT[0].text = 30 + ":" + 00.ToString("00");
                else
                    timeT[0].text = (29 - Mathf.FloorToInt(timeD / 60)).ToString("00") + ":" + (60 - Mathf.CeilToInt(timeD % 60)).ToString("00");
                adsButton[0].interactable = false;
            }
            else
            {
                timeT[0].text = "";
                adsButton[0].interactable = true;
            }
            if (magic)
            {
                if (timeM / 60 == 0 && timeM % 60 == 0)
                    timeT[1].text = 30 + ":" + 00.ToString("00");
                else
                    timeT[1].text = (29 - Mathf.FloorToInt(timeM / 60)).ToString("00") + ":" + (60 - Mathf.CeilToInt(timeM % 60)).ToString("00");
                adsButton[1].interactable = false;
            }
            else
            {
                timeT[1].text = "";
                adsButton[1].interactable = true;
            }

            for (int i = 0; i < stats.Length; i++)
            {
                if (i < 2)
                {
                    if (points >= statsValue[i] * (statsLevel[i] + 1) && statsLevel[i] < statsLimits[i])
                        stats[i].interactable = true;
                    else
                        stats[i].interactable = false;
                }
                else
                {
                    if (points >= statsValue[i] * (level[operation - 1] + 1) && level[operation - 1] < statsLimits[2])
                        stats[i].interactable = true;
                    else
                        stats[i].interactable = false;
                }
            }
        }
        if(SceneManager.GetActiveScene().name == "Game" && !startG)
        {
            if (life == 0 && aS.clip == audios.audios[0])
            {
                death = true;
                retry.SetActive(true);
                aS.clip = audios.audios[1];
                aS.loop = false;
                aS.Play();
            }
            if (focus == 5)
            {
                sp++;
                focus = 0;
                aS.PlayOneShot(audios.sounds[6]);
                recover.SetActive(true);
            }

            if (cure == 10)
            {
                life++;
                cure = 0;
                aS.PlayOneShot(audios.sounds[5]);
                recover.SetActive(true);
            }
            if (aS != null)
            {
                if (aS.clip == audios.audios[1] && !aS.isPlaying)
                {

                    retry.transform.Find("PanelRetry").gameObject.SetActive(true);
                    aS.clip = audios.audios[2];
                    aS.loop = true;
                    aS.PlayDelayed(1);
                }
            }
        }
        //if (spellImage != null)
        //    spellImage.gameObject.SetActive(cast);
        sp = Mathf.Clamp(sp, 0, statsLevel[1] + 1);
        if(operation > 0)
            level[operation - 1] = Mathf.Clamp(level[operation - 1], 0, statsLimits[2]);
        statsLevel[1] = Mathf.Clamp(statsLevel[1], 0, statsLimits[1]);
        statsLevel[0] = Mathf.Clamp(statsLevel[0], 0, statsLimits[0]);
        life = Mathf.Clamp(life, 0, statsLevel[0] + 1);

        if(special != null)
            special.fillAmount = 0.1f * (statsLevel[1] + 1);

        timeD = (factor > 1) ? timeD += Time.deltaTime : timeD = 0;
        timeM = (magic) ? timeM += Time.deltaTime : timeM = 0;

        if (timeD > 1800)
            factor = 1;
        if (timeM > 1800)
        {
            magic = false;
            cast = false;
        }


        if (SceneManager.GetActiveScene().name == "Game" || SceneManager.GetActiveScene().name == "Shop")
        {
            operation = 1;
            if (lifes != null)
            {
                for (int i = 0; i < lifes.Length; i++)
                {
                    if (i > statsLevel[0])
                    {
                        lifes[i].enabled = false;
                    }
                    else
                    {
                        lifes[i].enabled = true;
                    }
                }
                for (int i = 0; i <= statsLevel[0]; i++)
                {
                    if (i < life)
                    {
                        lifes[i].sprite = Numbers.heart[2];
                    }
                    else
                    {
                        lifes[i].sprite = Numbers.heart[3];
                    }
                }
            }
            mathValue = Score(points, mathValue);
            mathPoints[0].sprite = Numbers.sprite[mathValue[0]];
            mathPoints[1].sprite = Numbers.sprite[mathValue[1]];
            mathPoints[2].sprite = Numbers.sprite[mathValue[2]];

            special.sprite = Numbers.special[sp];
            spellImage = GameObject.Find("Cast").GetComponent<Image>();
            spellImage.sprite = Numbers.spell[spell];

        }
    }

    private int[] Score (int points, int[] a)
    {
        int x, y, z;
        if(points < 10)
        {
            z = points;
            y = 0;
            x = 0;
        }
        else if (points < 100)
        {
            z = points%10;
            points /= 10;
            y = points;
            x = 0;
        }
        else
        {
            z = points % 10;
            points /= 10;
            y = points % 10;
            points /= 10;
            x = points;
        }
        a[0] = x;
        a[1] = y;
        a[2] = z;
        return a;
    }

    //private void OnLevelWasLoaded(int level)
    //{
    //    //lifes = new Image[5];
    //    special = null;
    //    aS = null;
    //    Time.timeScale = 1;
    //    startG = true;
    //}

    private void TextCall(string a)
    {
        for (int i = 0; i < FontCall.title.childCount; i++)
        {
            Destroy(FontCall.title.GetChild(i).gameObject);
        }
        for (int i = 0; i < a.Length; i++)
        {
            int x = a[i];
            if (a[i] == ' ')
                x = 26;
            else if (a[i] == '-')
                x = 44;
            else
                x -= 65;
            FontCall.letter.GetComponent<Image>().sprite = Numbers.letters[x];
            Instantiate(FontCall.letter, FontCall.title);
        }
    }
    private void OnApplicationQuit()
    {
        GameDatas.Save();
    }
}
