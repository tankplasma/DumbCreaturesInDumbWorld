Shader"Custom/CutoutShader" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _Cutoff ("Cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {"Queue"="Transparent" "RenderType"="TransparentCutout" }
            LOD 100

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            
            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 texcoord : TEXCOORD0;
            };

            sampler2D _MainTex;
            float _Cutoff;

            v2f vert(appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.texcoord = v.texcoord;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.texcoord);
                clip(col.a - _Cutoff);
                return col;
            }
            ENDCG
        }
    }
}
