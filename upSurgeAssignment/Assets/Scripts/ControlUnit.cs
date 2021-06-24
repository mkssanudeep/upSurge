using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ControlUnit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        print("Regenerate!"); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void LoadGenerate()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
