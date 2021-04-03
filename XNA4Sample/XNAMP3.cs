// /***************************************************************************
//  * XNAMP3.cs
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
using Microsoft.Xna.Framework.Audio;
using MP3Sharp;

namespace XNA4Sample {
    class XNAMP3 : IDisposable {
        private MP3Stream _Stream;
        private DynamicSoundEffectInstance _Instance;

        private const int NUMBER_OF_PCM_BYTES_TO_READ_PER_CHUNK = 4096;
        private readonly byte[] _WaveBuffer = new byte[NUMBER_OF_PCM_BYTES_TO_READ_PER_CHUNK];

        private bool _Repeat;
        private bool _Playing;

        public XNAMP3(string path) {
            _Stream = new MP3Stream(path, NUMBER_OF_PCM_BYTES_TO_READ_PER_CHUNK);
            _Instance = new DynamicSoundEffectInstance(_Stream.Frequency, AudioChannels.Stereo);
        }

        public void Dispose() {
            if (_Playing) {
                Stop();
            }
            _Instance.Dispose();
            _Instance = null;
            _Stream.Close();
            _Stream = null;
        }

        public void Play(bool repeat = false) {
            if (_Playing) {
                Stop();
            }
            _Playing = true;
            _Repeat = repeat;
            SubmitBuffer(3);
            _Instance.BufferNeeded += InstanceBufferNeeded;
            _Instance.Play();
        }

        public void Stop() {
            if (_Playing) {
                _Playing = false;
                _Instance.Stop();
                _Instance.BufferNeeded -= InstanceBufferNeeded;
            }
        }

        private void InstanceBufferNeeded(object sender, EventArgs e) {
            SubmitBuffer();
        }

        private void SubmitBuffer(int count = 1) {
            while (count > 0) {
                ReadFromStream();
                _Instance.SubmitBuffer(_WaveBuffer);
                count--;
            }
        }

        private void ReadFromStream() {
            int bytesReturned = _Stream.Read(_WaveBuffer, 0, _WaveBuffer.Length);
            if (bytesReturned != NUMBER_OF_PCM_BYTES_TO_READ_PER_CHUNK) {
                if (_Repeat) {
                    _Stream.Position = 0;
                    _Stream.Read(_WaveBuffer, bytesReturned, _WaveBuffer.Length - bytesReturned);
                }
                else {
                    if (bytesReturned == 0) {
                        Stop();
                    }
                }
            }
        }
    }
}
