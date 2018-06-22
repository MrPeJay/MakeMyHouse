using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {
    [SerializeField]
    private int size = 1;

    public int xSize = 50;
    public int zSize = 50;

    public bool InGame = true;

    private Decision DecisionScript;

    private GameObject Constraint1;
    private GameObject Constraint2;

    private void Start()
    {
        Constraint1 = GameObject.FindGameObjectWithTag("CONSTRAINT");
        Constraint2 = GameObject.FindGameObjectWithTag("CONSTRAINT1");

        if (!InGame)
        {
            DecisionScript = GameObject.FindGameObjectWithTag("Decision").GetComponent<Decision>();

            xSize = DecisionScript.Length;
            zSize = DecisionScript.Width;
        }
        gameObject.transform.localScale += new Vector3(xSize, 0, zSize);

        Constraint1.transform.position += new Vector3(0,0,zSize);
        Constraint2.transform.position += new Vector3(xSize, 0, 0);
    }

    public Vector3 GetNearestPointOnGrid(Vector3 position)
    {
        position -= transform.position;

        int xCount = Mathf.RoundToInt(position.x / size);
        int yCount = Mathf.RoundToInt(position.y / size);
        int zCount = Mathf.RoundToInt(position.z / size);

        Vector3 result = new Vector3(
            (float)xCount * size,
            (float)yCount * size,
            (float)zCount * size);

        result += transform.position;

        return result;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        for (int x = 0; x < xSize; x += size)
        {
            for (int z = 0; z < zSize; z += size)
            {
                var point = GetNearestPointOnGrid(new Vector3(x, 0f, z));
                Gizmos.DrawSphere(point,.1f);
            }
        }
    }
}
