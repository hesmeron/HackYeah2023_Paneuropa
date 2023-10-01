using DG.Tweening;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class CameraManager : MonoBehaviour
{
    [SerializeField] 
    private Transform _choosePlayerCharcterTransform;    
    [SerializeField] 
    private Transform _centerTransform;
    [SerializeField] 
    private Shader _comicPanelShader;

    [SerializeField]
    private Camera _portraitCamera;
    
    [SerializeField]
    private Material _material;

    [SerializeField] 
    private float _fullSlideIn = 1f;   
    [SerializeField] 
    private float _partialSlideIn = 1f;

    [SerializeField] private GraphicsFormat _textureFormat;

    private RenderTexture _renderTexture;

    private bool _isPlayerPartial = false;
    private static readonly int PortraitTexture = Shader.PropertyToID("_PortraitTex");
    private static readonly int SlideIn = Shader.PropertyToID("_SlideIn");

    private void Awake()
    {
        _renderTexture = new RenderTexture(Screen.width, Screen.height, _textureFormat, GraphicsFormat.D16_UNorm);
        _renderTexture.enableRandomWrite = true;
        _portraitCamera.targetTexture = _renderTexture;
        _material = new Material(_material);
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

    public void CloseUp(Fighter fighter)
    {
        float slideIn = _isPlayerPartial ? _partialSlideIn : _fullSlideIn;
        Vector3 position = _isPlayerPartial ? fighter.PartialCameraPosition.position : fighter.CameraPosition.position;
        _portraitCamera.transform.DOMove(position, 0.5f);
        _material.DOFloat(slideIn, SlideIn, 0.5f);
    }

    public void ChangeViewToPlayerChoice()
    {
        transform.DOMove(_choosePlayerCharcterTransform.position, 0.7f);
        _material.DOFloat(_partialSlideIn, SlideIn, 0.5f);
        _isPlayerPartial = true;
    }

    public void ChangeViewToCenter()
    {
        _isPlayerPartial = false;
        _material.DOFloat(_fullSlideIn, SlideIn, 0.5f);
        transform.DOMove(_centerTransform.position, 0.7f);
    }
}
