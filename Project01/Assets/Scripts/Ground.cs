using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ground : MonoBehaviour
{

    public GameObject[] tiles;
    private int sunkFirst = -1;
    private int sunkSecond = -1;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(sunkFirst != -1)
        {
            tiles[sunkFirst].transform.Translate(Vector3.up * Time.deltaTime * 4);
            if(tiles[sunkFirst].transform.position.y >= -0.5f)
            {
                tiles[sunkFirst].transform.position = new Vector3(tiles[sunkFirst].transform.position.x,  - 0.5f, tiles[sunkFirst].transform.position.z);
                sunkFirst = -1;
            }
        }
        if (sunkSecond != -1)
        {
            tiles[sunkSecond].transform.Translate(Vector3.up * Time.deltaTime * 4);
            if (tiles[sunkSecond].transform.position.y >= -0.5f)
            {
                tiles[sunkSecond].transform.position = new Vector3(tiles[sunkSecond].transform.position.x, -0.5f, tiles[sunkSecond].transform.position.z);
                sunkSecond = -1;
            }
        }

        if (Input.GetKeyDown("escape"))
        {
            Application.Quit();
        }
        if (Input.GetKeyDown("backspace"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void sinkGround(int first, int second)
    {
        tiles[first].transform.position = new Vector3(tiles[first].transform.position.x, tiles[first].transform.position.y-32f, tiles[first].transform.position.z);
        tiles[second].transform.position = new Vector3(tiles[second].transform.position.x, tiles[second].transform.position.y-32f, tiles[second].transform.position.z);
        sunkFirst = first;
        sunkSecond = second;

    }
}
