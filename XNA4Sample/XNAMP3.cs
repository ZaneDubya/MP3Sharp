using Microsoft.Xna.Framework.Audio;
using MP3Sharp;
using System;

namespace XNA4Sample
{
    class XNAMP3 : IDisposable
    {
        private MP3Stream m_Stream;
        private DynamicSoundEffectInstance m_Instance;

        private const int NUMBER_OF_PCM_BYTES_TO_READ_PER_CHUNK = 4096;
        private byte[] m_WaveBuffer = new byte[NUMBER_OF_PCM_BYTES_TO_READ_PER_CHUNK];

        private bool m_Repeat = false;
        private bool m_Playing = false;

        public XNAMP3(string path)
        {
            m_Stream = new MP3Stream(path, NUMBER_OF_PCM_BYTES_TO_READ_PER_CHUNK);
            m_Instance = new DynamicSoundEffectInstance(22050, AudioChannels.Stereo);
            m_Instance.BufferNeeded += instance_BufferNeeded;
        }

        public void Dispose()
        {
            m_Instance.BufferNeeded -= instance_BufferNeeded;
            m_Instance.Dispose();
            m_Instance = null;

            m_Stream.Close();
            m_Stream = null;
        }

        public void Play(bool repeat = false)
        {
            if (m_Playing)
            {
                Stop();
            }

            m_Repeat = repeat;
            m_Playing = true;
            SubmitBuffer(3);
            m_Instance.Play();
        }

        public void Stop()
        {
            m_Playing = false;
            m_Instance.Stop();
        }

        private void instance_BufferNeeded(object sender, EventArgs e)
        {
            SubmitBuffer();
        }

        private void SubmitBuffer(int count = 1)
        {
            while (count > 0)
            {
                ReadFromStream();
                m_Instance.SubmitBuffer(m_WaveBuffer);
                count--;
            }
        }

        private void ReadFromStream()
        {
            int bytesReturned = m_Stream.Read(m_WaveBuffer, 0, m_WaveBuffer.Length);
            if (bytesReturned != NUMBER_OF_PCM_BYTES_TO_READ_PER_CHUNK)
            {
                if (m_Repeat)
                {
                    m_Stream.Position = 0;
                    m_Stream.Read(m_WaveBuffer, bytesReturned, m_WaveBuffer.Length - bytesReturned);
                }
                else
                {
                    if (bytesReturned == 0)
                    {
                        Stop();
                    }
                }
            }
        }
    }
}
