using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickManager : MonoBehaviour
{
    public int row;
    public int columns;
    public float padding;
    public GameObject brickPrefab;


    // Start is called before the first frame update
    void Start()
    {
        resetBoard();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void resetBoard()
    {
        for (int i = 0; i < row; i ++) {
            for (int j = 0; j < columns; j ++) {
                Vector2 pos2D = (Vector2) transform.position + new Vector2(
                    j * (brickPrefab.transform.localScale.x + padding),
                    -i * (brickPrefab.transform.localScale.y + padding));
                Vector3 pos3D = new Vector3(pos2D.x, pos2D.y, 1);
                GameObject brick = Instantiate(brickPrefab, pos3D, Quaternion.identity);
            }
        }
    }
}

