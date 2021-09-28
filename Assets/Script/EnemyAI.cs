using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Pathfinding pathfinding;

    public Stack<Node> path = new Stack<Node>();

    public Node my_node;

    GameObject my_transform;

    public Grid grid;

    public float speed;

    void Start()
    {
        my_transform = gameObject;
    }

    void FixedUpdate()
    {
        if (my_node == null)
        {

            my_node = grid.grid[new Vector2(10, 19)];
            my_transform.transform.position = my_node.world_pos;
        }
        else
        {
            if (path.Count != 0 || path == null)
            {
                if (Vector3.Distance(path.Peek().world_pos, my_transform.transform.position) > 1f)
                {
                    float step = speed * Time.deltaTime;

                    my_transform.transform.position = Vector3.MoveTowards(my_transform.transform.position, path.Peek().world_pos, step);
                }
                else
                {
                    my_node = path.Peek();
                    path.Pop();
                }
            }
            else
            {
                path = pathfinding.FindPath(my_node, grid.player_node);
            }
        }

    }

    void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.color = Color.black;
            if (path.Count != 0)
                Gizmos.DrawSphere(path.Peek().world_pos, 0.5f);
        }
    }
}
