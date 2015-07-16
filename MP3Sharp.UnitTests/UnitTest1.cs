using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;

namespace MP3Sharp.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void MP3Stream_basic_properties()
        {
            using (var mp3 = Assembly.GetExecutingAssembly().GetManifestResourceStream("MP3Sharp.UnitTests.sample.mp3"))
            using (var stream = new MP3Stream(mp3))
            {
                Assert.IsFalse(stream.IsEOF);
                Assert.AreEqual(stream.Length, mp3.Length);
                Assert.IsTrue(stream.CanRead);
                Assert.IsTrue(stream.CanSeek);
                Assert.IsFalse(stream.CanWrite);
                Assert.AreEqual(0, stream.ChunkSize);
                Assert.AreEqual(370, stream.Position);
                Assert.AreEqual(44100, stream.Frequency);
                Assert.AreEqual(2, stream.ChannelCount);
                Assert.AreEqual(SoundFormat.Pcm16BitStereo, stream.Format);

                byte[] buffer = new byte[4096];
                int bytesReturned = 1;
                int totalBytesRead = 0;
                while (bytesReturned > 0)
                {
                    bytesReturned = stream.Read(buffer, 0, buffer.Length);
                    totalBytesRead += bytesReturned;
                }

                Assert.IsTrue(stream.IsEOF);
            }
        }

        [TestMethod]
        public void MP3Stream_read_md5()
        {
            var md5 = System.Security.Cryptography.MD5.Create();

            using (var mp3 = Assembly.GetExecutingAssembly().GetManifestResourceStream("MP3Sharp.UnitTests.sample.mp3"))
            using (var stream = new MP3Stream(mp3))
            using (var memory = new System.IO.MemoryStream())
            {
                byte[] buffer = new byte[4096];
                int bytesRead;

                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    memory.Write(buffer, 0, bytesRead);
                }

                byte[] md5hash = md5.ComputeHash(memory.ToArray());
                Assert.AreEqual(9576522144988971710UL, BitConverter.ToUInt64(md5hash, 0));
                Assert.AreEqual(4012292588662891826UL, BitConverter.ToUInt64(md5hash, 8));
            }
        }

    }
}