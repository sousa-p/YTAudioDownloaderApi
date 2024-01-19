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
  public IActionResult DownloadPlaylistUrl(string url)
  {
    if (_videoService.IsPlaylistUrl(url))
      return Ok(_videoService.DownloadVideos(url));
    
    return BadRequest("Invalid playlist Url");
  }

}