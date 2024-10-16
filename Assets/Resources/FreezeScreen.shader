Shader "Unlit/FreezeScreen"
{
    Properties
    {
        _Ice ("Ice Texture", 2D) = "white" {}
        _Aperture ("Aperture", Range(0,1)) = 0.15
    }
    SubShader
    {
        Tags { "Queue" = "AlphaTest"}
        Blend SrcAlpha OneMinusSrcAlpha
        
        LOD 100
        Pass
        {
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            float _Aperture;
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float2 uv2 : TEXCOORD1;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float2 uv2 : TEXCOORD1;
                float4 vertex : SV_POSITION;
            };
            
            sampler2D _Ice;
            float4 _Ice_ST;

            float4 MakeBlend (float2 uv)
            {
                float4 leftside = uv.x;
                leftside*=uv.y;
                float4 rightside = 1-uv.x;
                rightside*=1-uv.y;
                float4 finalblend = leftside;
                finalblend *=rightside;
                // cobine left and right side
                return pow(finalblend, _Aperture);
            }
            v2f vert (appdata v)
            {

                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _Ice);
                o.uv2 = v.uv2;
                return o;
            }
            
            float4 frag (v2f i) : SV_Target
            {
                float4 IceColor = tex2D(_Ice, i.uv);
                // color with alpha 0 is transparent
                
                float4 blendFactor = MakeBlend(i.uv2);
                float4 transparent = float4(0,0,0,0);
                float4 Image = lerp(IceColor, transparent, blendFactor);
                
                return saturate(Image);
            }
            ENDCG
        }
    }
}
