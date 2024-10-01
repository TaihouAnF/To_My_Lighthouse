Shader "Custom/TextBurnInHDRP"
{
    Properties
    {
        _MainTex ("Font Texture", 2D) = "white" {}      // ��������
        _NoiseTex ("Noise Texture", 2D) = "white" {}    // �����������ڿ�������Ч��
        _Fade ("Fade", Range(0,1)) = 0.0               // ���ƽ������ԵĲ���
    }

    SubShader
    {
        Tags { "RenderPipeline" = "HDRenderPipeline" "RenderType" = "Transparent" }
        
        Pass
        {
            Name "ForwardLit"
            Tags { "LightMode" = "Forward" }
            
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite Off
            
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            sampler2D _NoiseTex;
            float _Fade;

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
                float4 fontColor = tex2D(_MainTex, i.uv);
                float noise = tex2D(_NoiseTex, i.uv).r;

                // ͨ�����������Fadeֵ����͸����
                float alpha = smoothstep(_Fade + 0.01, _Fade - 0.01, noise);

                // �������͸��������������Ŀ�������
                alpha *= fontColor.a;

                // ���ش���͸���ȵ���ɫ
                return float4(fontColor.rgb, alpha);
            }
            ENDHLSL
        }
    }
    FallBack "Unlit/Transparent"
}
