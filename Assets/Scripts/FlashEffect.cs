using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FlashEffect : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Sprite[] _sprites;

    private void Awake()
    {
        if (_image == null)
            throw new NullReferenceException(nameof(_image));

        if (_sprites == null)
            throw new NullReferenceException(nameof(_image));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out RollerGirl rollerGirl))
            StartCoroutine(Play());
    }

    private IEnumerator Play()
    {
        _image.enabled = true;

        float delay = 0.05f;
        WaitForSeconds waitForSeconds = new WaitForSeconds(delay);

        foreach (Sprite image in _sprites)
        {
            _image.sprite = image;
            _image.SetNativeSize();
            yield return waitForSeconds;
        }

        _image.enabled = false;
    }
}
