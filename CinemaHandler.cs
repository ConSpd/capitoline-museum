using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CinemaHandler : MonoBehaviour
{

    private UnityEngine.Video.VideoPlayer videoPlayer;
    private Vector3 playerPosition;
    private Player player;
    private float distance;

    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = gameObject.GetComponent<UnityEngine.Video.VideoPlayer>();
        videoPlayer.Pause();
        player = FindObjectOfType<Player>();
    }

    private void Update() {
        playerPosition = player.transform.position;
        distance = Vector3.Distance(playerPosition,transform.position);
        if(distance <= 10)
            StartVideo();
        else
            videoPlayer.Pause();
    }

    public void StartVideo() {
        videoPlayer.Play();
    }
}
