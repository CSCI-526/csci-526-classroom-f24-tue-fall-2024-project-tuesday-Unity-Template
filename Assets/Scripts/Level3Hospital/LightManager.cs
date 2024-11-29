using UnityEngine;
using UnityEngine.SceneManagement;

public class LightManager : MonoBehaviour
{
    public GameObject directionalLight; // 指向方向光对象

    private void Start()
    {
        // 在开始时根据当前场景设置光源状态
        UpdateLightStatus();
    }

    private void UpdateLightStatus()
    {
        // 根据当前场景名启用或禁用方向光
        if (SceneManager.GetActiveScene().name == "Level3") // 替换为你的场景名称
        {
            if (directionalLight != null)
            {
                directionalLight.SetActive(false); // 在 Level3 中关闭方向光
            }
        }
        else
        {
            if (directionalLight != null)
            {
                directionalLight.SetActive(true); // 在其他场景中开启方向光
            }
        }
    }

    private void OnEnable()
    {
        // 订阅场景切换事件
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // 取消订阅场景切换事件
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateLightStatus(); // 当场景加载时更新光源状态
    }
}
