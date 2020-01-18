// Amplify Shader Editor - Visual Shader Editing Tool
// Copyright (c) Amplify Creations, Lda <info@amplify.pt>
#if UNITY_POST_PROCESSING_STACK_V2
using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
[PostProcess( typeof( FogScreenEffectPPSRenderer ), PostProcessEvent.AfterStack, "FogScreenEffect", true )]
public sealed class FogScreenEffectPPSSettings : PostProcessEffectSettings
{
	[Tooltip( "Screen" )]
	public TextureParameter _MainTex = new TextureParameter {  };
	[Tooltip( "Distance" )]
	public FloatParameter _Distance = new FloatParameter { value = 0f };
	[Tooltip( "FallOff" )]
	public FloatParameter _FallOff = new FloatParameter { value = 3f };
}

public sealed class FogScreenEffectPPSRenderer : PostProcessEffectRenderer<FogScreenEffectPPSSettings>
{
	public override void Render( PostProcessRenderContext context )
	{
		var sheet = context.propertySheets.Get( Shader.Find( "FogScreenEffect" ) );
		if(settings._MainTex.value != null) sheet.properties.SetTexture( "_MainTex", settings._MainTex );
		sheet.properties.SetFloat( "_Distance", settings._Distance );
		sheet.properties.SetFloat( "_FallOff", settings._FallOff );
		context.command.BlitFullscreenTriangle( context.source, context.destination, sheet, 0 );
	}
}
#endif
