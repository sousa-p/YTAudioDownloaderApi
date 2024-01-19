using System;
using System.Diagnostics;
using System.IO;
using VideoLibrary;

namespace YTAudioDownloader.Services;

class AudioService {
  private string _source;

  public AudioService(string source = null) {
    _source = source ?? @"./downloads/";
  }


  public bool IsPlaylistUrl(string url)
  {
    return url.Contains("youtube.com/playlist");
  }

  public bool IsVideoUrl(string url)
  {
    return url.Contains("youtube.com/watch");
  }


  public List<string> DownloadAudios(string playlistUrl) {
    var playlist = YouTube.Default.GetAllVideos(playlistUrl);
    List<string> endpoints = new();

    foreach (var video in playlist) {
      endpoints.Add(DownloadAudio(video.Uri));
    }

    return endpoints;
  }


  public string DownloadAudio(string videoUrl) {
    try {
      var youTube = YouTube.Default;
      var video = youTube.GetVideo(videoUrl);

      var timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
      var arqName =  _source + _cleanVideoName(video.FullName) + timestamp;
      var videoPath = arqName + ".mp4";
      var outputPath = arqName + ".mp3";

      File.WriteAllBytes(videoPath, video.GetBytes());

      _convertMp4toMp3(videoPath, outputPath);
      _removeFile(videoPath);
      
      return outputPath;
    } catch (Exception ex) {
      Console.WriteLine(ex.Message);
    }
    
    return $"{videoUrl} Error Try Again";
  }


  private void _convertMp4toMp3(string videoPath, string outputPath) {
    string ffmpegCommand = $"ffmpeg -i {videoPath} -vn -acodec mp3 {outputPath}";

    ProcessStartInfo psi = new ProcessStartInfo("bash");
    psi.RedirectStandardInput = true;
    psi.UseShellExecute = false;
    psi.CreateNoWindow = true;

    Process process = new Process();
    process.StartInfo = psi;
    process.Start();

    process.StandardInput.WriteLine(ffmpegCommand);
    process.StandardInput.Close();

    process.WaitForExit();
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

  private void _removeFile(string filePath) {
    if (File.Exists(filePath)) File.Delete(filePath);
  }
}