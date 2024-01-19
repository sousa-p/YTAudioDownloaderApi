using System.IO;
using VideoLibrary;
using MediaToolkit;
using MediaToolkit.Model;

namespace YTAudioDownloader.Services;

class AudioService {
  private string _source;

  public AudioService(string source = null) {
    _source = source ?? @"../downloads";
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
    var youTube = YouTube.Default;
    var video = youTube.GetVideo(videoUrl);

    var videoFileName = _source + video.FullName;
    var mp3FileName = $"{videoFileName}.mp3";

    File.WriteAllBytes(videoFileName, video.GetBytes());

    var inputFile = new MediaFile { Filename = videoFileName };
    var outputFile = new MediaFile { Filename = mp3FileName };

    using (var engine = new Engine())
    {
      engine.GetMetadata(inputFile);
      engine.Convert(inputFile, outputFile);
    }

    return mp3FileName;
  }
}