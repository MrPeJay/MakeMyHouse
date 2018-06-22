using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectSelect : MonoBehaviour {

    public GameObject SelectedObject;

    public GameObject[] GroundObjects;
    public GameObject[] WallObjects;
    public GameObject[] FurnitureObjects;
    public GameObject[] EnvironmentObjects;

    public GameObject GroundDragObject;
    public GameObject WallDragObject;
    public GameObject[] FurnitureDragObjects;
    public GameObject[] EnvironmentDragObjects;

    public bool Selected = false;

    public bool Edit = true;

    public Image EyeImage;
    public Image BucketImage;

    public Text RoomNumber;

    public GameObject ViewModePanel;
    public GameObject EditMenuPanel;
    public GameObject RoomPanel;
    public GameObject EnvPanel;
    public GameObject FurnPanel;
    public GameObject ColorPanel;

    private GameObject LastSelection;

    private int SelectionMode = 0;

    private Animator ViewModeAnim;
    private Animator EditMenuAnim;
    private Animator RoomAnim;
    private Animator EnvAnim;
    private Animator FurnAnim;
    private Animator ColorAnim;

    private PauseMenu PauseMenuScript;

    public bool selectedWall = false;
    public bool selectedGround = false;
    public bool selectedEnvironment = false;
    public bool selectedFurniture = false;
    public bool selectedColor = false;

    public int roomNr = 1;
    GameObject roomFloor;
    GameObject roomWalls;

    public Material selectedMaterial;
    public Material[] Materials;

    private Transform SpawnPoint;

    public GameObject ClickSound;


    private void Start()
    {
        PauseMenuScript = gameObject.GetComponent<PauseMenu>();
        ViewModeAnim = ViewModePanel.GetComponent<Animator>();
        EditMenuAnim = EditMenuPanel.GetComponent<Animator>();
        RoomAnim = RoomPanel.GetComponent<Animator>();
        EnvAnim = EnvPanel.GetComponent<Animator>();
        FurnAnim = FurnPanel.GetComponent<Animator>();
        ColorAnim = ColorPanel.GetComponent<Animator>();

        SpawnPoint = GameObject.FindGameObjectWithTag("SPAWN").transform;

        SelectionMode = 5;

        ViewModeAnim.enabled = true;
        EditMenuAnim.enabled = true;
        RoomAnim.enabled = true;
        EnvAnim.enabled = true;
        FurnAnim.enabled = true;
        ColorAnim.enabled = true;

        roomWalls = new GameObject("roomWalls" + roomNr.ToString());
        roomWalls.AddComponent<RandomColor>();

        roomFloor = new GameObject("roomFloor" + roomNr.ToString());
        roomFloor.AddComponent<RandomColor>();
    }

    private void Update()
    {
        if (!PauseMenuScript.PauseMenuEnabled)
        {
            if (Input.GetButtonDown("Switch"))
            {
                if (Edit == true)
                {
                    Edit = false;
                    ViewModeAnim.Play("SLIDEOUT");
                    EnvAnim.Play("SLIDEOUT");
                    FurnAnim.Play("SLIDEOUT");
                    ColorAnim.Play("SLIDEOUT");
                    selectedEnvironment = false;
                    selectedFurniture = false;
                }
                else
                {
                    Edit = true;
                    ViewModeAnim.Play("SLIDEIN");
                    EditMenuAnim.Play("SLIDEIN");
                }
            }
        }

        if (Edit)
        {
            BucketImage.enabled = true;
            EyeImage.enabled = false;
            if (!selectedEnvironment && !selectedFurniture && !selectedColor)
            {
                EditMenuAnim.Play("SLIDEIN");
            }
        }
        else
        {
            BucketImage.enabled = false;
            EyeImage.enabled = true;
            EditMenuAnim.Play("SLIDEOUT");
            RoomAnim.Play("SLIDEOUT");
            Selected = false;
            DestroyDraggables();
        }

        if (Input.GetButtonDown("CancelSelection"))
        {
            if (Edit)
            {
                if (Selected)
                {
                    Selected = false;
                    selectedGround = false;
                    selectedWall = false;
                    DestroyDraggables();
                }
                else
                {
                    if (SelectionMode != 4 && SelectionMode != 5)
                    {
                        Instantiate(LastSelection);
                        if (SelectionMode == 0)
                        {
                            selectedGround = true;
                        }
                        else if (SelectionMode == 1)
                        {
                            selectedWall = true;
                        }
                        else if (SelectionMode == 2)
                        {
                            selectedFurniture = true;
                        }
                        else if (SelectionMode == 3)
                        {
                            selectedEnvironment = true;
                        }
                        Selected = true;
                    }
                }
            }
        }

        RoomNumber.text = roomNr.ToString();
    }

    public void SelectedGround()
    {
        Selected = true;
        SelectedObject = GroundObjects[0];

        if (!selectedWall)
        {
            RoomAnim.Play("SLIDEIN");
        }

        selectedGround = true;
        selectedWall = false;
        selectedEnvironment = false;
        selectedFurniture = false;
        selectedColor = false;

        DestroyDraggables();
        Instantiate(GroundDragObject,SpawnPoint);
        LastSelection = GroundDragObject;
        SelectionMode = 0;
        SpawnSound(ClickSound);
    }

    public void SelectedWall()
    {
        Selected = true;
        SelectedObject = WallObjects[0];

        if (!selectedGround)
        {
            RoomAnim.Play("SLIDEIN");
        }

        selectedGround = false;
        selectedWall = true;
        selectedEnvironment = false;
        selectedFurniture = false;
        selectedColor = false;

        DestroyDraggables();
        Instantiate(WallDragObject, SpawnPoint);
        LastSelection = WallDragObject;
        SelectionMode = 1;
        SpawnSound(ClickSound);
    }

    public void SelectedEnvironment()
    {
        EditMenuAnim.Play("SLIDEOUT");
        RoomAnim.Play("SLIDEOUT");

        EnvAnim.Play("SLIDEIN");

        selectedGround = false;
        selectedWall = false;
        selectedEnvironment = true;
        selectedFurniture = false;
        selectedColor = false;

        DestroyDraggables();
        SpawnSound(ClickSound);
    }

    public void SelectedFurniture()
    {
        EditMenuAnim.Play("SLIDEOUT");
        RoomAnim.Play("SLIDEOUT");

        FurnAnim.Play("SLIDEIN");

        selectedGround = false;
        selectedWall = false;
        selectedEnvironment = false;
        selectedFurniture = true;
        selectedColor = false;

        DestroyDraggables();
        SpawnSound(ClickSound);
    }

    public void SelectedColor()
    {
        EditMenuAnim.Play("SLIDEOUT");
        RoomAnim.Play("SLIDEOUT");

        ColorAnim.Play("SLIDEIN");

        selectedGround = false;
        selectedWall = false;
        selectedEnvironment = false;
        selectedFurniture = false;
        selectedColor = true;

        DestroyDraggables();
        SpawnSound(ClickSound);
    }

    public void ColorSelectLightBrown()
    {
        Selected = true;

        selectedMaterial = Materials[1]; 
        DestroyDraggables();
        SelectionMode = 4;
        selectedEnvironment = false;
        SpawnSound(ClickSound);
    }

    public void ColorSelectBrown()
    {
        Selected = true;

        selectedMaterial = Materials[0];
        DestroyDraggables();
        SelectionMode = 4;
        selectedEnvironment = false;
        SpawnSound(ClickSound);
    }

    public void ColorSelectBlack()
    {
        Selected = true;

        selectedMaterial = Materials[2];
        DestroyDraggables();
        SelectionMode = 4;
        selectedEnvironment = false;
        SpawnSound(ClickSound);
    }

    public void ColorSelectWhite()
    {
        Selected = true;

        selectedMaterial = Materials[3];
        DestroyDraggables();
        SelectionMode = 4;
        selectedEnvironment = false;
        SpawnSound(ClickSound);
    }

    public void ColorSelectBlue()
    {
        Selected = true;

        selectedMaterial = Materials[4];
        DestroyDraggables();
        SelectionMode = 4;
        selectedEnvironment = false;
        SpawnSound(ClickSound);
    }

    public void ColorSelectRed()
    {
        Selected = true;

        selectedMaterial = Materials[5];
        DestroyDraggables();
        SelectionMode = 4;
        selectedEnvironment = false;
        SpawnSound(ClickSound);
    }

    public void ColorBack()
    {
        ColorAnim.Play("SLIDEOUT");

        EditMenuAnim.Play("SLIDEIN");

        selectedColor = false;
        SpawnSound(ClickSound);
    }

    public void FurnitureBack()
    {
        FurnAnim.Play("SLIDEOUT");

        EditMenuAnim.Play("SLIDEIN");

        selectedFurniture = false;
        SpawnSound(ClickSound);
    }

    public void EnvironmentBack()
    {
        EnvAnim.Play("SLIDEOUT");

        EditMenuAnim.Play("SLIDEIN");

        selectedEnvironment = false;
        SpawnSound(ClickSound);
    }

    public void FurnitureSelectBed()
    {
        Selected = true;

        SelectedObject = FurnitureObjects[0];
        DestroyDraggables();
        Instantiate(FurnitureDragObjects[0], SpawnPoint);
        LastSelection = FurnitureDragObjects[0];
        SelectionMode = 2;
        selectedEnvironment = false;
        SpawnSound(ClickSound);
    }

    public void FurnitureSelectTable()
    {
        Selected = true;

        SelectedObject = FurnitureObjects[1];
        DestroyDraggables();
        Instantiate(FurnitureDragObjects[1], SpawnPoint);
        LastSelection = FurnitureDragObjects[1];
        SelectionMode = 2;
        selectedEnvironment = false;
        SpawnSound(ClickSound);
    }

    public void FurnitureSelectCabinet()
    {
        Selected = true;

        SelectedObject = FurnitureObjects[2];
        DestroyDraggables();
        Instantiate(FurnitureDragObjects[2], SpawnPoint);
        LastSelection = FurnitureDragObjects[2];
        SelectionMode = 2;
        selectedEnvironment = false;
        SpawnSound(ClickSound);
    }

    public void FurnitureSelectSofa()
    {
        Selected = true;

        SelectedObject = FurnitureObjects[3];
        DestroyDraggables();
        Instantiate(FurnitureDragObjects[3], SpawnPoint);
        LastSelection = FurnitureDragObjects[3];
        SelectionMode = 2;
        selectedEnvironment = false;
        SpawnSound(ClickSound);
    }

    public void FurnitureSelectLamp()
    {
        Selected = true;

        SelectedObject = FurnitureObjects[4];
        DestroyDraggables();
        Instantiate(FurnitureDragObjects[4], SpawnPoint);
        LastSelection = FurnitureDragObjects[4];
        SelectionMode = 2;
        selectedEnvironment = false;
        SpawnSound(ClickSound);
    }

    public void FurnitureSelectVase()
    {
        Selected = true;

        SelectedObject = FurnitureObjects[5];
        DestroyDraggables();
        Instantiate(FurnitureDragObjects[5], SpawnPoint);
        LastSelection = FurnitureDragObjects[5];
        SelectionMode = 2;
        selectedEnvironment = false;
        SpawnSound(ClickSound);
    }

    public void EnvironmentSelectFence()
    {
        Selected = true;

        SelectedObject = EnvironmentObjects[0];
        DestroyDraggables();
        Instantiate(EnvironmentDragObjects[0], SpawnPoint);
        LastSelection = EnvironmentDragObjects[0];
        SelectionMode = 3;
        selectedFurniture = false;
        SpawnSound(ClickSound);
    }

    public void EnvironmentSelectTree1()
    {
        Selected = true;

        SelectedObject = EnvironmentObjects[2];
        DestroyDraggables();
        Instantiate(EnvironmentDragObjects[2], SpawnPoint);
        LastSelection = EnvironmentDragObjects[2];
        SelectionMode = 3;
        selectedFurniture = false;
        SpawnSound(ClickSound);
    }

    public void EnvironmentSelectTree2()
    {
        Selected = true;

        SelectedObject = EnvironmentObjects[3];
        DestroyDraggables();
        Instantiate(EnvironmentDragObjects[3], SpawnPoint);
        LastSelection = EnvironmentDragObjects[3];
        SelectionMode = 3;
        selectedFurniture = false;
        SpawnSound(ClickSound);
    }

    public void EnvironmentSelectRock()
    {
        Selected = true;

        SelectedObject = EnvironmentObjects[1];
        DestroyDraggables();
        Instantiate(EnvironmentDragObjects[1], SpawnPoint);
        LastSelection = EnvironmentDragObjects[1];
        SelectionMode = 3;
        selectedFurniture = false;
        SpawnSound(ClickSound);
    }

    public void DestroyDraggables()
    {
        Destroy(GameObject.FindGameObjectWithTag("Draggable"));
    }

    public void Plus()
    {
        if (roomNr < 10)
        {
            roomNr++;
            if (!GameObject.Find("roomFloor" + roomNr.ToString()))
            {
                roomWalls = new GameObject("roomWalls" + roomNr.ToString());
                roomWalls.AddComponent<RandomColor>();

                roomFloor = new GameObject("roomFloor" + roomNr.ToString());
                roomFloor.AddComponent<RandomColor>();
                SpawnSound(ClickSound);
            }
        }
    }

    public void Minus()
    {
        if (roomNr > 1)
        {
            roomNr--;
            SpawnSound(ClickSound);
        }
    }

    private void SpawnSound(GameObject sound)
    {
        Instantiate(sound);
    }
}
