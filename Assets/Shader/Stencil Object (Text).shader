Shader "Custom/Stencil Object (Text)"
  {  
     Properties
     {
         [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
         _Color ("Tint", Color) = (1,1,1,1)
         
         // required for UI.Mask
         _StencilComp ("Stencil Comparison", Float) = 8
         _Stencil ("Stencil ID", Float) = 0
         _StencilOp ("Stencil Operation", Float) = 0
         _StencilWriteMask ("Stencil Write Mask", Float) = 255
         _StencilReadMask ("Stencil Read Mask", Float) = 255
         _ColorMask ("Color Mask", Float) = 15
		 _AlphaTex ("Alpha mask (R)", 2D) = "white" {}
     }
     SubShader
     {
         Tags 
         { 
        	"QUEUE"="Transparent" "IGNOREPROJECTOR"="true" "RenderType"="Transparent" "PreviewType"="Plane" 
         }
		 Blend SrcAlpha OneMinusSrcAlpha
         ZWrite Off
         // required for UI.Mask
         Stencil
         {
             Ref [_Stencil]
             Comp [_StencilComp]
             Pass [_StencilOp] 
             ReadMask [_StencilReadMask]
             WriteMask [_StencilWriteMask]
         }
          ColorMask [_ColorMask]
         
         Pass
         {
            SetTexture[_Alpha] {
                Combine previous, texture
            }
			SetTexture[_MainTex] {
                Combine previous, texture
            }
			 // ...
         }
     }
  }