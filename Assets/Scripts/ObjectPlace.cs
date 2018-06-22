using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlace : MonoBehaviour {

    private Grid grid;

    private ObjectSelect ObjectSelectScript;
    private PauseMenu PauseMenuScript;
    private DragObject DragObjectScript;

    public Transform parentFloor;
    public Transform parentWall;

    public GameObject PlaceSound;
    public GameObject DenySound;
    public GameObject DeleteSound;

    public void Start()
    {
        ObjectSelectScript = gameObject.GetComponent<ObjectSelect>();
        PauseMenuScript = gameObject.GetComponent<PauseMenu>();
    }

    // Use this for initialization
    void Awake () {
        grid = FindObjectOfType<Grid>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!PauseMenuScript.PauseMenuEnabled)
        {
            if (ObjectSelectScript.Edit)
            {
                if (ObjectSelectScript.Selected)
                {
                    if (Input.GetButtonDown("Select"))
                    {
                        RaycastHit hitInfo;
                        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                        if (Physics.Raycast(ray, out hitInfo))
                        {
                            if (ObjectSelectScript.selectedWall)
                            {
                                if (hitInfo.transform.tag == "Grid" || hitInfo.transform.tag == "Ground")
                                {
                                    if (!GameObject.FindGameObjectWithTag("Draggable").GetComponent<DragObject>().IsCollided)
                                    {
                                        PlaceWallNear(hitInfo.point);
                                    }
                                    else
                                    {
                                        SpawnSound(DenySound);
                                    }
                                }
                                else
                                {
                                    SpawnSound(DenySound);
                                }
                            }
                            else if (ObjectSelectScript.selectedGround)
                            {
                                if (hitInfo.transform.tag == "Grid")
                                {
                                    if (!GameObject.FindGameObjectWithTag("Draggable").GetComponent<DragObject>().IsCollided)
                                    {
                                        PlaceGroundNear(hitInfo.point);
                                    }
                                    else
                                    {
                                        SpawnSound(DenySound);
                                    }
                                }
                                else
                                {
                                    SpawnSound(DenySound);
                                }
                            }
                            else if (ObjectSelectScript.selectedEnvironment)
                            {
                                if (hitInfo.transform.tag == "Grid")
                                {
                                    if (!GameObject.FindGameObjectWithTag("Draggable").GetComponent<DragObject>().IsCollided)
                                    {
                                        PlaceObjectNear(hitInfo.point);
                                    }
                                    else
                                    {
                                        SpawnSound(DenySound);
                                    }
                                }
                                else
                                {
                                    SpawnSound(DenySound);
                                }
                            }
                            else if (ObjectSelectScript.selectedFurniture)
                            {
                                if (hitInfo.transform.tag == "Grid" || hitInfo.transform.tag == "Ground")
                                {
                                    if (!GameObject.FindGameObjectWithTag("Draggable").GetComponent<DragObject>().IsCollided)
                                    {
                                        PlaceObjectNear(hitInfo.point);
                                    }
                                    else
                                    {
                                        SpawnSound(DenySound);
                                    }
                                }
                                else
                                {
                                    SpawnSound(DenySound);
                                }
                            }
                        }
                    }
                }

                if (Input.GetButtonDown("Destroy"))
                {
                    RaycastHit hitInfo;
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                    if (Physics.Raycast(ray, out hitInfo))
                    {
                        if (hitInfo.transform.tag != "Land" && hitInfo.transform.tag != "Grid")
                        {
                            Destroy(hitInfo.transform.gameObject);
                            SpawnSound(DeleteSound);
                        }
                    }
                }
            }
        }

        parentFloor = GameObject.Find("roomFloor" + ObjectSelectScript.roomNr.ToString()).transform;
        parentWall = GameObject.Find("roomWalls" + ObjectSelectScript.roomNr.ToString()).transform;
    }

    private void PlaceGroundNear(Vector3 clickPoint)
    {
        DragObjectScript = GameObject.FindGameObjectWithTag("Draggable").GetComponent<DragObject>();
        var finalPosition = grid.GetNearestPointOnGrid(clickPoint);
        Instantiate(ObjectSelectScript.SelectedObject,finalPosition,DragObjectScript.ObjectRotation, parentFloor);
        SpawnSound(PlaceSound);
    }

    private void PlaceWallNear(Vector3 clickPoint)
    {
        DragObjectScript = GameObject.FindGameObjectWithTag("Draggable").GetComponent<DragObject>();
        var finalPosition = grid.GetNearestPointOnGrid(clickPoint);
        Instantiate(ObjectSelectScript.SelectedObject, finalPosition, DragObjectScript.ObjectRotation, parentWall);
        SpawnSound(PlaceSound);
    }

    private void PlaceObjectNear(Vector3 clickPoint)
    {
        DragObjectScript = GameObject.FindGameObjectWithTag("Draggable").GetComponent<DragObject>();
        var finalPosition = grid.GetNearestPointOnGrid(clickPoint);
        Instantiate(ObjectSelectScript.SelectedObject, finalPosition, DragObjectScript.ObjectRotation);
        SpawnSound(PlaceSound);
    }

    private void SpawnSound(GameObject sound)
    {
        Instantiate(sound);
    }
}
