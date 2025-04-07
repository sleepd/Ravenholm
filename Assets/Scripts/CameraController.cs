using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] private float mouseSensitivity = 3f;
    [SerializeField] private float maxLookAngle = 80f;
    [SerializeField] private bool invertY = false;
    
    [Header("References")]
    [SerializeField] private Transform playerBody;
    
    private float xRotation = 0f;
    private bool isCursorLocked = true;
    
    void Start()
    {
        // 如果没有指定玩家身体，尝试找到父对象
        if (playerBody == null)
        {
            playerBody = transform.parent;
        }
        
        // 锁定并隐藏鼠标光标
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    void Update()
    {
        // 切换鼠标锁定状态
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleCursorLock();
        }
        
        // 如果鼠标未锁定，不处理旋转
        if (!isCursorLocked)
            return;
            
        // 获取鼠标输入
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        
        // 应用Y轴反转（如果需要）
        if (invertY)
            mouseY = -mouseY;
            
        // 计算垂直旋转（上下看）
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -maxLookAngle, maxLookAngle);
        
        // 应用旋转
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        
        // 旋转玩家身体（水平旋转）
        if (playerBody != null)
        {
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }
    
    // 切换鼠标锁定状态
    public void ToggleCursorLock()
    {
        isCursorLocked = !isCursorLocked;
        Cursor.lockState = isCursorLocked ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !isCursorLocked;
    }
    
    // 设置鼠标灵敏度
    public void SetMouseSensitivity(float sensitivity)
    {
        mouseSensitivity = sensitivity;
    }
} 