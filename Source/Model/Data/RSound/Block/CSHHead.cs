using System.Runtime.InteropServices;

namespace ASTRedux.Model.Data.RSound.Block;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
internal struct CSHHead
{
    public const int CSH_MAGIC = 0x20485343; // csh (CSoundHeader?)
}
