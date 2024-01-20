**YouTube Audio or Video Downloader RestAPI with C#**

A simple API created with C# and Swagger to download YouTube content!

**EndPoints:**

---

POST - /api/audio

`<string>` rmVideo default true;

_`<string>`_ url;

---

POST - /api/audio/playlist

_`<bool>`_ rmVideo default true;

_`List<string>`_ urls;

---

POST - /api/video

_`<string>`_ url;

---

POST - /api/video/playlist

_`List<string>`_ urls;

---

**Requirements to run:**

- .Net CLI
- ffmpeg apt-get
- Run with sudo
