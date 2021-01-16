using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Helpers.RegexValidators
{
    public class RegularExpressionType
    {
        /// <summary>
        /// Consisting of or using both letters and numerals.
        /// </summary>
        public const string Alphanumeric = @"^[àáâäæãåāçćčèéêëēėęîïíīįìłñńôöòóœøōõßśšşûüùúūÿžźżÀÁÂÄÆÃÅĀÇĆČÈÉÊËĒĖĖĘÎÏÍĪĮÌŁÑŃÔÖÒÓŒØŌÕŚŠÛÜÙÚŪYŽŹŻA-z0-9.', -/\n]+$";

        /// <summary>
        /// Password min 8 char
        /// </summary>
        public const string Password = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[\d#$^+=!*()@%&.]).{8,}$";

        /// <summary>
        /// UserName or email
        /// </summary>
        public const string UserName = @"^[a-zA-Z0-9_\.\-]+@[a-zA-Z0-9\-]+\.[a-zA-Z0-9\.\-]+$|^([a-zA-Z0-9]+)$";

        /// <summary>
        /// Regular expression for token
        /// </summary>
        public const string Token = @"^([\d\s\w\-=_.+\\/]+)$";

        /// <summary>
        /// email with/o numeric
        /// </summary>
        public const string EmailAdd = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";


        
    }
}
