using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GridManager : MonoBehaviour
{

    public Transform[] startingPositions;
    public GameObject[] colors; //index 0 --> Selected color, 1 --> Random color ONE, 2 --> random color TWO

    private GameObject[] reds;
    private GameObject[] blues;
    private GameObject[] greens;

    public GameObject selectPanel;
    public GameObject buttonGenerate;
    public GameObject buttonRegenrate;

    private int direction;
    public float moveAmount;

    private float timeBtwColor;
    public float startTimeBtwColor = 0.25f;

    public float minX;
    public float maxX;
    public float maxY;
    public bool stopGeneration;

    public LayerMask color;

    private int selectedColorIndex;
    private int selectedColorTileCounter = 0;

    private int upCounter;

    public void Start()
    {
        //Generate();
    }

    public void Generate()
    {
        Debug.Log(selectedColorIndex);
        selectPanel.SetActive(false);
        buttonGenerate.SetActive(false);
        buttonRegenrate.SetActive(true);


        int randStartingPos = Random.Range(0, startingPositions.Length);
        transform.position = startingPositions[randStartingPos].position;
        //decide Color here. SelectedColorIndex
        Instantiate(colors[selectedColorIndex], transform.position, Quaternion.identity);
        selectedColorTileCounter = 1;
        direction = Random.Range(1, 4);
    }

    public void Regenerate()
    {
        reds = GameObject.FindGameObjectsWithTag("Red");
        foreach(GameObject item in reds)
        {
            Destroy(item);
        }
        blues = GameObject.FindGameObjectsWithTag("Blue");
        foreach (GameObject item in blues)
        {
            Destroy(item);
        }
        greens = GameObject.FindGameObjectsWithTag("Green");
        foreach (GameObject item in greens)
        {
            Destroy(item);
        }
        selectedColorTileCounter = 0;
        timeBtwColor = 0f;
        Generate();

    }
    private void Update()
    {
        if (timeBtwColor <= 0 && stopGeneration == false)
        {
            Move();
            timeBtwColor = startTimeBtwColor;
        }
        else
        {
            timeBtwColor -= Time.deltaTime;
        }
    }


    public void RedChosen()
    {
        for (int i = 0; i > 3; i++)
        {
            if (colors[i].gameObject.tag == ("Red"))
            {
                selectedColorIndex = i;
                Debug.Log("Red has been selected");
            }
        }
    }

    public void BlueChosen()
    {
        for (int i = 0; i > 3; i++)
        {
            if (colors[i].gameObject.tag == ("Blue"))
            {
                selectedColorIndex = i;
                Debug.Log("Blue has been selected");
            }
        }
    }

    public void GreenChosen()
    {
        for (int i = 0; i > 3; i++)
        {
            if (colors[i].gameObject.tag == ("Green"))
            {
                selectedColorIndex = i;
                Debug.Log("Green has been selected");
            }
        }
    }
    private void Move()
    {
        if (selectedColorTileCounter < 4)
        {
            if (direction == 1)
            {
                if (transform.position.x < maxX)
                {
                    selectedColorTileCounter++; //Two Tiles while max is threee

                    //GOING RIGHT
                    upCounter = 0;
                    Vector2 newPos = new Vector2(transform.position.x + moveAmount, transform.position.y);
                    transform.position = newPos;
                    Collider2D colorDetection = Physics2D.OverlapCircle(transform.position, 1, color);
                    //int rand = Random.Range(0, colors.Length);
                    Instantiate(colors[selectedColorIndex], transform.position, Quaternion.identity);

                    direction = Random.Range(1, 4);
                    if (selectedColorTileCounter == 2)
                    {
                        if (direction == 1)
                        {
                            if (transform.position.x < maxX)
                            {
                                Instantiate(colors[selectedColorIndex], (transform.position + new Vector3(moveAmount, 0, 0)), Quaternion.identity);
                                selectedColorTileCounter++;
                            }
                            else
                            {
                                Instantiate(colors[selectedColorIndex], (transform.position - new Vector3(2.0f, 0, 0)), Quaternion.identity);
                                selectedColorTileCounter++;
                            }
                        }
                        else if (direction == 2)
                        {
                            direction = 1;
                        }

                        else if (direction == 3)
                        {
                            if (transform.position.x == maxX)
                            {
                                Instantiate(colors[selectedColorIndex], (transform.position - new Vector3(2.0f, 0, 0)), Quaternion.identity);
                                selectedColorTileCounter++;
                            }
                            else
                            {
                                if (transform.position.y + moveAmount < maxY)
                                {
                                    colorDetection.GetComponent<ColorType>().ColorDestruction();
                                    Instantiate(colors[selectedColorIndex], (transform.position + new Vector3(0, 1f, 0)), Quaternion.identity);
                                    Instantiate(colors[selectedColorIndex], (transform.position + new Vector3(1f, 2f, 0)), Quaternion.identity);
                                    selectedColorTileCounter++;
                                }
                                else
                                {
                                    direction = 1;
                                }

                            }

                        }
                    }


                }
                else
                {
                    direction = 3;
                }
            }
            else if (direction == 2)
            {
                if (transform.position.x > minX)
                {
                    selectedColorTileCounter++; //Two Tiles while max is threee

                    //GOING LEFT
                    upCounter = 0;
                    Vector2 newPos = new Vector2(transform.position.x - moveAmount, transform.position.y);
                    transform.position = newPos;
                    Collider2D colorDetection = Physics2D.OverlapCircle(transform.position, 1, color);
                    //int rand = Random.Range(0, colors.Length);
                    Instantiate(colors[selectedColorIndex], transform.position, Quaternion.identity);

                    direction = Random.Range(1, 4);
                    if (selectedColorTileCounter == 2)
                    {
                        if (direction == 2)
                        {
                            if (transform.position.x > minX)
                            {
                                Instantiate(colors[selectedColorIndex], (transform.position - new Vector3(moveAmount, 0, 0)), Quaternion.identity);
                                selectedColorTileCounter++;
                            }
                            else
                            {
                                Instantiate(colors[selectedColorIndex], (transform.position + new Vector3(2.0f, 0, 0)), Quaternion.identity);
                                selectedColorTileCounter++;
                            }
                        }
                        else if (direction == 1)
                        {
                            direction = 2;
                        }
                        else if (direction == 3)
                        {
                            if (transform.position.x == minX)
                            {
                                Instantiate(colors[selectedColorIndex], (transform.position + new Vector3(2.0f, 0, 0)), Quaternion.identity);
                                selectedColorTileCounter++;
                            }
                            else
                            {
                                colorDetection.GetComponent<ColorType>().ColorDestruction();
                                Instantiate(colors[selectedColorIndex], (transform.position + new Vector3(0, 1f, 0)), Quaternion.identity);
                                Instantiate(colors[selectedColorIndex], (transform.position - new Vector3(1f, -2f, 0)), Quaternion.identity);
                                selectedColorTileCounter++;
                            }

                        }
                    }

                    else
                    {
                        direction = 3;
                    }

                }
            }
            else if (direction == 3)
            {
                if (transform.position.y < maxY)
                {
                    selectedColorTileCounter++; //Two Tiles while max is threee

                    //GOING Up
                    upCounter = 0;
                    Vector2 newPos = new Vector2(transform.position.x, transform.position.y + moveAmount);
                    transform.position = newPos;
                    Collider2D colorDetection = Physics2D.OverlapCircle(transform.position, 1, color);
                    //int rand = Random.Range(0, colors.Length);
                    Instantiate(colors[selectedColorIndex], transform.position, Quaternion.identity);

                    direction = Random.Range(1, 4);
                    if (selectedColorTileCounter == 2)
                    {
                        if (direction == 2)
                        {
                            if (transform.position.x > minX)
                            {
                                if ((transform.position.x - moveAmount) > minX)
                                {
                                    colorDetection.GetComponent<ColorType>().ColorDestruction();
                                    Instantiate(colors[selectedColorIndex], (transform.position - new Vector3(moveAmount, 0, 0)), Quaternion.identity);
                                    Instantiate(colors[selectedColorIndex], (transform.position - new Vector3(2f, 1f, 0)), Quaternion.identity);
                                    selectedColorTileCounter++;
                                }
                                else
                                {
                                    Instantiate(colors[selectedColorIndex], (transform.position + new Vector3(0, moveAmount, 0)), Quaternion.identity);
                                    selectedColorTileCounter++;
                                }


                            }
                            else
                            {
                                Instantiate(colors[selectedColorIndex], (transform.position + new Vector3(0, moveAmount, 0)), Quaternion.identity);
                                selectedColorTileCounter++;
                            }
                        }
                        else if (direction == 1)
                        {

                            if (transform.position.x < maxX)
                            {
                                if ((transform.position.x + moveAmount) < maxX)
                                {
                                    colorDetection.GetComponent<ColorType>().ColorDestruction();
                                    Instantiate(colors[selectedColorIndex], (transform.position + new Vector3(moveAmount, 0, 0)), Quaternion.identity);
                                    Instantiate(colors[selectedColorIndex], (transform.position + new Vector3(2f, 1f, 0)), Quaternion.identity);
                                    selectedColorTileCounter++;
                                }
                                else
                                {
                                    Instantiate(colors[selectedColorIndex], (transform.position + new Vector3(0, moveAmount, 0)), Quaternion.identity);
                                    selectedColorTileCounter++;
                                }


                            }
                            else
                            {
                                Instantiate(colors[selectedColorIndex], (transform.position + new Vector3(0, moveAmount, 0)), Quaternion.identity);
                                selectedColorTileCounter++;
                            }
                        }

                        else if (direction == 3)
                        {
                            if (transform.position.y < maxY)
                            {
                                Instantiate(colors[selectedColorIndex], (transform.position + new Vector3(0, moveAmount, 0)), Quaternion.identity);
                                selectedColorTileCounter++;
                            }
                            else
                            {
                                Instantiate(colors[selectedColorIndex], (transform.position - new Vector3(0, 2f, 0)), Quaternion.identity);
                                selectedColorTileCounter++;
                            }


                        }
                    }



                }
            }




        }
        else
        {
            stopGeneration = true;
        }
    }
}