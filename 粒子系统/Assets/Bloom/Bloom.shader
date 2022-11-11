Shader "MyShader/Bloom"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _BloomTex("BloomTex", 2D) = "white" {}
        _BloomWidth("_BloomWidth", Vector) = (0, 0, 0, 0)
        _BloomStrength("_BloomStrength", Range(0, 2)) = 0
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always
        Tags{ "queue" = "transparent" }

        Pass  //获取亮度贴图
        {
            blend srcalpha oneminussrcalpha
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

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

            sampler2D _MainTex;
            float4 _BloomWidth;
            float _BloomStrength;

            fixed4 frag(v2f i) : SV_Target
            {
                float4 col = tex2D(_MainTex, i.uv);
                float luminance = 0.2125 * col.r + 0.7154 * col.g + 0.0721 * col.b;
                if (luminance > 0.07) {
                    col.rgb *= luminance;
                    col = float4(pow(col.r, 0.5), pow(col.g, 0.5), pow(col.b, 0.5), 1);
                }
                else {
                    col = float4(0, 0, 0, 1);
                }
                if (col.r > 0.8) {
                    col.rgb = 0.8;
                }
                return col;
            }
            ENDCG
        }

        Pass  //高斯模糊
        {
            blend srcalpha oneminussrcalpha
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

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

            sampler2D _MainTex;
            float4 _BloomWidth;
            float _BloomStrength;

            fixed4 frag(v2f i) : SV_Target
            {
                float4 col = float4(0, 0, 0, 1);
                float b[9] = { 0.05,0.1,0.05,0.1,0.4,0.1,0.05,0.1,0.05 };
                for (int u = 0; u < 3; u++) {
                    for (int j = 0; j < 3; j++) {
                        col += tex2D(_MainTex, i.uv + float2((u - 1) * _BloomWidth.x, (j - 1) * _BloomWidth.y)) * b[u * 3 + j];
                    }
                }
                col.a = 1;
                return col;
            }
            ENDCG
        }

        Pass  //均值模糊
        {
            blend srcalpha oneminussrcalpha
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

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

            sampler2D _MainTex;
            float4 _BloomWidth;
            float _BloomStrength;

            fixed4 frag(v2f i) : SV_Target
            {
                float4 col = float4(0, 0, 0, 1);
                for (int u = 0; u < 3; u++) {
                    for (int j = 0; j < 3; j++) {
                        col += tex2D(_MainTex, i.uv + float2((u - 1) * _BloomWidth.x, (j - 1) * _BloomWidth.y) / 2) / 9;
                    }
                }
                col.a = 1;
                return col;
            }
            ENDCG
        }

        Pass  //最终输出（叠加）
        {
            blend srcalpha oneminussrcalpha
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            sampler2D _BloomTex;
            float _BloomWidth;
            float _BloomStrength;

            fixed4 frag (v2f i) : SV_Target
            {
                float4 col = tex2D(_MainTex, i.uv);
                float4 col0 = tex2D(_BloomTex, i.uv);
                return col * (0.7 + 0.3 * (1 - _BloomStrength)) + col0 * _BloomStrength * 1.2;
            }
            ENDCG
        }
    }
}
