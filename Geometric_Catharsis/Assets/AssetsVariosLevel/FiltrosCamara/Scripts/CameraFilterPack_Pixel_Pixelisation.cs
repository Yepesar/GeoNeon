﻿////////////////////////////////////////////////////////////////////////////////////
//  CameraFilterPack v2.0 - by VETASOFT 2015 //////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[AddComponentMenu ("Camera Filter Pack/Pixel/Pixelisation")]
public class CameraFilterPack_Pixel_Pixelisation : MonoBehaviour {


    //parte mieluda

    public float tiempoEfectoIni = 2f;
    private float elapsedTime = 0;
    private float valorPixel;
    public float prueba;

    //aqui acaba

    #region Variables
    public Shader SCShader;
	[Range(0.6f, 120)]
	public float _Pixelisation = 0f;
	[Range(0.6f, 120)]
	public float _SizeX = 1f;
	[Range(0.6f, 120)]
	public float _SizeY = 1f;
	private Material SCMaterial;

	public static float ChangePixel;
	public static float ChangePixelX;
	public static float ChangePixelY;

	#endregion
	
	#region Properties
	Material material
	{
		get
		{
			if(SCMaterial == null)
			{
				SCMaterial = new Material(SCShader);
				SCMaterial.hideFlags = HideFlags.HideAndDontSave;	
			}
			return SCMaterial;
		}
	}
	#endregion
	void Start () 
	{
		ChangePixel = _Pixelisation;
		ChangePixelX = _SizeX;
		ChangePixelY = _SizeY;
		SCShader = Shader.Find("CameraFilterPack/Pixel_Pixelisation");

		if(!SystemInfo.supportsImageEffects)
		{
			enabled = false;
			return;
		}
	}
	
	void OnRenderImage (RenderTexture sourceTexture, RenderTexture destTexture)
	{
		if(SCShader != null)
		{
			material.SetFloat("_Val", _Pixelisation);
			material.SetFloat("_Val2", _SizeX);
			material.SetFloat("_Val3", _SizeY);

			Graphics.Blit(sourceTexture, destTexture, material);
		}
		else
		{
			Graphics.Blit(sourceTexture, destTexture);	
		}
		
		
	}
void OnValidate()
{
		ChangePixel=_Pixelisation;
		ChangePixelX=_SizeX;
		ChangePixelY=_SizeY;

	
}
	// Update is called once per frame
	void Update () 
	{


        //parte mieluda
        if (elapsedTime <= tiempoEfectoIni)
        {
            elapsedTime += Time.deltaTime;

            valorPixel = ((1f - (elapsedTime / tiempoEfectoIni)) * 64f) + 0.6f;

            ChangePixel = valorPixel;
            
        }
        //aquí acaba
        if (Application.isPlaying)
		{
			_Pixelisation = ChangePixel;
			_SizeX = ChangePixelX;
			_SizeY = ChangePixelY;
		}
		#if UNITY_EDITOR
		if (Application.isPlaying!=true)
		{
			SCShader = Shader.Find("CameraFilterPack/Pixel_Pixelisation");
	
		}
		#endif

	}
	
	void OnDisable ()
	{
		if(SCMaterial)
		{
			DestroyImmediate(SCMaterial);	
		}
		
	}
	
	
}