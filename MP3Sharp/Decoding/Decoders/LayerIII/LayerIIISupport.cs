
namespace MP3Sharp.Decoding.Decoders.LayerIII
{
    class SBI
    {
        public int[] l;
        public int[] s;

        public SBI()
        {
            l = new int[23];
            s = new int[14];
        }

        public SBI(int[] thel, int[] thes)
        {
            l = thel;
            s = thes;
        }
    }

    internal class GranuleInfo
    {
        public int BigValues;
        public int BlockType;
        public int Count1TableSelect;
        public int GlobalGain;
        public int MixedBlockFlag;
        public int Part23Length;
        public int Preflag;
        public int Region0Count;
        public int Region1Count;
        public int ScaleFacCompress;
        public int ScaleFacScale;
        public int[] SubblockGain;
        public int[] TableSelect;
        public int WindowSwitchingFlag;

        /// <summary>
        ///     Dummy Constructor
        /// </summary>
        public GranuleInfo()
        {
            TableSelect = new int[3];
            SubblockGain = new int[3];
        }
    }

    internal class temporaire
    {
        public GranuleInfo[] Granules;
        public int[] scfsi;

        /// <summary>
        ///     Dummy Constructor
        /// </summary>
        public temporaire()
        {
            scfsi = new int[4];
            Granules = new GranuleInfo[2];
            Granules[0] = new GranuleInfo();
            Granules[1] = new GranuleInfo();
        }
    }

    internal class Layer3SideInfo
    {
        public temporaire[] Channels;
        public int MainDataBegin;
        public int PrivateBits;

        /// <summary>
        ///     Dummy Constructor
        /// </summary>
        public Layer3SideInfo()
        {
            Channels = new temporaire[2];
            Channels[0] = new temporaire();
            Channels[1] = new temporaire();
        }
    }

    internal class temporaire2
    {
        public int[] l; /* [cb] */
        public int[][] s; /* [window][cb] */

        /// <summary>
        ///     Dummy Constructor
        /// </summary>
        public temporaire2()
        {
            l = new int[23];
            s = new int[3][];
            for (int i = 0; i < 3; i++)
            {
                s[i] = new int[13];
            }
        }
    }

    internal class Sftable
    {
        private LayerIIIDecoder enclosingInstance;
        public int[] l;
        public int[] s;

        public Sftable(LayerIIIDecoder enclosingInstance)
        {
            InitBlock(enclosingInstance);
            l = new int[5];
            s = new int[3];
        }

        public Sftable(LayerIIIDecoder enclosingInstance, int[] thel, int[] thes)
        {
            InitBlock(enclosingInstance);
            l = thel;
            s = thes;
        }

        public LayerIIIDecoder Enclosing_Instance
        {
            get { return enclosingInstance; }
        }

        private void InitBlock(LayerIIIDecoder enclosingInstance)
        {
            this.enclosingInstance = enclosingInstance;
        }
    }
}
