// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "ASESampleShaders/TwoSideWithFace"
{
	Properties
	{
		_Cutoff( "Mask Clip Value", Float ) = 0.5
		_Mask("Mask", 2D) = "white" {}
		_FrontAlbedo("FrontAlbedo", 2D) = "white" {}
		_FrontNormalMap("FrontNormalMap", 2D) = "bump" {}
		_FrontColor("FrontColor", Color) = (1,0.6691177,0.6691177,0)
		_BackAlbedo("BackAlbedo", 2D) = "white" {}
		_BackNormalMap("BackNormalMap", 2D) = "bump" {}
		_BackColor("BackColor", Color) = (0,0,1,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "TransparentCutout"  "Queue" = "AlphaTest+0" }
		Cull Off
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
			half ASEVFace : VFACE;
		};

		uniform sampler2D _FrontNormalMap;
		uniform sampler2D _BackNormalMap;
		uniform sampler2D _FrontAlbedo;
		uniform float4 _FrontColor;
		uniform sampler2D _BackAlbedo;
		uniform float4 _BackColor;
		uniform sampler2D _Mask;
		uniform float4 _Mask_ST;
		uniform float _Cutoff = 0.5;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float3 switchResult3 = (((i.ASEVFace>0)?(UnpackNormal( tex2D( _FrontNormalMap, i.uv_texcoord ) )):(UnpackNormal( tex2D( _BackNormalMap, i.uv_texcoord ) ))));
			o.Normal = switchResult3;
			float4 switchResult2 = (((i.ASEVFace>0)?(( tex2D( _FrontAlbedo, i.uv_texcoord ) * _FrontColor )):(( tex2D( _BackAlbedo, i.uv_texcoord ) * _BackColor ))));
			o.Albedo = switchResult2.rgb;
			o.Alpha = 1;
			float2 uv_Mask = i.uv_texcoord * _Mask_ST.xy + _Mask_ST.zw;
			clip( tex2D( _Mask, uv_Mask ).a - _Cutoff );
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17101
720;75;718;261;679.2386;-123.3749;1.5;True;False
Node;AmplifyShaderEditor.CommentaryNode;12;-1271.091,-592;Inherit;False;1252.009;1229.961;Inspired by 2Side Sample by The Four Headed Cat;12;14;4;5;8;7;10;11;6;9;3;1;2;Two Sided Shader using Switch by Face;1,1,1,1;0;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;14;-1222.402,60.79997;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;8;-865,-160;Inherit;True;Property;_BackAlbedo;BackAlbedo;5;0;Create;True;0;0;False;0;b297077dae62c1944ba14cad801cddf5;b297077dae62c1944ba14cad801cddf5;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;4;-865,-528;Inherit;True;Property;_FrontAlbedo;FrontAlbedo;2;0;Create;True;0;0;False;0;c68296334e691ed45b62266cbc716628;c68296334e691ed45b62266cbc716628;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;7;-785,32;Float;False;Property;_BackColor;BackColor;7;0;Create;True;0;0;False;0;0,0,1,0;0,0,0,0;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;5;-785,-336;Float;False;Property;_FrontColor;FrontColor;4;0;Create;True;0;0;False;0;1,0.6691177,0.6691177,0;0,0,0,0;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;9;-523.1008,-105.9;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;6;-518.001,-241.8997;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;10;-825,210;Inherit;True;Property;_FrontNormalMap;FrontNormalMap;3;0;Create;True;0;0;False;0;f5453dca2ac649e4182c56a3966ad395;f5453dca2ac649e4182c56a3966ad395;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;11;-809,408;Inherit;True;Property;_BackNormalMap;BackNormalMap;6;0;Create;True;0;0;False;0;0bebe40e9ebbecc48b8e9cfea982da7e;0bebe40e9ebbecc48b8e9cfea982da7e;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SwitchByFaceNode;3;-369,208;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SwitchByFaceNode;2;-257,-176;Inherit;False;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;1;-353,336;Inherit;True;Property;_Mask;Mask;1;0;Create;True;0;0;False;0;45ea98975d00f49d4a37af20347af491;45ea98975d00f49d4a37af20347af491;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;56.8,7.000003;Float;False;True;2;ASEMaterialInspector;0;0;Standard;ASESampleShaders/TwoSideWithFace;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Masked;0.5;True;True;0;False;TransparentCutout;;AlphaTest;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;0;4;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;1;False;-1;1;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;8;1;14;0
WireConnection;4;1;14;0
WireConnection;9;0;8;0
WireConnection;9;1;7;0
WireConnection;6;0;4;0
WireConnection;6;1;5;0
WireConnection;10;1;14;0
WireConnection;11;1;14;0
WireConnection;3;0;10;0
WireConnection;3;1;11;0
WireConnection;2;0;6;0
WireConnection;2;1;9;0
WireConnection;0;0;2;0
WireConnection;0;1;3;0
WireConnection;0;10;1;4
ASEEND*/
//CHKSM=97306DF547E75F4DAA6F6CEF18BD33E4446E12E6