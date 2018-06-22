using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour {

    public Material[] Materials;

    private Grid grid;

    private MeshRenderer[] Renderers;

    public bool IsCollided = false;

    private PauseMenu PauseMenuScript;
    private ObjectSelect ObjectSelectScript;

    public Quaternion ObjectRotation;

    public GameObject RotateSound;

    private void Start()
    {
        Renderers = gameObject.GetComponentsInChildren<MeshRenderer>();

        PauseMenuScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PauseMenu>();
        ObjectSelectScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ObjectSelect>();
    }

    void Awake()
    {
        grid = FindObjectOfType<Grid>();
    }

    void Update () {
        RaycastHit hitInfo;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (!PauseMenuScript.PauseMenuEnabled)
        {
            if (Physics.Raycast(ray, out hitInfo))
            {
                if (ObjectSelectScript.selectedGround)
                {
                    if (hitInfo.transform.tag == "Grid")
                    {
                        PlaceObjectNear(hitInfo.point);
                        if (IsCollided)
                        {
                            foreach (MeshRenderer renderer in Renderers)
                            {
                                renderer.material = Materials[1];
                            }
                        }
                        else
                        {
                            foreach (MeshRenderer renderer in Renderers)
                            {
                                renderer.material = Materials[0];
                            }
                        }
                    }
                    else
                    {
                        transform.position = hitInfo.point;
                        foreach (MeshRenderer renderer in Renderers)
                        {
                            renderer.material = Materials[1];
                        }
                    }
                }
                else if (ObjectSelectScript.selectedWall)
                {
                    if (Input.GetButtonDown("Rotate"))
                    {
                        transform.Rotate(0,90,0);
                        ObjectRotation = transform.rotation;
                        SpawnSound(RotateSound);
                    }

                    if (hitInfo.transform.tag == "Grid" || hitInfo.transform.tag == "Ground")
                    {
                        PlaceObjectNear(hitInfo.point);
                        if (IsCollided)
                        {
                            foreach (MeshRenderer renderer in Renderers)
                            {
                                renderer.material = Materials[1];
                            }
                        }
                        else
                        {
                            foreach (MeshRenderer renderer in Renderers)
                            {
                                renderer.material = Materials[0];
                            }
                        }
                    }
                    else
                    {
                        transform.position = hitInfo.point;
                        foreach (MeshRenderer renderer in Renderers)
                        {
                            renderer.material = Materials[1];
                        }
                    }
                }
                else if (ObjectSelectScript.selectedEnvironment)
                {
                    if (Input.GetButtonDown("Rotate"))
                    {
                        transform.Rotate(0, 90, 0);
                        ObjectRotation = transform.rotation;
                        SpawnSound(RotateSound);
                    }

                    if (hitInfo.transform.tag == "Grid")
                    {
                        PlaceObjectNear(hitInfo.point);
                        if (IsCollided)
                        {
                            foreach (MeshRenderer renderer in Renderers)
                            {
                                renderer.material = Materials[1];
                            }
                        }
                        else
                        {
                            foreach (MeshRenderer renderer in Renderers)
                            {
                                renderer.material = Materials[0];
                            }
                        }
                    }
                    else
                    {
                        transform.position = hitInfo.point;
                        foreach (MeshRenderer renderer in Renderers)
                        {
                            renderer.material = Materials[1];
                        }
                    }
                }

                else if (ObjectSelectScript.selectedFurniture)
                {
                    if (Input.GetButtonDown("Rotate"))
                    {
                        transform.Rotate(0, 90, 0);
                        ObjectRotation = transform.rotation;
                        SpawnSound(RotateSound);
                    }

                    if (hitInfo.transform.tag == "Grid" || hitInfo.transform.tag == "Ground")
                    {
                        PlaceObjectNear(hitInfo.point);
                        if (IsCollided)
                        {
                            foreach (MeshRenderer renderer in Renderers)
                            {
                                renderer.material = Materials[1];
                            }
                        }
                        else
                        {
                            foreach (MeshRenderer renderer in Renderers)
                            {
                                renderer.material = Materials[0];
                            }
                        }
                    }
                    else
                    {
                        transform.position = hitInfo.point;
                        foreach (MeshRenderer renderer in Renderers)
                        {
                            renderer.material = Materials[1];
                        }
                    }
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag != "Grid")
        {
            IsCollided = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IsCollided = false;
    }

    private void PlaceObjectNear(Vector3 clickPoint)
    {
        var finalPosition = grid.GetNearestPointOnGrid(clickPoint);
        transform.position = finalPosition;
    }

    private void SpawnSound(GameObject sound)
    {
        Instantiate(sound);
    }
}
