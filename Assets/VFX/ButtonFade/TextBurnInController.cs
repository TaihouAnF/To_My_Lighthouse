using UnityEngine;
using TMPro;

public class TextBurnInController : MonoBehaviour
{
    public TextMeshProUGUI tmpText;        // TextMeshPro对象
    public Texture2D noiseTexture;         // 噪声纹理
    public float fadeDuration = 2f;        // 渐入时间

    private Material textMaterial;         // 动态创建的材质
    private float fadeValue = 0f;          // 当前Fade值

    void Start()
    {
        if (tmpText == null)
        {
            Debug.LogError("TextMeshProUGUI reference is not assigned!");
            return;
        }

        // 创建材质并应用自定义Shader
        Shader shader = Shader.Find("Custom/TextBurnInHDRP");
        if (shader == null)
        {
            Debug.LogError("Shader not found!");
            return;
        }

        textMaterial = new Material(shader);

        // 获取字体纹理
        Texture fontTexture = tmpText.fontSharedMaterial?.GetTexture("_MainTex");
        if (fontTexture == null)
        {
            Debug.LogError("Font texture not found in the TextMeshPro material!");
            return;
        }

        // 设置材质的字体和噪声纹理
        textMaterial.SetTexture("_MainTex", fontTexture);
        textMaterial.SetTexture("_NoiseTex", noiseTexture);
        textMaterial.SetFloat("_Fade", 0f);  // 初始为完全透明

        // 将材质应用到TextMeshPro
        tmpText.fontSharedMaterial = textMaterial;
    }

    void Update()
    {
        // 渐进式增加Fade值，实现逐渐显现效果
        if (fadeValue < 1f)
        {
            fadeValue += Time.deltaTime / fadeDuration;
            textMaterial.SetFloat("_Fade", fadeValue);
        }
    }
}
