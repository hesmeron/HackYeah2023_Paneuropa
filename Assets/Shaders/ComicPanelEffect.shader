Shader "Hidden/ComicPanelEffect"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _PortraitTex ("Portrait Texture", 2D) = "black" {}
        _XCoefficient ("xCoefficient", float) = 1
        _Offset ("Offset", float) = 0.5
        _SlideIn ("SlideIn", Range(0,1)) = 0.5
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
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
            sampler2D _PortraitTex;
            half _XCoefficient;
            half _Offset;
            half _SlideIn;

            fixed4 frag (v2f i) : SV_Target
            {
                half offset = 0.5f - _Offset;
                half slideIn = 1 - _SlideIn;
                half x = (i.uv.x  + offset + slideIn) * _XCoefficient;
                half y = i.uv.y;


                if(x < y)
                {
                    fixed4 portrait = tex2D(_PortraitTex, i.uv + float2( slideIn, 0));
                    return portrait;
                }
                
                fixed4 src = tex2D(_MainTex, i.uv);
                return src;;

            }
            ENDCG
        }
    }
}
