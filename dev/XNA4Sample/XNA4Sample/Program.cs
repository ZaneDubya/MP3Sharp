using System;
using Mp3Sharp;

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
            Console.WriteLine("Begin read...");
            ReadAllTheWayThroughMp3File();
            Console.WriteLine("... end!");
            Console.ReadKey();

            using (BaseGame game = new BaseGame())
            {
                game.Run();
            }
        }

        public static void ReadAllTheWayThroughMp3File()
        {
            Mp3Stream stream = new Mp3Stream(@"sample.mp3");

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
        }
    }
#endif
}

