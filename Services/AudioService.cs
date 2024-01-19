using System;
using System.Diagnostics;
using System.IO;
using VideoLibrary;

namespace YTAudioDownloader.Services;

class AudioService {
  public AudioService() {}

  public List<string> ConvertVideosToAudio(List<string> videosPath, bool rmVideos = true) {
    List<string> endpoints = new();

    foreach (var video in videosPath) {
      endpoints.Add(ConvertVideoToAudio(video, rmVideos));
    }

    return endpoints;
  }

  public string ConvertVideoToAudio(string videoPath, bool rmVideo = true) {
    var outputPath = _transformToOutputPath(videoPath);
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

    if (rmVideo) _removeFile(videoPath);

    return outputPath;
  }

  private string _transformToOutputPath(string videoPath) {
    return videoPath.Replace("mp4", "mp3");
  }

  private void _removeFile(string filePath) {
    if (File.Exists(filePath)) File.Delete(filePath);
  }
}