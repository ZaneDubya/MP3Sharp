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
            /*Console.WriteLine("Begin read...");
            for (int i = 0; i < 10; i++)
            {
                ReadAllTheWayThroughMp3File();
            }
            Console.WriteLine("... end!");
            Console.ReadKey();*/

            using (BaseGame game = new BaseGame())
            {
                game.Run();
            }
        }

        public static void ReadAllTheWayThroughMp3File()
        {
            MP3Stream stream = new MP3Stream(@"sample.mp3");
            XNAMP3 mp3 = new XNAMP3(stream);
            mp3.Play();

            // Create the buffer
            int numberOfPcmBytesToReadPerChunk = 512;
            byte[] buffer = new byte[numberOfPcmBytesToReadPerChunk];

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

