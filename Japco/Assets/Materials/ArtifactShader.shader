// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/ArtifactShader"
{
    Properties
    {
        // Color property for material inspector, default to white
        _Color ("Main Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            struct input
            {
                float2 uv : TEXCOORD0;
                float4 vertex : POSITION;
            };
            
            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION; // clip space position
            };
            
            v2f vert (input i)
            {
                v2f o;
                o.uv = i.uv;
                o.vertex = UnityObjectToClipPos(i.vertex);
                return o;
            }
            
            // color from the material
            fixed4 color = fixed4(1.0, 1.0, 0.0, 1.0);
/*
			float noise(float2 st) {
			    float2 i = floor(st);
			    float2 f = frac(st);
			
			    float2 u = f*f*(3.0-2.0*f);
			
			    return mix( mix( dot( rand2(i + fixed2(0.0,0.0) ), f - fixed2(0.0,0.0) ),
			                     dot( random2(i + vec2(1.0,0.0) ), f - vec2(1.0,0.0) ), u.x),
			                mix( dot( random2(i + vec2(0.0,1.0) ), f - vec2(0.0,1.0) ),
			                     dot( random2(i + vec2(1.0,1.0) ), f - vec2(1.0,1.0) ), u.x), u.y);
			}
*/
            
            fixed4 frag (v2f i) : SV_Target
            {/*
                fixed2 ab = frac(uv * 16.0);
                fixed2 cd = frac(uv * 8.0);
                fixed2 ef = frac(uv * 4.0);
                fixed2 gh = frac(uv * 2.0);
                fixed2 ij = uv;*/
                fixed4 o;
                o.r = sin(i.uv.x);
                o.g = cos(i.uv.x) * 0.5 + sin(i.uv.y) * 0.5;
                o.b = tan(i.uv.x) * 0.5 + cos(i.uv.y) * 0.5;
                o.a = 1.0;
                return o;
            }
            ENDCG
        }
    }
}
