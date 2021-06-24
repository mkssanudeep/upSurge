using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnColors : MonoBehaviour
{
    public LayerMask whatIsRoom;
    public GridManager gridMang;
    private void Update()
    {
        Collider2D colorDetection = Physics2D.OverlapCircle(transform.position, 0.5f, whatIsRoom);
        if(colorDetection == null && gridMang.stopGeneration ==true)
        {
            //Spawn Random Color
            
            
            int rand = Random.Range(0, gridMang.colors.Length); // make the zero one to make sure the other TWO NON SELECTED COLORS spawn.
            Instantiate(gridMang.colors[rand], transform.position, Quaternion.identity);
            
            
            Destroy(gameObject);
        }

    }
}
