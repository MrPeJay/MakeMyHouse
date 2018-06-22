using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    private Decision DecisionScript;

    public Text LNum;
    public Text WNum;
    public Text Space;

    public Slider Length;
    public Slider Width;

    private Animator MainAnim;
    private Animator InfoAnim;
    private Animator ExitAnim;
    private Animator SelectAnim;

    public GameObject MainPanel;
    public GameObject InfoPanel;
    public GameObject ExitPanel;
    public GameObject SelectPanel;

    private bool MAINCANVAS = true;
    private bool INFOCANVAS = false;
    private bool EXITCANVAS = false;
    private bool SELECTCANVAS = false;

    public GameObject ClickSound;


    // Use this for initialization
    void Start () {
        DecisionScript = GameObject.FindGameObjectWithTag("Decision").GetComponent<Decision>();

        MainAnim = MainPanel.GetComponent<Animator>();
        InfoAnim = InfoPanel.GetComponent<Animator>();
        ExitAnim = ExitPanel.GetComponent<Animator>();
        SelectAnim = SelectPanel.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if (MAINCANVAS)
        {
            INFOCANVAS = false;
            EXITCANVAS = false;
            SELECTCANVAS = false;
            MainAnim.enabled = true;
            MainAnim.Play("SLIDEIN");
        }
        else
        {
            MainAnim.Play("SLIDEOUT");
        }

        if (INFOCANVAS)
        {
            MAINCANVAS = false;
            EXITCANVAS = false;
            SELECTCANVAS = false;
            InfoAnim.enabled = true;
            InfoAnim.Play("SLIDEIN");

            if (Input.GetButtonDown("Cancel"))
            {
                MAINCANVAS = true;
                InfoAnim.Play("SLIDEOUT");
            }
        }

        if (EXITCANVAS)
        {
            MAINCANVAS = false;
            INFOCANVAS = false;
            SELECTCANVAS = false;
            ExitAnim.enabled = true;
            ExitAnim.Play("SLIDEIN");

            if (Input.GetButtonDown("Cancel"))
            {
                MAINCANVAS = true;
                ExitAnim.Play("SLIDEOUT");
            }
        }

        if (SELECTCANVAS)
        {
            MAINCANVAS = false;
            INFOCANVAS = false;
            EXITCANVAS = false;
            SelectAnim.enabled = true;
            SelectAnim.Play("SLIDEIN");

            if (Input.GetButtonDown("Cancel"))
            {
                MAINCANVAS = true;
                SelectAnim.Play("SLIDEOUT");
            }
        }

        LNum.text = Mathf.RoundToInt(Length.value).ToString() + "m";
        WNum.text = Mathf.RoundToInt(Width.value).ToString() + "m";
        Space.text = Mathf.RoundToInt(Length.value * Width.value).ToString() + "m2";
	}

    public void Play()
    {
        MAINCANVAS = false;
        SELECTCANVAS = true;
        SpawnSound(ClickSound);
    }

    public void PlayBack()
    {
        MAINCANVAS = true;
        SelectAnim.Play("SLIDEOUT");
        SpawnSound(ClickSound);
    }

    public void BuildIt()
    {
        DecisionScript.Length = Mathf.RoundToInt(Length.value);
        DecisionScript.Width = Mathf.RoundToInt(Width.value);
        SceneManager.LoadScene("BUILDING SCENE");
        SpawnSound(ClickSound);
    }

    public void Info()
    {
        MAINCANVAS = false;
        INFOCANVAS = true;
        SpawnSound(ClickSound);
    }

    public void InfoBack()
    {
        MAINCANVAS = true;
        InfoAnim.Play("SLIDEOUT");
        SpawnSound(ClickSound);
    }

    public void Exit()
    {
        MAINCANVAS = false;
        EXITCANVAS = true;
        SpawnSound(ClickSound);
    }

    public void ExitYes()
    {
        Application.Quit();
        SpawnSound(ClickSound);
    }

    public void ExitNo()
    {
        MAINCANVAS = true;
        ExitAnim.Play("SLIDEOUT");
        SpawnSound(ClickSound);
    }

    private void SpawnSound(GameObject sound)
    {
        Instantiate(sound);
    }
}
