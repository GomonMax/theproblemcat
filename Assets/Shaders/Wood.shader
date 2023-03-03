Shader "LeafCutoutShader"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _Color("Color", Color) = (1,1,1,1)
        _Cutoff("Transparency Cutoff", Range(0, 1)) = 0.5
    }
        SubShader
        {
            Tags { "LightMode" = "ForwardBase" "IGNOREPROJECTOR" = "true"  "RenderType" = "Opaque" "Queue" = "AlphaTest" "DisableBatching" = "LodFading" }
            LOD 100

            Pass
            {
                Cull Off
                ZWrite On

                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #pragma multi_compile_instancing
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"
            #include "UnityLightingCommon.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                float4 color : COLOR0;
                UNITY_VERTEX_INPUT_INSTANCE_ID // necessary only if you want to access instanced properties in fragment Shader.
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Cutoff;

            UNITY_INSTANCING_BUFFER_START(Props)
            UNITY_DEFINE_INSTANCED_PROP(float4, _Color)
            UNITY_INSTANCING_BUFFER_END(Props)


            v2f vert(appdata v)
            {
                v2f o;

                UNITY_INITIALIZE_OUTPUT(v2f, o);
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_TRANSFER_INSTANCE_ID(v, o); // necessary only if you want to access instanced properties in the fragment Shader.

                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);

                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // sample the texture
                float4 col = tex2D(_MainTex, i.uv);
                //Tint out texture by our tint color, ensuring our Alpha value is also adjusted by our Tint's alpha before we clip()
                col *= UNITY_ACCESS_INSTANCED_PROP(Props, _Color) * _LightColor0;

                clip(col.a - _Cutoff); //If our color's alpha value, minus our _Cutoff value, is less than 0, 'discard' this pixel.
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                UNITY_SETUP_INSTANCE_ID(i); // necessary only if any instanced properties are going to be accessed in the fragment Shader.
                return col;
            }
            ENDCG
        }
        }
}