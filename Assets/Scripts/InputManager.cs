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
        /*switch (Random.Range(0, 4))
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


        switch (DetectSwipe())
        {
            case Swipe.Up:
                time = 0;
                BoardController.instance.MakeMove(Vector2.up);
                break;
            case Swipe.Down:
                time = 0;
                BoardController.instance.MakeMove(Vector2.down);
                break;
            case Swipe.Left:
                time = 0;
                BoardController.instance.MakeMove(Vector2.left);
                break;
            case Swipe.Right:
                time = 0;
                BoardController.instance.MakeMove(Vector2.right);
                break;
        }

        /*if (Input.GetKeyDown(KeyCode.UpArrow))
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
        }*/
    }



    public float minSwipeLength = 5f;
    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;
 
    Vector2 firstClickPos;
    Vector2 secondClickPos;
  
  
   
    public Swipe DetectSwipe ()
    {
        if (Input.touches.Length > 0) 
        {
            Touch t = Input.GetTouch(0);
           
            if (t.phase == TouchPhase.Began) 
            {
                firstPressPos = new Vector2(t.position.x, t.position.y);
            }
           
            if (t.phase == TouchPhase.Ended) 
            {
                secondPressPos = new Vector2(t.position.x, t.position.y);
                currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
               
                // Make sure it was a legit swipe, not a tap
                if (currentSwipe.magnitude < minSwipeLength) 
                {
                    return Swipe.None;
                }
               
                currentSwipe.Normalize();
               
                // Swipe up
                if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f) {
                    return Swipe.Up;
                    // Swipe down
                } else if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f) {
                    return Swipe.Down;
                    // Swipe left
                } else if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f) {
                    return Swipe.Left;
                    // Swipe right
                } else if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f) {
                    return Swipe.Right;
                }
            }
        } 
        else 
        {
            if (Input.GetMouseButtonDown(0)) 
            {
                firstClickPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            } 
            else 
            {
                return Swipe.None;
                //Debug.Log ("None");
            }
            if (Input.GetMouseButtonUp (0))
            {
                secondClickPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                currentSwipe = new Vector3(secondClickPos.x - firstClickPos.x, secondClickPos.y - firstClickPos.y);
 
                // Make sure it was a legit swipe, not a tap
                if (currentSwipe.magnitude < minSwipeLength) 
                {
                    return Swipe.None;
                }
               
                currentSwipe.Normalize();
 
                //Swipe directional check
                // Swipe up
                if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f) {
                    return Swipe.Up;
                    // Swipe down
                } else if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f) {
                   return Swipe.Down;
                    // Swipe left
                } else if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f) {
                    return Swipe.Left;
                    // Swipe right
                } else if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f) {
                    return Swipe.Right;
                }
            }
        }
        return Swipe.None;
    }
}

public enum Swipe { None, Up, Down, Left, Right };
