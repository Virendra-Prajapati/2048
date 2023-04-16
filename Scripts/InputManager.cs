using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    float time;
	private void Update()
	{
        if(BoardController.gameover)
            return;
        if(time < 0.2f)
        {
            time += Time.deltaTime;
            return;
        }

        //ForAutoPlay
        /*switch(Random.Range(0, 4))
        {
            case 0:
                BoardController.instance.MakeMove(Vector2.up);
                break;
            case 1:
                BoardController.instance.MakeMove(Vector2.down);
                break;
            case 2:
                BoardController.instance.MakeMove(Vector2.right);
                break;
            case 3:
                BoardController.instance.MakeMove(Vector2.left);
                break;
        }
        time = 0;
        return;*/

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            time = 0;
            BoardController.instance.MakeMove(Vector2.up);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            time = 0;
            BoardController.instance.MakeMove(Vector2.down);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            time = 0;
            BoardController.instance.MakeMove(Vector2.left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            time = 0;
            BoardController.instance.MakeMove(Vector2.right);
        }
    }
}
