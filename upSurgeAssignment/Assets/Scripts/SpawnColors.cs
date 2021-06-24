using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnColors : MonoBehaviour
{
    public LayerMask whatIsRoom;
    public float waitTime;
    public GridManager gridMang;
    bool hasColor;

    public GameObject[] notselectedcolors;

    private void Update()
    {
        Collider2D colorDetection = Physics2D.OverlapCircle(transform.position, 0.5f, whatIsRoom);
        if(gridMang.stopGeneration == true)
        {
            if (colorDetection != null)
            {
                //Spawn Random Color
                hasColor = true;
            }

            if (waitTime <= 0)
            {
                if (hasColor == false)
                {
                    int rand = Random.Range(0, notselectedcolors.Length); // make the zero one to make sure the other TWO NON SELECTED COLORS spawn.

                    Instantiate(notselectedcolors[rand], transform.position, Quaternion.identity);
                }
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
        
            
            
            
            
            
        

    }
}
