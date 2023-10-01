using DG.Tweening;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private TeamController _player;   
    [SerializeField] private TeamController _enemy;
    [SerializeField] 
    private Transform _choosePlayerCharcterTransform;      
    [SerializeField] 
    private Transform _chooseTargetTransform;    
    [SerializeField] 
    private Transform _centerTransform;
    [SerializeField] 
    private Shader _comicPanelShader;

    [SerializeField]
    private Camera _portraitCamera;    [SerializeField]
    private Camera _enemyPortraitCamera;
    
    [SerializeField]
    private Material _material;

    [SerializeField] 
    private float _fullSlideIn = 1f;   
    [SerializeField] 
    private float _partialSlideIn = 1f;

    [SerializeField] private GraphicsFormat _textureFormat;

    private RenderTexture _renderTexture;
    private RenderTexture _enemyRenderTexture;

    [SerializeField]
    private bool _isPlayerPartial = false;
    [SerializeField]
    private bool _isEnemyPartial = false;
    private static readonly int PortraitTexture = Shader.PropertyToID("_PortraitTex");
    private static readonly int SlideIn = Shader.PropertyToID("_SlideIn");
    private static readonly int EnemySlideIn = Shader.PropertyToID("_SlideInEnemy");

    private void Awake()
    {
        _renderTexture = new RenderTexture(Screen.width, Screen.height, _textureFormat, GraphicsFormat.D16_UNorm);
        _renderTexture.enableRandomWrite = true;        
        _enemyRenderTexture = new RenderTexture(Screen.width, Screen.height, _textureFormat, GraphicsFormat.D16_UNorm);
        _enemyRenderTexture.enableRandomWrite = true;
        _portraitCamera.targetTexture = _renderTexture;
        _enemyPortraitCamera.targetTexture = _enemyRenderTexture;
        _material = new Material(_material);
        _material.SetTexture(PortraitTexture, _renderTexture);
        _material.SetTexture("_EnemyPortraitTex",_enemyRenderTexture);
    }

    private void Update()
    {
        _portraitCamera.Render();
        _enemyPortraitCamera.Render();
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
    
    public void CloseUpEnemy(Fighter fighter)
    {
        float slideIn = _isEnemyPartial ? _partialSlideIn : _fullSlideIn;
        Vector3 position = _isEnemyPartial ? fighter.PartialCameraPosition.position : fighter.CameraPosition.position;      
        _enemyPortraitCamera.transform.DOMove(position, 0.5f);
        _material.DOFloat(slideIn, EnemySlideIn, 0.5f);
    }

    public void ChangeViewToPlayerChoice()
    {
        _material.DOFloat(0f, EnemySlideIn, 0.5f);
        transform.DOMove(_choosePlayerCharcterTransform.position, 0.7f);
        PlayerSetToFull(_player.CurrentSelectedFighter);
    }  
    
    public void ChangeViewToEnemyChoice()
    { 
        EnemySetToPartial(_enemy.CurrentSelectedFighter);
        PlayerSetToPartial(_player.CurrentSelectedFighter);
        transform.DOMove(_chooseTargetTransform.position, 0.7f);
    }

    public void ChangeViewToCenter()
    {
        _isPlayerPartial = false;
        _material.DOFloat(_fullSlideIn, SlideIn, 0.5f);
        transform.DOMove(_centerTransform.position, 0.7f);
    }

    public void PlayerSetToPartial(Fighter fighter)
    {
        float slideIn = _partialSlideIn;
        Vector3 position = fighter.PartialCameraPosition.position;
        _portraitCamera.transform.DOMove(position, 0.5f);
        _material.DOFloat(slideIn, SlideIn, 0.5f);
        _isPlayerPartial = true;
    }  
    
    public void EnemySetToPartial(Fighter fighter)
    {
        float slideIn = _partialSlideIn;
        Vector3 position = fighter.PartialCameraPosition.position;
        _enemyPortraitCamera.transform.DOMove(position, 0.5f);
        _material.DOFloat(slideIn, EnemySlideIn, 0.5f);
        _isEnemyPartial = true;
    }
    public void PlayerSetToFull(Fighter fighter)
    {
        float slideIn = _fullSlideIn;
        Vector3 position = fighter.CameraPosition.position;
        _portraitCamera.transform.DOMove(position, 0.5f);
        _material.DOFloat(slideIn, SlideIn, 0.5f);
        _isPlayerPartial = false;
    }  
    
    public void EnemySetToFull(Fighter fighter)
    {
        float slideIn = _fullSlideIn;
        Vector3 position = fighter.CameraPosition.position;
        _enemyPortraitCamera.transform.DOMove(position, 0.5f);
        _material.DOFloat(slideIn, EnemySlideIn, 0.5f);
        _isEnemyPartial = false;
    }

    public void SetBothToFull()
    {
        EnemySetToFull(_enemy.CurrentSelectedFighter);
        PlayerSetToFull(_player.CurrentSelectedFighter);
    }
}
