using System.Runtime.InteropServices;

namespace System.WinSystem
{
    // Token: 0x02000029 RID: 41
    public struct GUID
    {
        // Token: 0x0400007A RID: 122
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] Data1;

        // Token: 0x0400007B RID: 123
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] Data2;

        // Token: 0x0400007C RID: 124
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] Data3;

        // Token: 0x0400007D RID: 125
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] Data4;
    }
}
