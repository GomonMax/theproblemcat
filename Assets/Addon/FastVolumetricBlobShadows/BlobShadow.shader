// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "BlobShadow"
{
	Properties
	{
		[HDR]_Color("Color", Color) = (0,0,0,1)
		_Intensity("Intensity", Float) = 1
		_Power("Power (Sharpness)", Float) = 1
		[Toggle(_ALLOWSHAPEBLENDING_ON)] _AllowShapeBlending("Allow Shape Blending", Float) = 1
		_CubeToSphereBlend("Cube To Sphere Blend", Range( 0 , 1)) = 1
		_RoundedCubeBias("Rounded Cube Bias", Range( 0 , 3)) = 2.5
		_RoundedCubePower("Rounded Cube Power", Range( 0 , 5)) = 3.5
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "ForceNoShadowCasting" = "True" "DisableBatching" = "True" "IsEmissive" = "true"  }
		Cull Front
		ZWrite Off
		ZTest GEqual
		Blend SrcAlpha OneMinusSrcAlpha
		
		CGPROGRAM
		#include "UnityCG.cginc"
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma shader_feature _ALLOWSHAPEBLENDING_ON
		#pragma surface surf Unlit keepalpha noshadow noambient novertexlights nolightmap  nodynlightmap nodirlightmap nometa noforwardadd vertex:vertexDataFunc 
		struct Input
		{
			float4 screenPos;
			float clampDepth;
			half3 vertexToFrag15_g1;
			float3 worldPos;
			half3 vertexToFrag16_g1;
		};

		uniform float4 _Color;
		UNITY_DECLARE_DEPTH_TEXTURE( _CameraDepthTexture );
		uniform float4 _CameraDepthTexture_TexelSize;
		uniform float _CubeToSphereBlend;
		uniform float _RoundedCubeBias;
		uniform float _RoundedCubePower;
		uniform float _Power;
		uniform float _Intensity;


		half CubeToSphereBlend88( half3 LocalPoint , half Blend , half Distance , half RoundedEdgeBias , half RoundedEdgePower )
		{
			#ifdef _ALLOWSHAPEBLENDING_ON
				half3 normalized = saturate(abs(LocalPoint));
				half maximized = max(normalized.x, max(normalized.y, normalized.z));
				half roundedEdges = pow((Distance - maximized) * RoundedEdgeBias, RoundedEdgePower);
				return lerp(roundedEdges + maximized, Distance, Blend);
			#else
				return Distance;
			#endif
		}


		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			o.clampDepth = -UnityObjectToViewPos( v.vertex.xyz ).z * _ProjectionParams.w;
			float3 ase_worldPos = mul( unity_ObjectToWorld, v.vertex );
			half4 appendResult3_g1 = (half4(ase_worldPos , 1.0));
			half4 transform5_g1 = mul(unity_WorldToObject,appendResult3_g1);
			half4 appendResult4_g1 = (half4(_WorldSpaceCameraPos , 1.0));
			half4 transform6_g1 = mul(unity_WorldToObject,appendResult4_g1);
			half3 temp_output_7_0_g1 = (transform6_g1).xyz;
			o.vertexToFrag15_g1 = ( (transform5_g1).xyz - temp_output_7_0_g1 );
			o.vertexToFrag16_g1 = temp_output_7_0_g1;
		}

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			o.Emission = _Color.rgb;
			float4 ase_screenPos = float4( i.screenPos.xyz , i.screenPos.w + 0.00000000001 );
			half4 ase_screenPosNorm = ase_screenPos / ase_screenPos.w;
			ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
			half clampDepth10_g1 = Linear01Depth(SAMPLE_DEPTH_TEXTURE( _CameraDepthTexture, ase_screenPosNorm.xy ));
			half3 temp_output_119_0 = ( ( ( clampDepth10_g1 / i.clampDepth ) * i.vertexToFrag15_g1 ) + i.vertexToFrag16_g1 );
			half3 LocalPoint88 = temp_output_119_0;
			half Blend88 = _CubeToSphereBlend;
			half Distance88 = distance( temp_output_119_0 , float3( 0,0,0 ) );
			half RoundedEdgeBias88 = _RoundedCubeBias;
			half RoundedEdgePower88 = _RoundedCubePower;
			half localCubeToSphereBlend88 = CubeToSphereBlend88( LocalPoint88 , Blend88 , Distance88 , RoundedEdgeBias88 , RoundedEdgePower88 );
			half smoothstepResult60 = smoothstep( 0.0 , 1.0 , ( ( 1.0 - saturate( pow( ( localCubeToSphereBlend88 * 2.0 ) , _Power ) ) ) * _Intensity ));
			o.Alpha = saturate( smoothstepResult60 );
		}

		ENDCG
	}
}
/*ASEBEGIN
Version=17500
1294;308;1906;1034;1209.087;582.5278;1.230389;True;False
Node;AmplifyShaderEditor.FunctionNode;119;-114.3189,-12.56098;Inherit;False;ViewToLocalSpace;-1;;1;a1b468c2bbc966341b50a5cb1db6ee00;0;0;1;FLOAT3;0
Node;AmplifyShaderEditor.DistanceOpNode;12;149.5737,50.28194;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;121;18.12132,220.5697;Float;False;Property;_RoundedCubePower;Rounded Cube Power;7;0;Create;False;0;0;False;0;3.5;1;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;120;1.121314,143.5697;Float;False;Property;_RoundedCubeBias;Rounded Cube Bias;6;0;Create;False;0;0;False;0;2.5;1;0;3;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;58;43.72315,-84.36367;Float;False;Property;_CubeToSphereBlend;Cube To Sphere Blend;5;0;Create;True;0;0;False;0;1;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.CustomExpressionNode;88;337.2782,-24.51973;Inherit;False;#ifdef _ALLOWSHAPEBLENDING_ON$	half3 normalized = saturate(abs(LocalPoint))@$	half maximized = max(normalized.x, max(normalized.y, normalized.z))@$	half roundedEdges = pow((Distance - maximized) * RoundedEdgeBias, RoundedEdgePower)@$	return lerp(roundedEdges + maximized, Distance, Blend)@$#else$	return Distance@$#endif;1;False;5;True;LocalPoint;FLOAT3;0,0,0;In;;Float;False;True;Blend;FLOAT;0;In;;Float;False;True;Distance;FLOAT;0;In;;Float;False;True;RoundedEdgeBias;FLOAT;0;In;;Half;False;True;RoundedEdgePower;FLOAT;0;In;;Half;False;CubeToSphereBlend;True;False;0;5;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;52;635.6404,-15.33017;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;30;541.0134,148.1952;Float;False;Property;_Power;Power (Sharpness);3;0;Create;False;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;29;769.873,28.44074;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;21;918.7153,33.14521;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;33;1063.38,119.3183;Float;False;Property;_Intensity;Intensity;2;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;23;1071.715,37.14518;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;32;1230.38,47.31828;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SmoothstepOpNode;60;1369.679,75.4459;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;22;1465.168,-110.384;Float;False;Property;_Color;Color;1;1;[HDR];Create;True;0;0;False;0;0,0,0,1;0,0,0,1;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StaticSwitch;64;420.4178,-130.7027;Inherit;False;Property;_AllowShapeBlending;Allow Shape Blending;4;0;Create;True;0;0;True;0;0;1;1;True;;Toggle;2;Key0;Key1;Create;False;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;79;1540.637,73.10341;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1687.698,-108.8609;Half;False;True;-1;2;;0;0;Unlit;BlobShadow;False;False;False;False;True;True;True;True;True;False;True;True;False;True;True;True;False;False;False;False;False;Front;2;False;-1;4;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;False;0;True;Transparent;;Transparent;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;12;0;119;0
WireConnection;88;0;119;0
WireConnection;88;1;58;0
WireConnection;88;2;12;0
WireConnection;88;3;120;0
WireConnection;88;4;121;0
WireConnection;52;0;88;0
WireConnection;29;0;52;0
WireConnection;29;1;30;0
WireConnection;21;0;29;0
WireConnection;23;0;21;0
WireConnection;32;0;23;0
WireConnection;32;1;33;0
WireConnection;60;0;32;0
WireConnection;79;0;60;0
WireConnection;0;2;22;0
WireConnection;0;9;79;0
ASEEND*/
//CHKSM=3E77CD13E4B4471858A7C01066F6AC3AEFB5F544