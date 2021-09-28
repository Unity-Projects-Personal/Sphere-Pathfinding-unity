using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    Grid grid;

    void Awake()
    {
        grid = GetComponent<Grid>();
    }

    public Stack<Node> FindPath(Node startPos, Node targetPos)
    {
        Node startNode = startPos;
        Node endNode = targetPos;
        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);
        grid.path.Add(startNode);
        grid.path.Add(endNode);

        while (openSet.Count > 0)
        {
            Node node = openSet[0];
            for (int i = 0; i < openSet.Count; i++)
            {
                if (openSet[i].FCost < node.FCost || openSet[i].FCost == node.FCost)
                {
                    if (openSet[i].h_cost < node.h_cost)
                        node = openSet[i];
                }
            }
            openSet.Remove(node);
            closedSet.Add(node);

            if (node.world_pos == endNode.world_pos)
            {
                return RetracePath(startNode, endNode);
            }

            foreach (Node neighbour in grid.GetNeighbours(node))
            {
                if (!neighbour.walkable || closedSet.Contains(neighbour))
                {
                    continue;
                }

                int newCostToNeighbour = node.g_cost + GetDistance(node, neighbour);

                if (newCostToNeighbour < neighbour.g_cost || !openSet.Contains(neighbour))
                {
                    neighbour.g_cost = newCostToNeighbour;
                    neighbour.h_cost = GetDistance(neighbour, endNode);
                    neighbour.parent = node;

                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                }
            }
        }

        return null;
    }

    Stack<Node> RetracePath(Node startNode, Node endNode)
    {
        Stack<Node> path = new Stack<Node>();
        Node currentNode = endNode;
        grid.path = new List<Node>();

        while (currentNode != startNode)
        {
            grid.path.Add(currentNode);
            path.Push(currentNode);
            currentNode = currentNode.parent;
        }

        return path;
    }
    int GetDistance(Node nodeA, Node nodeB)
    {
        int dstX = Mathf.Abs(nodeA.i - nodeB.i);
        int dstY = Mathf.Abs(nodeA.j - nodeB.j);

        if (dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY);

        return 14 * dstX + 10 * (dstY - dstX);
    }
}
