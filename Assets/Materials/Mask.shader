Shader "Custom/Mask"
{

  SubShader
  {
	 Tags {"Queue" = "AlphaTest"}	 

  Pass
     {
		 Blend Zero One 
     }
  }

}
