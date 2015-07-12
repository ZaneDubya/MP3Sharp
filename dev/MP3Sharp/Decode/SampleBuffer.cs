// /***************************************************************************
//  *   SampleBuffer.cs
//  *   Copyright (c) 2015 Zane Wagner, Robert Burke,
//  *   the JavaZoom team, and others.
//  * 
//  *   All rights reserved. This program and the accompanying materials
//  *   are made available under the terms of the GNU Lesser General Public License
//  *   (LGPL) version 2.1 which accompanies this distribution, and is available at
//  *   http://www.gnu.org/licenses/lgpl-2.1.html
//  *
//  *   This library is distributed in the hope that it will be useful,
//  *   but WITHOUT ANY WARRANTY; without even the implied warranty of
//  *   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
//  *   Lesser General Public License for more details.
//  *
//  ***************************************************************************/

namespace MP3Sharp.Decode
{
    /// <summary>
    ///     The <code>SampleBuffer</code> class implements an output buffer
    ///     that provides storage for a fixed size block of samples.
    /// </summary>
    internal class SampleBuffer : Obuffer
    {
        private readonly short[] buffer;
        private readonly int[] bufferp;
        private readonly int channels;
        private readonly int frequency;

        /// <summary>
        ///     Constructor
        /// </summary>
        public SampleBuffer(int sample_frequency, int number_of_channels)
        {
            buffer = new short[OBUFFERSIZE];
            bufferp = new int[MAXCHANNELS];
            channels = number_of_channels;
            frequency = sample_frequency;

            for (int i = 0; i < number_of_channels; ++i)
                bufferp[i] = (short) i;
        }

        public virtual int ChannelCount
        {
            get { return channels; }
        }

        public virtual int SampleFrequency
        {
            get { return frequency; }
        }

        public virtual short[] Buffer
        {
            get { return buffer; }
        }

        public virtual int BufferLength
        {
            get { return bufferp[0]; }
        }

        /// <summary>
        ///     Takes a 16 Bit PCM sample.
        /// </summary>
        public override void append(int channel, short value_Renamed)
        {
            buffer[bufferp[channel]] = value_Renamed;
            bufferp[channel] += channels;
        }

        public override void appendSamples(int channel, float[] f)
        {
            int pos = bufferp[channel];

            short s;
            float fs;
            for (int i = 0; i < 32;)
            {
                fs = f[i++];
                fs = (fs > 32767.0f ? 32767.0f : (fs < -32767.0f ? -32767.0f : fs));

                //UPGRADE_WARNING: Narrowing conversions may produce unexpected results in C#. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1042"'
                s = (short) fs;
                buffer[pos] = s;
                pos += channels;
            }

            bufferp[channel] = pos;
        }

        /// <summary>
        ///     Write the samples to the file (Random Acces).
        /// </summary>
        public override void write_buffer(int val)
        {
            //for (int i = 0; i < channels; ++i) 
            //	bufferp[i] = (short)i;
        }

        public override void close()
        {
        }

        /// <summary>
        ///     *
        /// </summary>
        public override void clear_buffer()
        {
            for (int i = 0; i < channels; ++i)
                bufferp[i] = (short) i;
        }

        /// <summary>
        ///     *
        /// </summary>
        public override void set_stop_flag()
        {
        }
    }
}