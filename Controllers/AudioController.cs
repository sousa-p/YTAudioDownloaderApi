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
  public IActionResult DownloadPlaylistUrl(List<string> playlist,  bool rmVideos = true)
  {
    List<string> endpoints = new();
    foreach (var url in playlist) {
      if (_videoService.IsVideoUrl(url)) {
        var videoPath = _videoService.DownloadVideo(url);
        endpoints.Add(_audioService.ConvertVideoToAudio(videoPath, rmVideos));
      } else {
        endpoints.Add($"{url} Invalid video Url");
      }
    }
    return Ok(endpoints);
  }
}