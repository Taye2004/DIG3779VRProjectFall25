using UnityEngine;
using UnityEngine.Video;

public class AutoVideoPlayer : MonoBehaviour
{
    public VideoPlayer videoPlayer;        // Assign in Inspector
    public Renderer targetRenderer;        // Assign Plane's MeshRenderer
    public VideoClip[] videoClips;         // Assign clips in Inspector

    private int currentIndex = 0;

    void Start()
    {
        if (videoClips.Length == 0)
        {
            Debug.LogWarning("No video clips assigned!");
            return;
        }

        // Configure video player
        videoPlayer.renderMode = VideoRenderMode.MaterialOverride;
        videoPlayer.targetMaterialRenderer = targetRenderer;
        videoPlayer.targetMaterialProperty = "_MainTex";

        // Subscribe to the end-of-video event
        videoPlayer.loopPointReached += OnVideoEnded;

        // Start the first video
        PlayVideo(currentIndex);
    }

    void PlayVideo(int index)
    {
        if (index >= 0 && index < videoClips.Length)
        {
            videoPlayer.clip = videoClips[index];
            videoPlayer.Play();
        }
    }

    void OnVideoEnded(VideoPlayer vp)
    {
        currentIndex++;

        if (currentIndex < videoClips.Length)
        {
            PlayVideo(currentIndex);
        }
      
   
           else
            {
                currentIndex = 0;
                PlayVideo(currentIndex); // Loops to the start
            }

        }
    }
