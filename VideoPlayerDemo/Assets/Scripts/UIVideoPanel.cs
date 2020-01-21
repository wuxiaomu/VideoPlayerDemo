using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Video;

[Serializable]
public class FloatEvent : UnityEvent<float> { }

public class UIVideoPanel : MonoBehaviour
{

    private VideoController controller;

    [SerializeField]
    private VideoClip clip;

    [SerializeField]
    private GameObject StartPanel;
    [SerializeField]
    private Button PlayBtn;

    [SerializeField]
    private Button PlayPauseBtn;
    [SerializeField]
    private Text PlayBtnTxt;

    [SerializeField]
    private Slider PositionSlider;
    [SerializeField]
    private Slider PreviewSlider;

    [SerializeField]
    private FloatEvent onSeeked = new FloatEvent();

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<VideoController>();
        PlayBtn.onClick.AddListener(PlayVideo);
        PlayPauseBtn.onClick.AddListener(ToggleIsPlaying);
        PositionSlider.onValueChanged.AddListener(SliderValueChanged);

        StartPanel.SetActive(true);
    }

    private void OnDestroy()
    {
        PlayBtn.onClick.RemoveListener(PlayVideo);
        PlayPauseBtn.onClick.RemoveListener(ToggleIsPlaying);
        PositionSlider.onValueChanged.RemoveListener(SliderValueChanged);
    }


    // Update is called once per frame
    void Update()
    {
        if (controller.IsPlaying)
        {
            PreviewSlider.value = controller.NormalizedTime;
        }
    }

    private void PlayVideo()
    {
        if (clip == null)
        {
            return;
        }
        StartPanel.SetActive(false);
        controller.PrepareForClip(clip);
        PlayBtnTxt.text = "Pause";
    }

    private void ToggleIsPlaying()
    {
        if (controller.IsPlaying)
        {
            controller.Pause();
            PlayBtnTxt.text = "Play";
        }
        else
        {
            controller.Play();
            PlayBtnTxt.text = "Pause";
        }
    }

    private void SliderValueChanged(float value)
    {
        onSeeked.Invoke(value);
    }
}
