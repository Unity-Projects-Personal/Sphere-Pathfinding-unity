using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public bool walkable;
    public Vector3 world_pos;
    public Node parent;
    public int g_cost;
    public int h_cost;
    public int i;
    public int j;
    public Node(bool walkable, Vector3 world_pos, int i, int j)
    {
        this.walkable = walkable;
        this.world_pos = world_pos;
        this.i = i;
        this.j = j;
    }
    public int FCost
    {
        get
        {
            return g_cost + h_cost;
        }
    }
}
