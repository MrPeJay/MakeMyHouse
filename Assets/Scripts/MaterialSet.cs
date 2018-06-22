using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialSet : MonoBehaviour {

    private ObjectSelect ObjectSelectScript;
    private PauseMenu PauseMenuScript;
    private RandomColor RandomColorScript;

    private MeshRenderer[] Renderers;

	// Use this for initialization
	void Start () {
        ObjectSelectScript = gameObject.GetComponent<ObjectSelect>();
        PauseMenuScript = gameObject.GetComponent<PauseMenu>();
	}

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenuScript.PauseMenuEnabled)
        {
            if (ObjectSelectScript.selectedColor && ObjectSelectScript.Selected)
            {
                if (Input.GetButtonDown("Select"))
                {
                    RaycastHit hitInfo;
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                    if (Physics.Raycast(ray, out hitInfo))
                    {
                        if (hitInfo.transform.tag == "Ground" || hitInfo.transform.tag == "Wall" || hitInfo.transform.tag == "Furniture"
                            || hitInfo.transform.tag == "Fence")
                        {
                            if (hitInfo.transform.tag == "Furniture" || hitInfo.transform.tag == "Fence")
                            {
                                Renderers = hitInfo.transform.GetComponentsInChildren<MeshRenderer>();
                            }
                            else
                            {
                                Renderers = hitInfo.transform.parent.GetComponentsInChildren<MeshRenderer>();
                            }

                            if (hitInfo.transform.tag == "Wall" || hitInfo.transform.tag == "Ground")
                            {
                                RandomColorScript = hitInfo.transform.parent.GetComponent<RandomColor>();
                                RandomColorScript.randomColor = ObjectSelectScript.selectedMaterial.color;
                            }

                            foreach (MeshRenderer renderer in Renderers)
                            {
                                renderer.material = ObjectSelectScript.selectedMaterial;
                            }
                        }
                    }
                }
            }
        }
    }
}
