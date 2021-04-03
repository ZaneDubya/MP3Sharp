// /***************************************************************************
//  * Program.cs
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
using MP3Sharp;

namespace XNA4Sample {
#if WINDOWS || XBOX
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args) {
            using (BaseGame game = new BaseGame()) {
                game.Run();
            }
        }

        static void ExampleReadEntireMP3File() {
            MP3Stream stream = new MP3Stream("@sample.mp3");

            // Create the buffer
            const int NUMBER_OF_PCM_BYTES_TO_READ_PER_CHUNK = 4096;
            byte[] buffer = new byte[NUMBER_OF_PCM_BYTES_TO_READ_PER_CHUNK];

            int bytesReturned = -1;
            int totalBytes = 0;
            while (bytesReturned != 0) {
                bytesReturned = stream.Read(buffer, 0, buffer.Length);
                totalBytes += bytesReturned;
            }
            Console.WriteLine("Read a total of " + totalBytes + " bytes.");

            stream.Close();
            stream = null;
        }
    }
#endif
}

