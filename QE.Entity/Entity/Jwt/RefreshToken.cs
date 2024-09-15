﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QE.Entity.Entity.Jwt
{
    [Owned]
    public class RefreshToken
    {
        public required string Token { get; set; }

        public DateTime ExpireOn { get; set; }

        public bool IsExpired => DateTime.UtcNow >= ExpireOn;

        // the time when the token is created
        public DateTime CreateOn { get; set; }

        // the time when the token is revoked/canceled
        public DateTime? RevokedOn { get; set; }

        // we have two conditions for token to be active"IsActive" => was not revoked and not IsExpired
        public bool IsActive => RevokedOn == null && !IsExpired;
    }
}
