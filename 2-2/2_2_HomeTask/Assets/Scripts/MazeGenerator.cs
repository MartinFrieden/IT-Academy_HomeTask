using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGeneratorCell
{
    public int X;
    public int Y;

    public bool wallLeft = true;
    public bool wallBottom = true;
    public bool doorLeft = false;
    public bool doorBottom = false;


    public bool vizited = false;

    public int distanceFromStart;
}

public class MazeGenerator 
{
    public int width = 7;
    public int height = 7;
    public MazeGeneratorCell[,] GenerateMaze()
    {
        MazeGeneratorCell[,] maze = new MazeGeneratorCell[width, height];

        for (int x = 0; x < maze.GetLength(0); x++)
        {
            for (int y = 0; y < maze.GetLength(1); y++)
            {
                maze[x, y] = new MazeGeneratorCell{X = x, Y = y};
            }
        }

        for (int x = 0; x < maze.GetLength(0); x++)
        {
            maze[x, height - 1].wallLeft = false;
        }

        for (int y = 0; y < maze.GetLength(1); y++)
        {
            maze[width - 1, y].wallBottom = false;
        }

        RemoveWallsWithBackTracker(maze);

        PlaceMazeExit(maze);
        return maze;
    }

    private void PlaceMazeExit(MazeGeneratorCell[,] maze)
    {
        MazeGeneratorCell furthest = maze[0,0];

        for (int x = 0; x < maze.GetLength(0); x++)
        {
            if (maze[x, height - 2].distanceFromStart > furthest.distanceFromStart)
                furthest = maze[x, height - 2];
            if (maze[x, 0].distanceFromStart > furthest.distanceFromStart)
                furthest = maze[x, 0];
        }

        for (int y = 0; y < maze.GetLength(1); y++)
        {
            if (maze[width-2, y].distanceFromStart > furthest.distanceFromStart)
                furthest = maze[width-2, y];
            if (maze[0, y].distanceFromStart > furthest.distanceFromStart)
                furthest = maze[0, y];
        }

        if (furthest.X ==0)
        {
            furthest.wallLeft = false;
            furthest.doorLeft = true;
        }
        else if(furthest.Y == 0)
        {
            furthest.wallBottom = false;
            furthest.doorBottom = true;
        }
        else if (furthest.X == width-2)
        {
            maze[furthest.X + 1, furthest.Y].wallLeft = false;
            maze[furthest.X + 1, furthest.Y].doorLeft = true;
        }
        else if (furthest.Y == height - 2)
        {
            maze[furthest.X, furthest.Y+1].wallBottom = false;
            maze[furthest.X, furthest.Y + 1].doorBottom = true;
        }

    }

    public void RemoveWallsWithBackTracker(MazeGeneratorCell[,] maze)
    {
        MazeGeneratorCell current = maze[0, 0];
        current.vizited = true;
        current.distanceFromStart = 0;

        Stack<MazeGeneratorCell> stack = new Stack<MazeGeneratorCell>();

        do
        {
            List<MazeGeneratorCell> unvizitedNeighbours = new List<MazeGeneratorCell>();
            int x = current.X;
            int y = current.Y;

            if (x>0 && !maze[x-1, y].vizited)
                unvizitedNeighbours.Add(maze[x - 1, y]);
            if (y > 0 && !maze[x, y-1].vizited)
                unvizitedNeighbours.Add(maze[x, y-1]);
            if (x < width-2 && !maze[x + 1, y].vizited)
                unvizitedNeighbours.Add(maze[x + 1, y]);
            if (y < height-2 && !maze[x, y+1].vizited)
                unvizitedNeighbours.Add(maze[x, y+1]);

            if (unvizitedNeighbours.Count >0)
            {
                MazeGeneratorCell chosen = unvizitedNeighbours[UnityEngine.Random.Range(0, unvizitedNeighbours.Count)];
                RemoveWall(current, chosen);
                chosen.vizited = true;
                
                stack.Push(chosen);
                current = chosen;
                chosen.distanceFromStart = stack.Count;

            }
            else
            {
                current = stack.Pop();
            }



        } while (stack.Count>0);
    }

    private void RemoveWall(MazeGeneratorCell a, MazeGeneratorCell b)
    {
        if (a.X == b.X)
        {
            if (a.Y > b.Y)
                a.wallBottom = false;
            else
                b.wallBottom = false;
        }
        else
        {
            if (a.X > b.X) a.wallLeft = false;
            else b.wallLeft = false;
        }
    }
}
