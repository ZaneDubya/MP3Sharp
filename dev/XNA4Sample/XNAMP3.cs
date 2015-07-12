using System;
using MP3Sharp;
using Microsoft.Xna.Framework.Audio;

namespace XNA4Sample
{
    class XNAMP3
    {
        private MP3Stream m_Stream;

        private const int NUMBER_OF_PCM_BYTES_TO_READ_PER_CHUNK = 4096;
        private byte[] m_WaveBuffer = new byte[NUMBER_OF_PCM_BYTES_TO_READ_PER_CHUNK];
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
            if (bytesReturned != NUMBER_OF_PCM_BYTES_TO_READ_PER_CHUNK)
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
