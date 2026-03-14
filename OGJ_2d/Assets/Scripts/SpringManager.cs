using UnityEngine;

public class SpringManager : MonoBehaviour
{

    void Update()
    {
        simulateSprings();
    }

    private void simulateSprings()
    {
        foreach (GameObject spring in GameObject.FindGameObjectsWithTag("spring"))
        {
            spring.GetComponent<Spring>().UpdateString();
        }
    }
}
