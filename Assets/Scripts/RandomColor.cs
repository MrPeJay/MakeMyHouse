using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColor : MonoBehaviour {

    public Color randomColor;

    private Renderer[] Renderers;

	// Use this for initialization
	void Start () {
        randomColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.childCount > 0)
        {
            Renderers = gameObject.GetComponentsInChildren<Renderer>();

            foreach (Renderer renderer in Renderers)
            {
                renderer.material.color = randomColor;
            }
        }
	}
}
