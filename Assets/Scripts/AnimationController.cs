using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour {

    public GameObject Car;

    private Animator CarAnim;

    public float timer;

	// Use this for initialization
	void Start () {
        CarAnim = Car.GetComponent<Animator>();

        timer = Random.Range(20,90);
	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            CarAnim.enabled = true;
            CarAnim.Play("DRIVE", -1, 0);
            ResetTimer();
        }
	}

    private void ResetTimer()
    {
        timer = Random.Range(60,180);
    }
}
