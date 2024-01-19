using YTAudioDownloader.Services;
using Microsoft.AspNetCore.Mvc;

[Route("api/audio")]
[ApiController]
public class AudioController : ControllerBase
{
  private static AudioService _service = new();

  [HttpPost]
  public IActionResult DownloadUrl(string url)
  {
    if (_service.IsPlaylistUrl(url))
      return Ok(_service.DownloadAudios(url));
    
    if (_service.IsVideoUrl(url))
      return Ok(_service.DownloadAudio(url));
    
    return BadRequest("Invalid Url");
  }

  [HttpPost("playlist")]
  public IActionResult DownloadPlaylistUrl(string url)
  {
    if (_service.IsPlaylistUrl(url))
      return Ok(_service.DownloadAudios(url));
    
    return BadRequest("Invalid playlist Url");
  }

  [HttpPost("video")]
  public IActionResult DownloadVideoUrl(string url)
  {
    if (_service.IsVideoUrl(url))
      return Ok(_service.DownloadAudio(url));
    
    return BadRequest("Invalid video Url");
  }
}