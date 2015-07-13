using System;
using MP3Sharp;
using Microsoft.Xna.Framework.Audio;

namespace XNA4Sample
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            // ExampleReadMP3File();

            using (BaseGame game = new BaseGame())
            {
                game.Run();
            }
        }

        public static void ExampleReadMP3File()
        {
            MP3Stream stream = new MP3Stream(@"sample.mp3");
            XNAMP3 mp3 = new XNAMP3(stream);
            mp3.Play();

            // Create the buffer
            const int NUMBER_OF_PCM_BYTES_TO_READ_PER_CHUNK = 4096;
            byte[] buffer = new byte[NUMBER_OF_PCM_BYTES_TO_READ_PER_CHUNK];

            int bytesReturned = -1;
            int totalBytes = 0;
            while (bytesReturned != 0)
            {
                bytesReturned = stream.Read(buffer, 0, buffer.Length);
                totalBytes += bytesReturned;
            }
            Console.WriteLine("Read a total of " + totalBytes + " bytes.");

            stream.Close();
            stream = null;
        }
    }
#endif
}

