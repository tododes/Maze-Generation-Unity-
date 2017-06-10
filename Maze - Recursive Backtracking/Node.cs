using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {
    [SerializeField] private int x, y;
    [SerializeField] private List<Node> neighbour = new List<Node>();
    public Node next, prev;
    [SerializeField] private bool visited;

    public void visit() { visited = true; }
    public bool isVisited() { return visited; }

    public void AddNeighbour(Node n){
        neighbour.Add(n);
    }

    public void setCoord(int x, int y){
        this.x = x;
        this.y = y;
    }

    public Node getNeighbourAt(int index) { return neighbour[index]; }
    public int getNeighbourCount() { return neighbour.Count; }

    public void setNeighboursfromGraph(Node[,] graph, int xl, int yl){
        for (int i = x-1; i <= x+1; i++){
            for (int j = y-1; j <= y+1; j++){
                if((i >= 0 && j >= 0 && i < xl && j < yl) && !(i == x && j == y) && !(i != x && j != y))
                    AddNeighbour(graph[i, j]);
            }
        }
    }

    public void UnNeighbour(int i) {
        neighbour.RemoveAt(i);
    }

    public void UnNeighbour(Node n){
        neighbour.Remove(n);
    }
}
