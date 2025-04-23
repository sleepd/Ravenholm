using UnityEngine;

public class ShowTips : MonoBehaviour
{
    [SerializeField] private string tips;
    [SerializeField] private float time = 5f;
    private bool isTriggered = false;
    private TipsManager tipsManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tipsManager = GameObject.Find("HUD").GetComponent<TipsManager>();
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (!isTriggered)
        {
            tipsManager.ShowTips(tips, time);
            isTriggered = true;
        }
    }
}
