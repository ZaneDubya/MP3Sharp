using System;
using System.Diagnostics;

namespace MP3Sharp
{
    /// <summary>
    ///     Internal class used to queue samples that are being obtained
    ///     from an Mp3 stream. This merges the old mp3stream OBuffer with
    ///     the javazoom SampleBuffer code for the highest efficiency...
    ///     well, not the highest possible. The highest I'm willing to sweat
    ///     over. --trs
    ///     This class handles stereo 16-bit data! Switch it out if you want mono or something.
    /// </summary>
    internal class OBuffer16BitStereo
        : Decode.Obuffer
    {
        // This is stereo!
        private static readonly int CHANNELS = 2;
        // Write offset used in append_bytes
        private readonly byte[] m_Buffer = new byte[OBUFFERSIZE * 2]; // all channels interleaved
        private readonly int[] m_Bufferp = new int[MAXCHANNELS]; // offset in each channel not same!
        // end marker, one past end of array. Same as bufferp[0], but
        // without the array bounds check.
        private int m_End;
        // Read offset used to read from the stream, in bytes.
        private int m_Offset;

        public OBuffer16BitStereo()
        {
            // Initialize the buffer pointers
            clear_buffer();
        }

        public int BytesLeft
        {
            get
            {
                return m_End - m_Offset;
            }
        }

        /// Copies as much of this buffer as will fit into hte output
        /// buffer. Return The amount of bytes copied.
        public int Read(byte[] bufferOut, int offset, int count)
        {
            int remaining = BytesLeft;
            int copySize;
            if (count > remaining)
            {
                copySize = remaining;
            }
            else
            {
                // Copy an even number of sample frames
                int remainder = count % (2 * CHANNELS);
                copySize = count - remainder;
            }

            Array.Copy(m_Buffer, m_Offset, bufferOut, offset, copySize);

            m_Offset += copySize;
            return copySize;
        }

        // Inefficiently write one sample value
        public override void append(int channel, short valueRenamed)
        {
            m_Buffer[m_Bufferp[channel]] = (byte)(valueRenamed & 0xff);
            m_Buffer[m_Bufferp[channel] + 1] = (byte)(valueRenamed >> 8);

            m_Bufferp[channel] += CHANNELS * 2;
        }

        // efficiently write 32 samples
        public override void appendSamples(int channel, float[] f)
        {
            // Always, 32 samples are appended
            int pos = m_Bufferp[channel];

            for (int i = 0; i < 32; i++)
            {
                float fs = f[i];
                if (fs > 32767.0f) // can this happen?
                    fs = 32767.0f;
                else if (fs < -32767.0f)
                    fs = -32767.0f;

                int sample = (int)fs;
                m_Buffer[pos] = (byte)(sample & 0xff);
                m_Buffer[pos + 1] = (byte)(sample >> 8);

                pos += CHANNELS * 2;
            }

            m_Bufferp[channel] = pos;
        }

        /// <summary>
        ///     This implementation does not clear the buffer.
        /// </summary>
        public override sealed void clear_buffer()
        {
            m_Offset = 0;
            m_End = 0;

            for (int i = 0; i < CHANNELS; i++)
                m_Bufferp[i] = i * 2; // two bytes per channel
        }

        public override void set_stop_flag()
        {
        }

        public override void write_buffer(int val)
        {
            m_Offset = 0;

            // speed optimization - save end marker, and avoid
            // array access at read time. Can you believe this saves
            // like 1-2% of the cpu on a PIII? I guess allocating
            // that temporary "new int(0)" is expensive, too.
            m_End = m_Bufferp[0];
        }

        public override void close()
        {
        }
    }
}
