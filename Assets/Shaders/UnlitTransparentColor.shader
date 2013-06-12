Shader "Mobile/Passar/UnlitTransparent Color" {
Properties {
	_Color ("Main color", Color) = (1, 1, 1, 1)
	_MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
}

Category {
	Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
	ZWrite Off
	Alphatest Greater 0
	Blend SrcAlpha OneMinusSrcAlpha 
	SubShader {
		Material {
			Diffuse [_Color]
			Ambient (1,1,1,1)
		}
		Pass {
			ColorMaterial AmbientAndDiffuse
			Fog { Mode Off }
			Lighting Off
			
			SetTexture [_MainTex] {
                constantColor [_Color]
                combine constant lerp(texture) previous
            }
        	SetTexture [_MainTex] {
	            Combine texture * previous
	        }
		}
	} 
}
}