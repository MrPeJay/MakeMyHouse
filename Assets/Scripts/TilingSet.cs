using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilingSet : MonoBehaviour {

    private Grid GridScript;

    private Renderer ObjectMaterial;

	// Use this for initialization
	void Start () {
        GridScript = gameObject.GetComponentInParent<Grid>();
        ObjectMaterial = gameObject.GetComponent<Renderer>();
        Invoke("Set", 0.1f);
	}

    void Set()
    {
        ObjectMaterial.material.SetTextureScale("_MainTex",new Vector2(GridScript.xSize+1,GridScript.zSize+1));
        transform.position -= new Vector3(.5f,0,.5f);
    }
}
