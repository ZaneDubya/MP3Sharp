using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MP3Sharp;
using Microsoft.Xna.Framework.Audio;

namespace XNA4Sample
{
    class XNAMP3
    {
        private MP3Stream m_Stream;

        private const int numberOfPcmBytesToReadPerChunk = 2048;
        private byte[] m_WaveBuffer = new byte[numberOfPcmBytesToReadPerChunk];
        DynamicSoundEffectInstance m_Instance;

        public XNAMP3(MP3Stream stream)
        {
            m_Stream = stream;
            ReadFromStream();
        }

        public void Play()
        {
            m_Instance = new DynamicSoundEffectInstance(22050, AudioChannels.Stereo);
            m_Instance.BufferNeeded += new EventHandler<EventArgs>(instance_BufferNeeded);
            m_Instance.SubmitBuffer(m_WaveBuffer);
            m_Instance.Play();
        }

        private void ReadFromStream()
        {
            int bytesReturned = m_Stream.Read(m_WaveBuffer, 0, m_WaveBuffer.Length);
            if (bytesReturned != numberOfPcmBytesToReadPerChunk)
            {

            }
        }

        void instance_BufferNeeded(object sender, EventArgs e)
        {
            ReadFromStream();
            m_Instance.SubmitBuffer(m_WaveBuffer);
        }
    }
}
