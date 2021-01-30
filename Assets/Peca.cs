using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Peca : MonoBehaviour
{

    private float pTime; //tempo anterior
    private float fTime = .6f; //Tempo de queda
    private Touch touch;
    public float ms = .01f; //Velocidade de movimento pecas
    public Vector2 mouse;
    public Vector2 exmouse;
    public int move;
    private static int minX = 0;
    private static int minY = 0;
    private static int maxX = 17;
    private static int maxY = 22;
    private static Transform[,] grid = new Transform[maxX, maxY];
    public Vector3 rPoint; //ponto de rotação



    void Update()
    {

        if (transform.position.y >= maxY)
        {
            //gameOver();
        }

        if (Input.GetKey(KeyCode.DownArrow) || (Time.time - pTime) >= (float)1 / (float)Mathf.Sqrt(LevelManager.level))
        {
            transform.position += new Vector3(0, -1, 0);
            if (!vMove() /*&& !gameOver()*/)
            {
                transform.position += new Vector3(0, 1, 0);
                AddToGrid();
                CheckLine();
                this.enabled = false;
                FindObjectOfType<Spawner>().Spawn();

            }
            pTime = Time.time;

        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(ms, 0, 0);
            if (!vMove())
            {
                transform.position += new Vector3(-ms, 0, 0);
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);
            if (!vMove())
            {
                transform.position += new Vector3(1, 0, 0);
            }
        }
        //Rotação da peça
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.RotateAround(transform.TransformPoint(rPoint), new Vector3(0, 0, 1), 90);
            if (!vMove())
            {
                transform.RotateAround(transform.TransformPoint(rPoint), new Vector3(0, 0, 1), -90);
            }
        }



        void AddToGrid()
        {
            foreach (Transform children in transform)
            {

                int arx = Mathf.RoundToInt(children.transform.position.x);
                int ary = Mathf.RoundToInt(children.transform.position.y);


                grid[arx, ary] = children;
            }
        }

        bool vMove() //checa se um movimento é valido
        {
            foreach (Transform children in transform)
            {
                int arx = Mathf.RoundToInt(children.transform.position.x);
                int ary = Mathf.RoundToInt(children.transform.position.y);

                if (arx < minX || arx >= maxX || ary < minY || arx >= maxY - 1)
                {
                    return false;
                }
                if (grid[arx, ary] != null)
                {
                    return false;
                }

            }

            return true;
        }
        void CheckLine()
        {
            for (int i = maxY - 1; i >= 0; i--)
            {

                if (HasLine(i))
                {
                    DelLine(i);
                    RowDown(i);
                    ScoreManager.score += (maxY - i) * 10;
                    i++;
                }
            }
        }
        bool HasLine(int i)
        {
            for (int j = 0; j < maxX; j++)
            {
                if (grid[j, i] == null)
                {
                    return false;

                }

            }
            Debug.Log("aaaa");
            return true;
        }
        /*bool gameOver()
        {
            Debug.Log("GAME OVER!");
            if (transform.position.y + 1) >= maxY){
                while (!vMove())
                {

                    transform.position += new Vector3(0, 1, 0);

                }
                enabled = false;
                return true;
            }
            return false;
        }
        */
        void DelLine(int i)
        {
            for (int j = 0; j < maxX; j++)
            {
                Destroy(grid[j, i].gameObject);
                grid[j, i] = null;
                Debug.Log("bbbb");
            }
        }
        void RowDown(int i)
        {
            for (int k = i; k < maxY; k++)
            {
                for (int j = 0; j < maxX; j++)
                {
                    if (grid[j, k] != null)
                    {
                        grid[j, k - 1] = grid[j, k];
                        grid[j, k] = null;
                        grid[j, k - 1].transform.position += new Vector3(0, -1, 0);
                        Debug.Log("cccc");
                    }
                }
            }
        }
    }
}
