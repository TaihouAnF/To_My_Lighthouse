using UnityEngine;
using TMPro;

public class TextBurnInController : MonoBehaviour
{
    public TextMeshProUGUI tmpText;        // TextMeshPro����
    public Texture2D noiseTexture;         // ��������
    public float fadeDuration = 2f;        // ����ʱ��

    private Material textMaterial;         // ��̬�����Ĳ���
    private float fadeValue = 0f;          // ��ǰFadeֵ

    void Start()
    {
        if (tmpText == null)
        {
            Debug.LogError("TextMeshProUGUI reference is not assigned!");
            return;
        }

        // �������ʲ�Ӧ���Զ���Shader
        Shader shader = Shader.Find("Custom/TextBurnInHDRP");
        if (shader == null)
        {
            Debug.LogError("Shader not found!");
            return;
        }

        textMaterial = new Material(shader);

        // ��ȡ��������
        Texture fontTexture = tmpText.fontSharedMaterial?.GetTexture("_MainTex");
        if (fontTexture == null)
        {
            Debug.LogError("Font texture not found in the TextMeshPro material!");
            return;
        }

        // ���ò��ʵ��������������
        textMaterial.SetTexture("_MainTex", fontTexture);
        textMaterial.SetTexture("_NoiseTex", noiseTexture);
        textMaterial.SetFloat("_Fade", 0f);  // ��ʼΪ��ȫ͸��

        // ������Ӧ�õ�TextMeshPro
        tmpText.fontSharedMaterial = textMaterial;
    }

    void Update()
    {
        // ����ʽ����Fadeֵ��ʵ��������Ч��
        if (fadeValue < 1f)
        {
            fadeValue += Time.deltaTime / fadeDuration;
            textMaterial.SetFloat("_Fade", fadeValue);
        }
    }
}
