using System;

namespace TeltonikaParser.Sensors
{
    public class MANUALCAN : IDisposable
    {
        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Cans = null;
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~MANUALCAN()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        private long[] Cans;
        public MANUALCAN() : base()
        {
            Cans = new long[70];
        }
        public long CAN00 { get => Cans[0]; set => Cans[0] = value; }
        public long CAN01 { get => Cans[1]; set => Cans[1] = value; }
        public long CAN02 { get => Cans[2]; set => Cans[2] = value; }
        public long CAN03 { get => Cans[3]; set => Cans[3] = value; }
        public long CAN04 { get => Cans[4]; set => Cans[4] = value; }
        public long CAN05 { get => Cans[5]; set => Cans[5] = value; }
        public long CAN06 { get => Cans[6]; set => Cans[6] = value; }
        public long CAN07 { get => Cans[7]; set => Cans[7] = value; }
        public long CAN08 { get => Cans[8]; set => Cans[8] = value; }
        public long CAN09 { get => Cans[9]; set => Cans[9] = value; }
        public long CAN10 { get => Cans[10]; set => Cans[10] = value; }
        public long CAN11 { get => Cans[11]; set => Cans[11] = value; }
        public long CAN12 { get => Cans[12]; set => Cans[12] = value; }
        public long CAN13 { get => Cans[13]; set => Cans[13] = value; }
        public long CAN14 { get => Cans[14]; set => Cans[14] = value; }
        public long CAN15 { get => Cans[15]; set => Cans[15] = value; }
        public long CAN16 { get => Cans[16]; set => Cans[16] = value; }
        public long CAN17 { get => Cans[17]; set => Cans[17] = value; }
        public long CAN18 { get => Cans[18]; set => Cans[18] = value; }
        public long CAN19 { get => Cans[19]; set => Cans[19] = value; }
        public long CAN20 { get => Cans[20]; set => Cans[20] = value; }
        public long CAN21 { get => Cans[21]; set => Cans[21] = value; }
        public long CAN22 { get => Cans[22]; set => Cans[22] = value; }
        public long CAN23 { get => Cans[23]; set => Cans[23] = value; }
        public long CAN24 { get => Cans[24]; set => Cans[24] = value; }
        public long CAN25 { get => Cans[25]; set => Cans[25] = value; }
        public long CAN26 { get => Cans[26]; set => Cans[26] = value; }
        public long CAN27 { get => Cans[27]; set => Cans[27] = value; }
        public long CAN28 { get => Cans[28]; set => Cans[28] = value; }
        public long CAN29 { get => Cans[29]; set => Cans[29] = value; }
        public long CAN30 { get => Cans[30]; set => Cans[30] = value; }
        public long CAN31 { get => Cans[31]; set => Cans[31] = value; }
        public long CAN32 { get => Cans[32]; set => Cans[32] = value; }
        public long CAN33 { get => Cans[33]; set => Cans[33] = value; }
        public long CAN34 { get => Cans[34]; set => Cans[34] = value; }
        public long CAN35 { get => Cans[35]; set => Cans[35] = value; }
        public long CAN36 { get => Cans[36]; set => Cans[36] = value; }
        public long CAN37 { get => Cans[37]; set => Cans[37] = value; }
        public long CAN38 { get => Cans[38]; set => Cans[38] = value; }
        public long CAN39 { get => Cans[39]; set => Cans[39] = value; }
        public long CAN40 { get => Cans[40]; set => Cans[40] = value; }
        public long CAN41 { get => Cans[41]; set => Cans[41] = value; }
        public long CAN42 { get => Cans[42]; set => Cans[42] = value; }
        public long CAN43 { get => Cans[43]; set => Cans[43] = value; }
        public long CAN44 { get => Cans[44]; set => Cans[44] = value; }
        public long CAN45 { get => Cans[45]; set => Cans[45] = value; }
        public long CAN46 { get => Cans[46]; set => Cans[46] = value; }
        public long CAN47 { get => Cans[47]; set => Cans[47] = value; }
        public long CAN48 { get => Cans[48]; set => Cans[48] = value; }
        public long CAN49 { get => Cans[49]; set => Cans[49] = value; }
        public long CAN50 { get => Cans[50]; set => Cans[50] = value; }
        public long CAN51 { get => Cans[51]; set => Cans[51] = value; }
        public long CAN52 { get => Cans[52]; set => Cans[52] = value; }
        public long CAN53 { get => Cans[53]; set => Cans[53] = value; }
        public long CAN54 { get => Cans[54]; set => Cans[54] = value; }
        public long CAN55 { get => Cans[55]; set => Cans[55] = value; }
        public long CAN56 { get => Cans[56]; set => Cans[56] = value; }
        public long CAN57 { get => Cans[57]; set => Cans[57] = value; }
        public long CAN58 { get => Cans[58]; set => Cans[58] = value; }
        public long CAN59 { get => Cans[59]; set => Cans[59] = value; }
        public long CAN60 { get => Cans[60]; set => Cans[60] = value; }
        public long CAN61 { get => Cans[61]; set => Cans[61] = value; }
        public long CAN62 { get => Cans[62]; set => Cans[62] = value; }
        public long CAN63 { get => Cans[63]; set => Cans[63] = value; }
        public long CAN64 { get => Cans[64]; set => Cans[64] = value; }
        public long CAN65 { get => Cans[65]; set => Cans[65] = value; }
        public long CAN66 { get => Cans[66]; set => Cans[66] = value; }
        public long CAN67 { get => Cans[67]; set => Cans[67] = value; }
        public long CAN68 { get => Cans[68]; set => Cans[68] = value; }
        public long CAN69 { get => Cans[69]; set => Cans[69] = value; }

    }
}
