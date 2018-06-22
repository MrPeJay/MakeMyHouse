using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public bool PauseMenuEnabled = false;
    public bool MAINCANVAS = false;
    public bool RESTARTCANVAS = false;
    public bool EXITCANVAS = false;
    public bool INFOCANVAS = false;

    public GameObject PauseMenuPanel;
    public GameObject RestartConfirmPanel;
    public GameObject ExitConfirmPanel;
    public GameObject InfoPanel;

    private Animator PauseAnim;
    private Animator RestartAnim;
    private Animator ExitAnim;
    private Animator InfoAnim;

    private ObjectSelect ObjectSelectScript;

    private GameObject Decision;

    public GameObject ClickSound;

    private void Start()
    {
        ObjectSelectScript = gameObject.GetComponent<ObjectSelect>();

        Decision = GameObject.FindGameObjectWithTag("Decision");

        PauseAnim = PauseMenuPanel.GetComponent<Animator>();
        RestartAnim = RestartConfirmPanel.GetComponent<Animator>();
        ExitAnim = ExitConfirmPanel.GetComponent<Animator>();
        InfoAnim = InfoPanel.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetButtonDown("Cancel"))
        {
            if (PauseMenuEnabled)
            {
                if (MAINCANVAS)
                {
                    Resume();
                }
            }
            else
            {
                Pause();
            }
        }

        if (PauseMenuEnabled)
        {
            if (MAINCANVAS)
            {
                RESTARTCANVAS = false;
                EXITCANVAS = false;
                INFOCANVAS = false;
                PauseAnim.enabled = true;
                PauseAnim.Play("SLIDEIN");
            }
            else
            {
                PauseAnim.Play("SLIDEOUT");
            }

            if (EXITCANVAS)
            {
                ExitAnim.enabled = true;
                ExitAnim.Play("SLIDEIN");
                if (Input.GetButtonDown("Cancel"))
                {
                    MAINCANVAS = true;
                    ExitAnim.Play("SLIDEOUT");
                }
            }

            if (RESTARTCANVAS)
            {
                RestartAnim.enabled = true;
                RestartAnim.Play("SLIDEIN");
                if (Input.GetButtonDown("Cancel"))
                {
                    MAINCANVAS = true;
                    RestartAnim.Play("SLIDEOUT");
                }
            }

            if (INFOCANVAS)
            {
                InfoAnim.enabled = true;
                InfoAnim.Play("SLIDEIN");
                if (Input.GetButtonDown("Cancel"))
                {
                    MAINCANVAS = true;
                    InfoAnim.Play("SLIDEOUT");
                }
            }
        }
	}

    private void Pause()
    {
        Time.timeScale = 0f;
        PauseMenuEnabled = true;
        MAINCANVAS = true;
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        PauseAnim.Play("SLIDEOUT");

        MAINCANVAS = false;
        PauseMenuEnabled = false;
        SpawnSound(ClickSound);
    }

    public void Restart()
    {
        MAINCANVAS = false;
        RESTARTCANVAS = true;
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

    public void ExitNo()
    {
        MAINCANVAS = true;
        ExitAnim.Play("SLIDEOUT");
        SpawnSound(ClickSound);
    }

    public void ExitYes()
    {
        Time.timeScale = 1f;
        MAINCANVAS = false;
        Destroy(Decision);
        SceneManager.LoadScene("Menu");
        SpawnSound(ClickSound);
    }

    public void RestartNo()
    {
        Time.timeScale = 1f;
        MAINCANVAS = true;
        RestartAnim.Play("SLIDEOUT");
        SpawnSound(ClickSound);
    }

    public void RestartYes()
    {
        Time.timeScale = 1f;
        MAINCANVAS = false;
        RestartAnim.Play("SLIDEOUT");
        SceneManager.LoadScene("BUILDING SCENE");
        SpawnSound(ClickSound);
    }

    private void SpawnSound(GameObject sound)
    {
        Instantiate(sound);
    }
}
