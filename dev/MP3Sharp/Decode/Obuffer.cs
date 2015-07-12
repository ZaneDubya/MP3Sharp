// /***************************************************************************
//  *   Obuffer.cs
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
    ///     Base Class for audio output.
    /// </summary>
    internal abstract class Obuffer
    {
        public const int OBUFFERSIZE = 2*1152; // max. 2 * 1152 samples per frame
        public const int MAXCHANNELS = 2; // max. number of channels

        /// <summary>
        ///     Takes a 16 Bit PCM sample.
        /// </summary>
        public abstract void append(int channel, short value_Renamed);

        /// <summary>
        ///     Accepts 32 new PCM samples.
        /// </summary>
        public virtual void appendSamples(int channel, float[] f)
        {
            short s;
            for (int i = 0; i < 32; i++)
            {
                append(channel, (short) clip((f[i])));
            }
        }

        /// <summary>
        ///     Clip Sample to 16 Bits
        /// </summary>
        private short clip(float sample)
        {
            //UPGRADE_WARNING: Narrowing conversions may produce unexpected results in C#. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1042"'
            return ((sample > 32767.0f) ? (short) 32767 : ((sample < -32768.0f) ? (short) -32768 : (short) sample));
        }

        /// <summary>
        ///     Write the samples to the file or directly to the audio hardware.
        /// </summary>
        public abstract void write_buffer(int val);

        public abstract void close();

        /// <summary>
        ///     Clears all data in the buffer (for seeking).
        /// </summary>
        public abstract void clear_buffer();

        /// <summary>
        ///     Notify the buffer that the user has stopped the stream.
        /// </summary>
        public abstract void set_stop_flag();
    }
}