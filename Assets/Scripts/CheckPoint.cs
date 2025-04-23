using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private int checkPointIndex;
    private bool isTriggered = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (!isTriggered)
        {
            GameManager.Instance.GetCheckPoint(checkPointIndex);
            isTriggered = true;
        }
    }
}
