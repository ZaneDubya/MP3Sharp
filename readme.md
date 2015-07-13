# MP3Sharp
Decode MP3 files to PCM bitstreams entirely in .NET managed code.
<img align="center" src ="https://raw.githubusercontent.com/ZaneDubya/MP3Sharp/master/Images/MP3SharpHeader.png" />

## License
MP3Sharp is released under the [LGPL Version 3](https://github.com/ZaneDubya/MP3Sharp/blob/master/license.txt).

## Credits
MP3Sharp is a port of JavaLayer, a MP3 decoder written by [JavaZoom](http://www.javazoom.net) and released under the LGPL. JavaLayer was initially ported to C# by [Robert Burke](http://www.robburke.net/), in what he modestly describes as a 'half day project'. [tekHedd](http://www.byteheaven.com/) added some significant speed optimizations. I've cleaned up the code and reduced redundancies throughout.

## Areas for improvement
* Many of the variables throughout the ported code are poorly named. I don't know enough about the MP3 specification to suggest better names for them. 
* There is a large amount of dead code and variables that are assigned but never used, and thus significant area for improvement of decoding (using these variables properly) or optimization (removing them altogether).
