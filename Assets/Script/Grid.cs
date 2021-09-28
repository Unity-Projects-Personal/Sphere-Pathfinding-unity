using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    float radius = 5f;

    int sector_count = 30;
    int stack_count = 30;

    public Dictionary<Vector2, Node> grid = new Dictionary<Vector2, Node>();

    public GameObject player;
    public Node player_node;
    Vector3 last_player_pos;


    public LayerMask objectMask;

    void Start()
    {
        last_player_pos = player.transform.position;

        float sectorStep = 2 * Mathf.PI / sector_count;
        float stackStep = Mathf.PI / stack_count;
        float sectorAngle, stackAngle;

        float x, y, z, xy;



        for (int i = 0; i < stack_count; i++)
        {
            stackAngle = Mathf.PI / 2 - i * stackStep;
            xy = radius * Mathf.Cos(stackAngle);
            z = radius * Mathf.Sin(stackAngle);


            for (int j = 0; j < sector_count; j++)
            {
                sectorAngle = j * sectorStep;

                x = xy * Mathf.Cos(sectorAngle);
                y = xy * Mathf.Sin(sectorAngle);

                bool walkable = Physics.CheckSphere(new Vector3(x, y, z), 0.5f, objectMask);

                Node n = new Node(!walkable, new Vector3(x, y, z), i, j);

                grid.Add(new Vector2(i, j), n);

            }
        }

        player_node = grid[new Vector2(10, 10)];
        player.transform.position = player_node.world_pos + new Vector3(0.5f, 0.5f, 0.5f);
    }

    void Update()
    {
        if (last_player_pos != player.transform.position)
        {
            List<Node> nearby = GetNeighbours(player_node);

            float smallestDistance = Vector3.Distance(player_node.world_pos, player.transform.position);
            Node closestNode = player_node;
            foreach (Node node in nearby)
            {
                float distance = Vector3.Distance(node.world_pos, player.transform.position);
                if (distance < smallestDistance)
                {
                    smallestDistance = distance;
                    closestNode = node;
                }
            }
            last_player_pos = player.transform.position;
            player_node = closestNode;
        }
    }

    public List<Node> rand = new List<Node>();

    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        for (int x = node.i - 1; x <= node.i + 1; x++)
        {
            for (int y = node.j - 1; y <= node.j + 1; y++)
            {
                if (y == node.j && x == node.i)
                    continue;
                int r = x;
                int l = y;
                if (Mathf.Sign(x) == -1)
                    r = 29;
                if (Mathf.Sign(y) == -1)
                    l = 29;
                neighbours.Add(grid[new Vector2(r % (stack_count), l % (stack_count))]);
            }
        }

        return neighbours;
    }

    Vector3 size = new Vector3(0.5f, 0.5f, 0.5f);
    public List<Node> path = new List<Node>();

    void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            for (int i = 0; i < path.Count; i++)
            {
                Gizmos.color = Color.white;
                Gizmos.DrawSphere(path[i].world_pos, 0.25f);
            }

            Gizmos.color = Color.green;
            Gizmos.DrawSphere(player_node.world_pos, 0.25f);
        }
    }


}
