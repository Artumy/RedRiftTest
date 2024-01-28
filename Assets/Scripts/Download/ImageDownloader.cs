using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class ImageDownloader : MonoBehaviour
{
    [SerializeField] private DownloadedImagesData _downloadedImagesData;
    [SerializeField] private string _url;
    [SerializeField] private int _minAmountImages;
    [SerializeField] private int _maxAmountImages;

    private int _amountOfImages;

    private void Awake()
    {
        _amountOfImages = Random.Range(_minAmountImages, _maxAmountImages + 1);
    }

    private void Start()
    {
        StartCoroutine(Download());
    }

    private IEnumerator Download()
    {
        _downloadedImagesData.Clear();
        
        for (int i = 0; i < _amountOfImages; i++)
        {
            UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(_url);

            yield return webRequest.SendWebRequest();
            
            if(!webRequest.isDone)
                Debug.Log("Error :" + webRequest.error);
            else
            {
                Texture texture = ((DownloadHandlerTexture)webRequest.downloadHandler).texture;
                
                _downloadedImagesData.AddSprite(Sprite.Create((Texture2D)texture,
                    new Rect(0, 0, texture.width, texture.height), 
                    new Vector2(0.5f, 0.5f)));
            }
        }

        SceneManager.LoadScene("MainScene");
    }
}
