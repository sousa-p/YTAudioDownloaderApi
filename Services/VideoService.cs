using System;
using System.Diagnostics;
using System.IO;
using VideoLibrary;

namespace YTAudioDownloader.Services;

class VideoService {
  private string _source;

  public VideoService(string? source = null) {
    _source = source ?? @"./downloads/";
  }

  public List<string> DownloadVideos(string playlistUrl) {
    var playlist = YouTube.Default.GetAllVideos(playlistUrl);
    List<string> endpoints = new();

    foreach (var video in playlist) {
      endpoints.Add(DownloadVideo(video.Uri));
    }

    return endpoints;
  }


  public string DownloadVideo(string videoUrl) {
    try {
      var youTube = YouTube.Default;
      var video = youTube.GetVideo(videoUrl);

      var timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
      var arqName =  _source + _cleanVideoName(video.FullName) + timestamp;
      var videoPath = arqName + ".mp4";

      File.WriteAllBytes(videoPath, video.GetBytes());

      return videoPath;
    } catch (Exception ex) {
      Console.WriteLine(ex.Message);
    }
    
    return $"{videoUrl} Error Try Again";
  }

  private string _cleanVideoName(string video) {
    return video
      .Replace(' ', '_')
      .Replace("mp4", "")
      .Replace(".", "")
      .Replace("-", "")
      .Replace("\"", "")
      .Replace("'", "");
  }

  public bool IsPlaylistUrl(string url)
  {
    return url.Contains("youtube.com/playlist");
  }

  public bool IsVideoUrl(string url)
  {
    return url.Contains("youtube.com/watch");
  }
}