Shader "Custom/TextBurnInHDRP"
{
    Properties
    {
        _MainTex ("Font Texture", 2D) = "white" {}         // ��������
        _NoiseTex ("Noise Texture", 2D) = "white" {}       // ��������
        _Fade ("Fade", Range(0,1)) = 0.0                  // �������
    }

    SubShader
    {
        Tags { "RenderPipeline" = "HDRenderPipeline" "RenderType" = "Transparent" }

        Pass
        {
            Name "ForwardLit"
            Tags { "LightMode" = "Forward" }

            Blend SrcAlpha OneMinusSrcAlpha  // ��׼͸���Ȼ��ģʽ
            Cull Off
            ZWrite Off
            
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            sampler2D _NoiseTex;
            float _Fade;
            float4 _Color;  // �� TextMeshPro ���ʴ������ɫ

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                // ���������������������
                float4 fontTexColor = tex2D(_MainTex, i.uv);   // �����������ɫ
                float noise = tex2D(_NoiseTex, i.uv).r;        // ����ֵ

                // ʹ�� Fade ֵ����������͸����
                float alpha = smoothstep(_Fade + 0.1, _Fade - 0.1, noise);

                // ��͸����Ӧ�õ���������
                float finalAlpha = fontTexColor.a * alpha;

                // ������ɫ�Ǵ������ɫ���������������ɫ
                return float4(_Color.rgb * fontTexColor.rgb, finalAlpha);
            }
            ENDHLSL
        }
    }
    FallBack "Unlit/Transparent"
}
