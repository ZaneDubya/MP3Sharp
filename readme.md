# MP3Sharp
Decode MP3 files to PCM bitstreams entirely in .NET managed code:
<img align="center" src ="https://raw.githubusercontent.com/ZaneDubya/MP3Sharp/master/Images/MP3SharpHeader.png" />

# SETUP
To use MP3Sharp, you will need: an audio device that accepts PCM data, an array of bytes to act as the PCM data buffer (default size is 4096 bytes), and a MP3 file. That's it!

The default interface to MP3Sharp is the MP3Stream class. An instance of MP3Stream takes a filepath to a MP3 file as a parameter and outputs PCM data:
```CSharp
// open the mp3 file.
MP3Stream stream = new MP3Stream(@"sample.mp3");
// Create the buffer.
byte[] buffer = new byte[4096];
// read the entire mp3 file.
int bytesReturned = 1;
int totalBytesRead = 0;
while (bytesReturned > 0)
{
    bytesReturned = stream.Read(buffer, 0, buffer.Length);
    totalBytesRead += bytesReturned;
}
// close the stream after we're done with it.
stream.Close();
```
So simple!

## LICENSE
MP3Sharp is licensed under the [LGPL Version 3](https://github.com/ZaneDubya/MP3Sharp/blob/master/license.txt).

## CREDITS
MP3Sharp is a port of JavaLayer, a MP3 decoder written by [JavaZoom](http://www.javazoom.net) and released under the LGPL. JavaLayer was initially ported to C# by [Robert Burke](http://www.robburke.net/), in what he modestly describes as a 'half day project'. [tekHedd](http://www.byteheaven.com/) added some significant speed optimizations. This repository includes bug fixes (fixes for correctly outputting VBR, mono, and ability to loop without crashing; nice!). The sample MP3 file used in this project is by [BenSound](http://www.bensound.com), and is included under the terms of the Creative Commons - Attribution - No Derivative Works license.
