Shader "Custom/LiquidTest" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		[NoScaleOffset] _FlowMap ("Flow (RG)", 2D) = "black" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows
		#pragma target gl4.1
		

		#if !defined(FLOW_INCLUDED)
		#define FLOW_INCLUDED

		float FlowUV (float2 uv, float2 flowVector, float time){
		    float pro = frac(time);
		    return uv - flowVector * pro;
		}

		#endif

		// Flip sampling of the Texture: 
		// The main Texture texel size will have negative Y).	
		/*#if UNITY_UV_STARTS_AT_TOP
		if (_MainTex_TexelSize.y < 0)
		        uv.y = 1-uv.y;
		#endif*/

		sampler2D _MainTex, _FlowMap;

		struct Input {
			float2 uv_MainTex;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		void surf (Input IN, inout SurfaceOutputStandard o) {
			float2 flowVector = tex2D(_FlowMap, IN.uv_MainTex).rg*2-1;
			float2 uv = FlowUV(IN.uv_MainTex, flowVector, _Time.y);
			fixed4 c = tex2D (_MainTex, uv) * _Color;
			o.Albedo = c.rgb;
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
