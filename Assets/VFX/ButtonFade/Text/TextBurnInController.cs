using UnityEngine;
using TMPro;

public class TextBurnInController : MonoBehaviour
{
    public TextMeshProUGUI tmpText;        // TextMeshPro����
    public Texture2D noiseTexture;         // ��������
    public float fadeDuration = 2f;        // ����ʱ��
    public float delay = 1f;               // �ӳ�ʱ��

    private Material textMaterial;         // ��̬�����Ĳ���
    private float fadeValue = 0f;          // ��ǰFadeֵ
    private float timer = 0f;              // ��ʱ��

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
        tmpText.fontMaterial = textMaterial;
    }

    void Update()
    {
        // ��ʱ������
        timer += Time.deltaTime;

        // ����ӳ�ʱ����δ������ֱ�ӷ��أ�����ʼ����
        if (timer < delay)
        {
            return;
        }

        // ��ʼ����Ч��
        if (fadeValue < 1f)
        {
            fadeValue += Time.deltaTime / fadeDuration;
            textMaterial.SetFloat("_Fade", fadeValue);
        }
    }
}
