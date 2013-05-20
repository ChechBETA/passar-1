Shader "Mobile/Passar/UnlitTransparent" {
Properties {
	_MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
}

Category {
	Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
	ZWrite Off
	Alphatest Greater 0
	Blend SrcAlpha OneMinusSrcAlpha 
	SubShader {
		Material {
			Diffuse (1,1,1,1)
			Ambient (1,1,1,1)
		}
		Pass {
			ColorMaterial AmbientAndDiffuse
			Fog { Mode Off }
			Lighting Off
        	SetTexture [_MainTex] {
	            Combine texture * primary, texture * primary
	        }
		}
	} 
}
}