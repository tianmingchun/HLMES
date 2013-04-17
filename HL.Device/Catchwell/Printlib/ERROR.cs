namespace Printlib
{
    using System;

    public enum ERROR
    {
        CARD_OK = 0x9000,
        ER_CARDCOMM = -7,
        ER_CARDTYPE = 0x2001,
        ER_COMMFORMATERR = -5,
        ER_COMMPORTCLOSED = -1,
        ER_COMMRANGE = -6,
        ER_COMREADFAIL = -3,
        ER_COMWRITEFAIL = -2,
        ER_HARD = 0x2400,
        ER_NOCARD = 0x2000,
        ER_PARAM = 0x2200,
        ER_UNKNOWN = 0x2300,
        ER_VERIFY = 0x2203,
        OPSUCCESS = 0
    }
}

