Shader "Custom/wallchanger" {
	Properties {
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0

		_RandomColor ("Random Color (Change initiated by collision with object", Color) = (1,1,1,1)
		[NoScaleOffset] _HMap ("Height Map", 2D) "gray" = {}
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		#pragma surface surf Standard fullforwardshadows
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _HMap;

		struct Input {
			float2 uv_MainTex;
			float4 worldPos;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _RandomColor;


		void surf (Input IN, inout SurfaceOutputStandard o) {
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _RandomColor;
			o.Albedo = c.rgb;
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
