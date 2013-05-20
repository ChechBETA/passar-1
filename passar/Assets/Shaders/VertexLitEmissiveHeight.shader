
Shader "Mobile/Passar/VertexLitEmissiveHeight" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
	}
	
	
	SubShader {
	    Pass {
			Tags { "LightMode" = "Vertex" }
			
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			
			//float4 _Color;
			sampler2D _MainTex;
			
			struct v2f {
			    float4  pos : SV_POSITION;
			    float4 _Color : COLOR;
			    float2  uv : TEXCOORD0;
			};
			
			float4 _MainTex_ST;
			
			v2f vert (appdata_base v)
			{
			    v2f o;
			    o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
			    o.uv = TRANSFORM_TEX (v.texcoord, _MainTex);
			    
			    //float3 worldPos = mul (UNITY_MATRIX_M, v.vertex);
			    //float col = worldPos.y * 1.0f;

			    //float col = o.pos.x / 1.0f;
			    float col = v.vertex.x * -1.0f;
			    
			    o._Color = col.rrrr;
			    
			    return o;
			}
			
			half4 frag (v2f i) : COLOR
			{
			    half4 texcol = tex2D (_MainTex, i.uv);
			    return texcol * i._Color * 1.5f;
			    //return i._Color * 1.5f;
			}
			ENDCG	    
		}
	}
	
	Fallback "Mobile/VertexLit"
} 
