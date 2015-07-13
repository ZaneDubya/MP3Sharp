using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MP3Sharp.Decoding.Decoders.LayerIII
{
    internal class ChannelData
    {
        public GranuleInfo[] Granules;
        public int[] scfsi;

        /// <summary>
        ///     Dummy Constructor
        /// </summary>
        public ChannelData()
        {
            scfsi = new int[4];
            Granules = new GranuleInfo[2];
            Granules[0] = new GranuleInfo();
            Granules[1] = new GranuleInfo();
        }
    }
}
