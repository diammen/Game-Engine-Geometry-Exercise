Shader "Custom/Eye Base" {
	Properties
	{
		_DarkenInnerLineColor("Darken Inner Line Color", Range(0, 1)) = 0.2
		_Threshold("Cutout threshold", Range(0,1)) = 0.1
		_Softness("Cutout softness", Range(0,0.5)) = 0.0

		_MainTex ("Texture", 2D) = "white" {}
		_ShadedTex ("Shaded Texture", 2D) = "white" {}
		_CombinedTex ("Combined", 2D) = "white" {}
	}
	SubShader
	{
		Tags {"Queue"="Geometry+1" "RenderType"="Overlay"}
		ZWrite Off
		BLend SrcAlpha OneMinusSrcAlpha
		LOD 200
		CGPROGRAM

		#pragma surface surf Custom alpha

		sampler2D _MainTex;
		sampler2D _ShadedTex;
		sampler2D _CombinedTex;

		float _DarkenInnerLineColor;
		float _Threshold;
		float _Softness;

		struct Input
		{
			float2 uv_MainTex;
			float3 vertexColor;
		};

		struct SurfaceOutputCustom
		{
			fixed3 Albedo;
			fixed3 Normal;
			fixed3 Emission;
			fixed Alpha;

			half3 BrightColor;
			half3 ShadowColor;
			half3 InnerLineColor;
			half ShadowThreshold;

			half SpecularIntensity;
			half SpecularSize;

		};

		// void surf(Input i, inout SurfaceOutput o)
		// {
		// 	fixed4 col = tex2D(_MainTex, i.uv_MainTex);
		// 	o.Albedo = col.rgb;
		// 	o.Alpha = col.a;
		// }

		float4 LightingCustom(SurfaceOutputCustom s, float3 lightDir, float atten)
		{
			float towardsLight = dot(lightDir, s.Normal);

			float4 col = float4(0, 0, 0, 1);

			towardsLight -= s.ShadowThreshold;
			half specStrength = s.SpecularIntensity;
			// if the vertex is facing away from the light
			if (towardsLight < 0)
			{
				if (towardsLight < - s.SpecularSize - 0.5f && specStrength <= 0.5f)
				{
					col.rgb = s.ShadowColor * (0.5f + specStrength);
				}
				else 
				{
					col.rgb = s.ShadowColor;
				}
			}
			else
			{
				if (s.SpecularSize < 1 && towardsLight * 1.8f > s.SpecularSize && specStrength >= 0.5f)
				{
					col.rgb = s.BrightColor * (0.5f + specStrength);
				}
				else
				{
					col.rgb = s.BrightColor;
				}
			}

			col.rgb *= s.InnerLineColor * _LightColor0.rgb;

			return col;
		}

		void surf(Input i, inout SurfaceOutputCustom o)
		{
			fixed4 col = tex2D(_MainTex, i.uv_MainTex);
			fixed4 shade = tex2D(_ShadedTex, i.uv_MainTex);
			fixed4 comb = tex2D(_CombinedTex, i.uv_MainTex);

			o.BrightColor = col.rgb;
			o.ShadowColor = col.rgb * shade.rgb;

			float clampedLineColor = comb.a;
			if (clampedLineColor < _DarkenInnerLineColor)
				clampedLineColor = _DarkenInnerLineColor;

			o.InnerLineColor = half3(clampedLineColor, clampedLineColor, clampedLineColor);

			o.ShadowThreshold = comb.g;
			o.ShadowThreshold *= i.vertexColor.r;
			o.ShadowThreshold = 1 - o.ShadowThreshold;

			o.SpecularIntensity = comb.r;
			o.SpecularSize = 1-comb.b;

			col *= shade;

			o.Albedo = col.rgb;
			o.Alpha = col.a;
		}
		ENDCG

	}
	FallBack "Diffuse"
}