using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour
{
	public static BoardController instance;
	public RectTransform boardParent;
	public BoardSize boardSize;
	public Vector2 cellSize; 
	private Tile[,] tilesArray;
    bool hasEmptySpace;

    public static bool gameover;

	private void Awake()
	{
		if(instance == null)
			instance = this;
        gameover = true;
	}


    public void StartGame()
    {
        GenerateBoard();
        SpawnTile();
        SpawnTile();
        gameover = false;
    }

    public void RestartGame()
    {
        for(int i = 0; i < boardSize.x; i++)
        {
            for(int j = 0; j < boardSize.y; j++)
            {
                if(tilesArray[i,j] != null)
                {
                    Destroy(tilesArray[i, j].gameObject);
                    tilesArray[i, j] = null;
                }
            }
        }
        SpawnTile();
        SpawnTile();
        gameover = false;
    }

	private void GenerateBoard()
	{
		boardParent.sizeDelta = new Vector2(boardSize.x * cellSize.x, boardSize.y * cellSize.y);
		tilesArray = new Tile[boardSize.x, boardSize.y];
	}	

    private (int, int) GetRandomPosition()
    {
        int rX = Random.Range(0, boardSize.x);
        int rY = Random.Range(0, boardSize.y);
        for(int i = 0; i < boardSize.x; i++)
        {
            for(int j = 0; j < boardSize.y; j++)
            {
                if(tilesArray[rX,rY] == null)
                {
                    return (rX, rY);
                }
                else
                {
                    if(rY == boardSize.y - 1)
                    {
                        rY = 0;
                    }  
                    else rY++;
                }
            }
            if(rX == boardSize.x - 1)
            {
                rX = 0;
            }
            else rX++;
        }
        return default;
    }


    private bool CheckEmptySpace()
    {
        for(int i = 0; i < boardSize.x; i++)
        {
            for(int j = 0; j < boardSize.y; j++)
            {
                if(tilesArray[i,j] == null)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private bool CheckForGameOver(bool print = false)
    {
        int adjX, adjY;
        for(int i = 0; i < boardSize.x; i++)
        {
            adjX = i + 1;
            for(int j = 0; j < boardSize.y; j++)
            {
                adjY = j + 1;
                if(print)
                    Debug.Log("<color=red>Main" + i + "," + j + " => " + tilesArray[i, j].Value + "</color>");
                if(adjX < boardSize.x)
                {
                    if(print)
                        Debug.Log("<color=yellow>X" + adjX + "," + j + " => " + tilesArray[adjX, j].Value + "</color>");
                    if(tilesArray[i, j].Value == tilesArray[adjX, j].Value)
                        return false;
                }
                if(adjY < boardSize.y)
                {
                    if(print)
                        Debug.Log("<color=yellow>Y" + i + "," + adjY + " => " + tilesArray[i, adjY].Value + "</color>");
                    if(tilesArray[i, j].Value == tilesArray[i, adjY].Value)
                        return false;
                }
            }
        }
        return true;
    }

	private void SpawnTile()
	{
		if(hasEmptySpace || CheckEmptySpace())
		{
			(int, int) random = GetRandomPosition();
			Tile tile = (Tile)Instantiate(Resources.Load<GameObject>("Tile"), boardParent).AddComponent(typeof(Tile));
            AddTile(random, tile);
			tile.Coordinate = random;
			tile.Value = 2;
			tile.UpdateUIAnimation();
			tile.SetPosition(GetPositionInBoard(random), false);
            tile.InAnimation();
            hasEmptySpace = CheckEmptySpace();
            if (!hasEmptySpace)
            {
                if (CheckForGameOver())
                {
                    Debug.LogError("GameOver");
                    gameover = true;
                    //CheckForGameOver(true);//Testing purpose
                    UIManager.instance.OpenGameOverPanel();
                }   
            }

		}
		//else Debug.LogError("Board Full");
	}

    private Vector2 GetPositionInBoard((int, int) coordinate)
	{
		float x = (coordinate.Item1 - boardSize.x / 2) * cellSize.x + cellSize.x / 2;
		float y = (coordinate.Item2 - boardSize.y / 2) * cellSize.y + cellSize.y / 2;;
		return new Vector2(x, y);
	}

	public void MakeMove(Vector2 direction)
	{
        if(direction == Vector2.up)//0,1
        {
            for(int i = 0; i < boardSize.y; i++)
            {
                List<(int, int)> coordinates = new List<(int, int)>();
                for(int j = boardSize.x - 1; j > -1; j--)
                {
                    //Debug.Log(i + " " + j);
                    if(tilesArray[i, j] != null)
                    {
                        tilesArray[i, j].moved = false;
                        MergeTile(tilesArray[i, j], coordinates);
                    }
                    coordinates.Add((i, j));
                }
            }
        }
		else if(direction == Vector2.down)//0,-1
        {
            for(int i = 0; i < boardSize.x; i++)
            {
                List<(int, int)> coordinates = new List<(int, int)>();
                for(int j = 0; j < boardSize.y; j++)
                {
                    //Debug.Log(i + " " + j);
                    if(tilesArray[i, j] != null)
                    {
                        tilesArray[i, j].moved = false;
                        MergeTile(tilesArray[i, j], coordinates);
                    }
                    coordinates.Add((i, j));
                }
            }
        }
        else if(direction == Vector2.right)//1,0
        {
            for(int i = 0; i < boardSize.y; i++)
            {
                List<(int, int)> coordinates = new List<(int, int)>();
                for(int j = boardSize.x - 1; j > -1; j--)
                {
                    //Debug.Log(j + " " + i);
                    if(tilesArray[j, i] != null)
                    {
                        tilesArray[j, i].moved = false;
                        MergeTile(tilesArray[j, i], coordinates);
                    }
                    coordinates.Add((j, i));
                }
            }
        }
        else if(direction == Vector2.left)//-1,0
        {
            for(int i = 0; i < boardSize.x; i++)
            {
                List<(int, int)> coordinates = new List<(int, int)>();
                for(int j = 0; j < boardSize.y; j++)
                {
                    //Debug.Log(j + " " + i);
                    if(tilesArray[j, i] != null)
                    {
                        tilesArray[j, i].moved = false;
                        MergeTile(tilesArray[j, i], coordinates);
                    }
                    coordinates.Add((j, i));
                }
            }
        }
        SpawnTile();
        //Debug.Log(emptySpaces.Count);
	}

    private void MergeTile(Tile tile, List<(int, int)> coordinates)
    {
        bool move = false;
        (int, int) coordinate = tile.Coordinate;
        for(int i = coordinates.Count - 1; i > -1; i--)
        {
            if(tilesArray[coordinates[i].Item1, coordinates[i].Item2] == null)
            {
                coordinate = coordinates[i];
                move = true;
            }
            else
            {
                if(!tilesArray[coordinates[i].Item1, coordinates[i].Item2].moved && tilesArray[coordinates[i].Item1, coordinates[i].Item2].Value == tile.Value)
                {
                    RemoveTile(tile.Coordinate);
                    tilesArray[coordinates[i].Item1, coordinates[i].Item2].Value *= 2;
                    tilesArray[coordinates[i].Item1, coordinates[i].Item2].moved = true;
                    tile.Coordinate = coordinate;
                    tile.SetPosition(GetPositionInBoard(tile.Coordinate), true, () => {
                        Destroy(tile.gameObject);
                        tilesArray[coordinates[i].Item1, coordinates[i].Item2].MergeAnimation();
                        tilesArray[coordinates[i].Item1, coordinates[i].Item2].UpdateUIAnimation();
                    });
                }
                break;
            } 
        }
        if (move)
        {
            RemoveTile(tile.Coordinate);
            AddTile(coordinate, tile);
            tile.Coordinate = coordinate;
            tile.SetPosition(GetPositionInBoard(tile.Coordinate), true);
        }
            
    }

    private void RemoveTile((int, int) coordinates)
    {
        tilesArray[coordinates.Item1, coordinates.Item2] = null;    
    }

    private void AddTile((int, int) coordinates, Tile tile)
    {       
        tilesArray[coordinates.Item1, coordinates.Item2] = tile;
    }
}

