using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MazeSpawner : MonoBehaviour
{
    public GameObject cellPrefab;
    public Vector3 cellSize = new Vector3(1,1,0);
    public NavMeshSurface navMeshSurface;
    // Start is called before the first frame update
    void Start()
    {
        MazeGenerator generator = new MazeGenerator();
        MazeGeneratorCell[,] maze = generator.GenerateMaze();

        for (int x = 0; x < maze.GetLength(0); x++)
        {
            for (int y = 0; y < maze.GetLength(1); y++)
            {
                Cell c = Instantiate(cellPrefab, new Vector3(x*cellSize.x,y*cellSize.y, y*cellSize.z), Quaternion.identity).GetComponent<Cell>();

                c.wallLeft.SetActive(maze[x,y].wallLeft);
                c.wallBottom.SetActive(maze[x, y].wallBottom);

                c.doorLeft.SetActive(maze[x,y].doorLeft);
                c.doorBottom.SetActive(maze[x, y].doorBottom);
            }
        }
        navMeshSurface.BuildNavMesh();
    }

    void Update()
    {
        
    }
}
