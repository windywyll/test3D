Shader "Custom/Boid" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_BumpTex("Normal", 2D) = "bump" {}
		_LavaTex("Albedo (RGB)", 2D) = "white" {}
		_PerlinTex("perlin", 2D) = "bump" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_Range("Range", Range(0,1)) = 0.0
	}
	SubShader {
			Tags{ "RenderType" = "Opaque" }
			LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _BumpTex;
		sampler2D _LavaTex;
		sampler2D _PerlinTex;

		struct Input {
			float2 uv_MainTex;
			float2 uv_LavaTex;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;
		float _Range;

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			fixed4 lava = tex2D(_LavaTex, IN.uv_LavaTex);
			float luminance = Luminance(tex2D(_PerlinTex, IN.uv_LavaTex));
			float isLava = step(luminance, _Range);
			o.Albedo = lerp(c.rgb, lava.rgb, isLava);

			o.Emission = lava.rgb * isLava;

			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;

			o.Normal = UnpackNormal(tex2D(_BumpTex, IN.uv_MainTex));

			o.Alpha = c.a;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
