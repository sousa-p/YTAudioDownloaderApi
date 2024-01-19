using YTAudioDownloader.Services;
using Microsoft.AspNetCore.Mvc;

[Route("api/audio")]
[ApiController]
public class AudioController : ControllerBase
{
  private static VideoService _videoService = new();
  private static AudioService _audioService = new();

  [HttpPost]
  public IActionResult DownloadAudioUrl(string url, bool rmVideo = true)
  {
    if (_videoService.IsVideoUrl(url)) {
      var videoPath = _videoService.DownloadVideo(url);
      return Ok(_audioService.ConvertVideoToAudio(videoPath, rmVideo));
    }
    
    return BadRequest("Invalid video Url");
  }

  [HttpPost("playlist")]
  public IActionResult DownloadPlaylistUrl(string url,  bool rmVideos = true)
  {
    if (_videoService.IsPlaylistUrl(url)) {
      var videosPath = _videoService.DownloadVideos(url);
      return Ok(_audioService.ConvertVideosToAudio(videosPath, rmVideos));
    }
    
    return BadRequest("Invalid playlist Url");
  }

}