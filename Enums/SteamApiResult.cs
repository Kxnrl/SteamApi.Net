using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kxnrl.SteamApi.Enums;

public enum SteamApiResult
{
    None,
    Ok,
    Fail,
    InvalidParam = 8,
    NotFound     = 9,
}