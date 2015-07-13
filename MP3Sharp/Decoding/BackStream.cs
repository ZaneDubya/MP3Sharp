// /***************************************************************************
//  * BackStream.cs
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
using System.IO;

namespace MP3Sharp.Decoding
{
    internal class BackStream
    {
        private readonly int BackBufferSize;
        private readonly CircularByteBuffer COB;
        private readonly Stream S;
        private readonly byte[] Temp;
        private int NumForwardBytesInBuffer;

        public BackStream(Stream s, int backBufferSize)
        {
            S = s;
            BackBufferSize = backBufferSize;
            Temp = new byte[BackBufferSize];
            COB = new CircularByteBuffer(BackBufferSize);
        }

        public int Read(sbyte[] toRead, int offset, int length)
        {
            // Read 
            int currentByte = 0;
            bool canReadStream = true;
            while (currentByte < length && canReadStream)
            {
                if (NumForwardBytesInBuffer > 0)
                {
                    // from mem
                    NumForwardBytesInBuffer--;
                    toRead[offset + currentByte] = (sbyte) COB[NumForwardBytesInBuffer];
                    currentByte++;
                }
                else
                {
                    // from stream
                    int newBytes = length - currentByte;
                    int numRead = S.Read(Temp, 0, newBytes);
                    canReadStream = numRead >= newBytes;
                    for (int i = 0; i < numRead; i++)
                    {
                        COB.Push(Temp[i]);
                        toRead[offset + currentByte + i] = (sbyte) Temp[i];
                    }
                    currentByte += numRead;
                }
            }
            return currentByte;
        }

        public void UnRead(int length)
        {
            NumForwardBytesInBuffer += length;
            if (NumForwardBytesInBuffer > BackBufferSize)
            {
                Console.WriteLine("YOUR BACKSTREAM IS FISTED!");
            }
        }

        public void Close()
        {
            S.Close();
        }
    }
}