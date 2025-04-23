using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class TipsManager : MonoBehaviour
{
    [SerializeField] private UIDocument document;
    private Label tipsLabel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tipsLabel = document.rootVisualElement.Q<Label>("LabelInfomation");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowTips(string tips, float time = 5f)
    {
        tipsLabel.text = tips;
        tipsLabel.style.display = DisplayStyle.Flex;
        StartCoroutine(HideTipsCoroutine(time));
    }

    IEnumerator HideTipsCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        HideTips();
    }

    public void HideTips()
    {
        tipsLabel.style.display = DisplayStyle.None;
    }
}
