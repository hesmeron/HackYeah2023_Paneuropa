using System;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class CameraManager : MonoBehaviour
{
    [SerializeField] 
    private Shader _comicPanelShader;

    [SerializeField]
    private Camera _portraitCamera;
    
    [SerializeField]
    private Material _material;

    [SerializeField] private GraphicsFormat _textureFormat;

    private RenderTexture _renderTexture;
    private static readonly int PortraitTexture = Shader.PropertyToID("_PortraitTex");

    private void Awake()
    {
        _renderTexture = new RenderTexture(Screen.width, Screen.height, _textureFormat, GraphicsFormat.D16_UNorm);
        _renderTexture.enableRandomWrite = true;
        _portraitCamera.targetTexture = _renderTexture;
        if (!_material)
        {
            _material = new Material(_comicPanelShader);
        }
        _material.SetTexture(PortraitTexture, _renderTexture);
    }

    private void Update()
    {
        _portraitCamera.Render();
    }

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src, dest, _material);
    }
}
