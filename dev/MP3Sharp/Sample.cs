// /***************************************************************************
//  *   Sample.cs.cs
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
using System;

namespace MP3Sharp
{
	/// <summary>
	/// Some samples that show the use of the Mp3Stream class.
	/// </summary>
	internal class Sample
	{
	    public static readonly string Mp3FilePath = @"Sample.mp3";

	    static void Main(string[] args)
        {
            Console.WriteLine("Begin read...");
            ReadAllTheWayThroughMp3File();
            Console.WriteLine("... end!");
            Console.ReadKey();
        }

	    /// <summary>
		/// Sample showing how to read through an MP3 file and obtain its contents as a PCM byte stream.
		/// </summary>
		public static void ReadAllTheWayThroughMp3File()
		{
			Mp3Stream stream = new Mp3Stream(Mp3FilePath);

			// Create the buffer
			int numberOfPcmBytesToReadPerChunk = 512;
			byte[] buffer = new byte[numberOfPcmBytesToReadPerChunk];

			int bytesReturned = -1;
			int totalBytes = 0;
			while (bytesReturned != 0)
			{
				bytesReturned = stream.Read(buffer, 0, buffer.Length);
				totalBytes += bytesReturned;
			}
			Console.WriteLine("Read a total of " + totalBytes + " bytes.");
		}
	}
}
