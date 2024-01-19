using YTAudioDownloader.Services;
using Microsoft.AspNetCore.Mvc;

[Route("api/video")]
[ApiController]
public class VideoController : ControllerBase
{
  private static VideoService _videoService = new();

  [HttpPost]
  public IActionResult DownloadVideoUrl(string url)
  {
    if (_videoService.IsVideoUrl(url))
      return Ok(_videoService.DownloadVideo(url));
    
    return BadRequest("Invalid video Url");
  }

  [HttpPost("playlist")]
  public IActionResult DownloadPlaylistUrl(List<string> playlist)
  {
    List<string> endpoints = new();
    foreach (var url in playlist) {
      endpoints.Add(
        (_videoService.IsVideoUrl(url))
          ? _videoService.DownloadVideo(url)
          : $"{url} Invalid video Url"
      );
    }
    return Ok(endpoints);
  }

}