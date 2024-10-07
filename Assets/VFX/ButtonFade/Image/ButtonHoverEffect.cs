using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image backgroundImage;  // 按钮背景的 Image 组件
    public Texture2D noiseTexture;  // 噪声纹理
    public float fadeDuration = 1f;  // 渐变时间

    private Material material;  // 动态材质
    private float fadeValue = 0f;  // 当前 Fade 值
    private bool isHovered = false;  // 是否处于悬停状态
    private bool isFadingIn = false;  // 是否正在渐入

    void Start()
    {
        // 确保ButtonBackground的Image组件存在
        if (backgroundImage != null)
        {
            // 创建动态材质并应用自定义Shader
            material = new Material(Shader.Find("Custom/BurnInOutShader"));

            // 将 Image 的材质设置为这个新创建的材质
            backgroundImage.material = material;

            // 为材质设置主纹理和噪声纹理
            material.SetTexture("_MainTex", backgroundImage.sprite.texture);  // 使用Image的背景纹理
            material.SetTexture("_NoiseTex", noiseTexture);
            material.SetFloat("_Fade", 0f);  // 初始Fade值
        }
    }

    void Update()
    {
        // 渐入效果
        if (isHovered && isFadingIn)
        {
            if (fadeValue < 1f)
            {
                fadeValue += Time.deltaTime / fadeDuration;
                material.SetFloat("_Fade", fadeValue);
            }
        }
        // 渐出效果
        else if (!isHovered && !isFadingIn)
        {
            if (fadeValue > 0f)
            {
                fadeValue -= Time.deltaTime / fadeDuration;
                material.SetFloat("_Fade", fadeValue);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovered = true;
        isFadingIn = true;  // 开始反向灰烬燃烧效果
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false;
        isFadingIn = false;  // 开始灰烬燃烧效果
    }
}
