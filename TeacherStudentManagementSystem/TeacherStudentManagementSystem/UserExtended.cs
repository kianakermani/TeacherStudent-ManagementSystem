using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace TeacherStudentManagementSystem
{
    public static class UserExtended
    {
        public static string GetFullName(this IPrincipal user)
        {
            var claim = ((ClaimsIdentity)user.Identity).FindFirst(ClaimTypes.NameIdentifier);
            return claim == null ? null : claim.Value;
        }
        public static string GetAddress(this IPrincipal user)
        {
            var claim = ((ClaimsIdentity)user.Identity).FindFirst(ClaimTypes.StreetAddress);
            return claim == null ? null : claim.Value;
        }


    }
}