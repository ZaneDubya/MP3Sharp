// /***************************************************************************
//  * WaveFileObuffer.cs
//  * Copyright (c) 2015 the authors.
//  * 
//  * All rights reserved. This program and the accompanying materials
//  * are made available under the terms of the GNU Lesser General Public License
//  * (LGPL) version 3 which accompanies this distribution, and is available at
//  * https://www.gnu.org/licenses/lgpl-3.0.en.html
//  *
//  * This library is distributed in the hope that it will be useful,
//  * but WITHOUT ANY WARRANTY; without even the implied warranty of
//  * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
//  * Lesser General Public License for more details.
//  *
//  ***************************************************************************/

using System;
using MP3Sharp.Decode;

namespace MP3Sharp.Convert
{
    /// <summary> Implements an Obuffer by writing the data to a file in RIFF WAVE format.</summary>
    internal class WaveFileObuffer : Obuffer
    {
        private readonly short[] buffer;
        private readonly short[] bufferp;
        private readonly int channels;
        private readonly WaveFile outWave;

        /// <summary>
        ///     Write the samples to the file (Random Acces).
        /// </summary>
        internal short[] myBuffer;

        /// <summary>
        ///     Creates a new WareFileObuffer instance.
        /// </summary>
        /// <param name="">
        ///     number_of_channels
        ///     The number of channels of audio data
        ///     this buffer will receive.
        /// </param>
        /// <param name="freq	The">
        ///     sample frequency of the samples in the buffer.
        /// </param>
        /// <param name="fileName	The">
        ///     filename to write the data to.
        /// </param>
        public WaveFileObuffer(int number_of_channels, int freq, string FileName)
        {
            InitBlock();
            if (FileName == null)
                throw new NullReferenceException("FileName");

            buffer = new short[OBUFFERSIZE];
            bufferp = new short[MAXCHANNELS];
            channels = number_of_channels;

            for (int i = 0; i < number_of_channels; ++i)
                bufferp[i] = (short) i;

            outWave = new WaveFile();

            int rc = outWave.OpenForWrite(FileName, null, freq, (short) 16, (short) channels);
        }

        public WaveFileObuffer(int number_of_channels, int freq, System.IO.Stream stream)
        {
            InitBlock();

            buffer = new short[OBUFFERSIZE];
            bufferp = new short[MAXCHANNELS];
            channels = number_of_channels;

            for (int i = 0; i < number_of_channels; ++i)
                bufferp[i] = (short) i;

            outWave = new WaveFile();

            int rc = outWave.OpenForWrite(null, stream, freq, (short) 16, (short) channels);
        }

        private void InitBlock()
        {
            myBuffer = new short[2];
        }

        /// <summary>
        ///     Takes a 16 Bit PCM sample.
        /// </summary>
        public override void append(int channel, short value_Renamed)
        {
            buffer[bufferp[channel]] = value_Renamed;
            bufferp[channel] = (short) (bufferp[channel] + channels);
        }

        public override void write_buffer(int val)
        {
            int k = 0;
            int rc = 0;

            rc = outWave.WriteData(buffer, bufferp[0]);
            // REVIEW: handle RiffFile errors. 
            /*
			for (int j=0;j<bufferp[0];j=j+2)
			{
			
			//myBuffer[0] = (short)(((buffer[j]>>8)&0x000000FF) | ((buffer[j]<<8)&0x0000FF00));
			//myBuffer[1] = (short) (((buffer[j+1]>>8)&0x000000FF) | ((buffer[j+1]<<8)&0x0000FF00));
			myBuffer[0] = buffer[j];
			myBuffer[1] = buffer[j+1];
			rc = outWave.WriteData (myBuffer,2);
			}
			*/
            for (int i = 0; i < channels; ++i)
                bufferp[i] = (short) i;
        }

        public void close(bool justWriteLengthBytes)
        {
            outWave.Close(justWriteLengthBytes);
        }

        public override void close()
        {
            outWave.Close();
        }

        /// <summary>
        ///     *
        /// </summary>
        public override void clear_buffer()
        {
        }

        /// <summary>
        ///     *
        /// </summary>
        public override void set_stop_flag()
        {
        }
    }
}