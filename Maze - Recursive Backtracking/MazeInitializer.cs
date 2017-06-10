using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeInitializer : MonoBehaviour {

    [SerializeField]
    private int sizeX, sizeY;

    [SerializeField]
    private GameObject cube;

    public static MazeInitializer singleton;
    private Node[,] graph;
    private GameObject g;

    [SerializeField]
    private List<Node> stack = new List<Node>();

    void Awake(){
        singleton = this;
        graph = new Node[sizeX, sizeY];
        for (int i = 0; i < sizeX; i++){
            for (int j = 0; j < sizeY; j++){
                g = Instantiate(cube, new Vector3(i, 0, j), Quaternion.identity) as GameObject;
                g.name = "Node " + i + " " + j;
                graph[i, j] = g.GetComponent<Node>();
                graph[i, j].setCoord(i, j);
            }
        }
        SetNeighbours();
        GenerateMaze(graph[0,0]);
    }

    private void SetNeighbours(){
        for (int i = 0; i < sizeX; i++){
            for (int j = 0; j < sizeY; j++){
                graph[i, j].setNeighboursfromGraph(graph, sizeX, sizeY);
            }
        }
    }

    private void GenerateMaze(Node Initial){
        Node current = Initial;
        stack.Add(Initial);
        Initial.visit();
        List<Node> adjacents = new List<Node>();
        while(stack.Count > 0) {
            Debug.Log("Current : " + current.name);
            
            for (int i = 0; i < current.getNeighbourCount(); i++){
                if (!stack.Contains(current.getNeighbourAt(i)) && !current.getNeighbourAt(i).isVisited()){
                    stack.Add(current.getNeighbourAt(i));
                }
                if (!current.getNeighbourAt(i).isVisited()){
                    adjacents.Add(current.getNeighbourAt(i));
                }
            }
            current.visit();
            stack.Remove(current);
            if (adjacents.Count > 0){
                int r = Random.Range(0, adjacents.Count);
                Node n = adjacents[r];
                current.next = n;
                n.prev = current;
                n.visit();
                current = n;
                adjacents.Clear();
            }
            else{
                current = current.prev;
            }
        }
    }
}
