using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour {

    public int X, Y;
    public int gridState;
    public bool visited;

    private MeshRenderer meshRenderer;
    private Collider coll;

    public Grid parent;
    public List<Grid> neighbours = new List<Grid>();
    public List<Grid> connectors = new List<Grid>();

    private Vector3 SmallX, SmallZ, SmallBoth;
    public bool DestroyedOnGeneration;

    private bool SizeDecided;
    private MazeGenerator mazeGenerator;

    [SerializeField] private Transform mainPlayerTransform;

    void Start()
    {
        DestroyedOnGeneration = false;
        visited = false;
        meshRenderer = GetComponent<MeshRenderer>();
        coll = GetComponent<Collider>();

        if(gridState == 1)
        {
            Disable();
        }
        SmallX = new Vector3(0.25f, 1, 1.75f);
        SmallZ = new Vector3(1.75f, 1, 0.25f);
        SmallBoth = new Vector3(0.25f, 1, 0.25f);

        SizeDecided = false;
        mazeGenerator = MazeGenerator.singleton;
    }

    void Update()
    {
    }

    void LateUpdate()
    {
        if (!SizeDecided)
        {
            if ((X % 2 == 0 || Y % 2 == 0) && X > 0 && Y > 0 && X < mazeGenerator.getSize().x && Y < mazeGenerator.getSize().y)
            {
                if (X % 2 == 1 && Y % 2 == 0){
                    transform.localScale = SmallZ;
                }
                else if (X % 2 == 0 && Y % 2 == 1){
                    transform.localScale = SmallX;
                }
                else{
                    transform.localScale = SmallBoth;
                }
            }
            //if ((X % 2 == 0 || Y % 2 == 0) && !DestroyedOnGeneration)
            //{
            //    if (Vector3.Distance(transform.position, mainPlayerTransform.position) > 17f && meshRenderer.enabled)
            //        meshRenderer.enabled = false;
            //    else if (Vector3.Distance(transform.position, mainPlayerTransform.position) <= 17f && !meshRenderer.enabled)
            //        meshRenderer.enabled = true;
            //}
            SizeDecided = true;
        }
    }

    public void Enable()
    {
        if (meshRenderer)
            meshRenderer.enabled = true;
        if (coll)
            coll.enabled = true;
    }

    public void Disable()
    {
        if (meshRenderer)
            meshRenderer.enabled = false;
        if (coll)
            coll.enabled = false;
    }

    public void AddNeighbour(Grid g)
    {
        neighbours.Add(g);
    }

    public void AddConnector(Grid g)
    {
        connectors.Add(g);
    }
}
