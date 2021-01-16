using System.Text.RegularExpressions;

namespace Application.Helpers.PasswordUtil
{
    public class PasswordValidation
    {
        public static bool IsValidPassword(string userName, string password, string sFullName, ref string errors, string oldPassword = "")
        {
            errors = string.Empty;
            //Minimum length 8
            if (password.Length < 8)
            {
                errors = $"The minimum password length must be at least 8 characters";
                return false;
            }

            if (!string.IsNullOrEmpty(oldPassword))
            {
                //New password letters cannot contain the same letters in same order as the old password
                if (Regex.Replace(oldPassword, @"[\d-]", string.Empty).ToLower().Contains(Regex.Replace(password, @"[\d-]", string.Empty).ToLower()))
                {
                    errors = $"Can't use similar character(s) from old password";
                    return false;
                }

                //Re-use cycle Cannot be the same as any of the previous passwords
                if (password == oldPassword)
                {
                    errors = $"Cannot re-used old password.";
                    return false;
                }
            }

            //Password Complexity contain the user's account name or parts of the user's full name that exceed two consecutive characters
            if (password.Replace(" ", "").ToLower().IndexOf(userName.ToLower()) > -1 || !IsNotSequentialChars(password, sFullName, 3))
            {
                errors = $"Cannot use username or parts of the full name (with 3 consecutive chars).";
                return false;
            }

            //Passwords must use at least three of the four available character types: lowercase letters, uppercase letters, numbers, and symbols.
            var matchCount = 0;
            var digitPattern = new Regex(@"\d");
            var smallLetterPattern = new Regex(@"[a-z]");
            var capitalLetterPattern = new Regex(@"[A-Z]");
            var specialCharacterPattern = new Regex(@"[^a-zA-Z\d]");

            if (digitPattern.IsMatch(password))
                matchCount++;

            if (smallLetterPattern.IsMatch(password))
                matchCount++;

            if (capitalLetterPattern.IsMatch(password))
                matchCount++;

            if (specialCharacterPattern.IsMatch(password))
                matchCount++;

            if (matchCount < 3)
            {
                errors = $"Passwords must use at least three of the four available character types: lowercase letters, uppercase letters, numbers, and symbols.";
                return false;
            }

            return true;
        }

        private static bool IsNotSequentialChars(string Src, string Dest, int check_len)
        {
            if (check_len < 1 || Src.Length < check_len) return true;
            Match m = Regex.Match(Src, "(?=(.{" + check_len + "})).");
            bool bOK = m.Success;

            while (bOK && m.Success)
            {
                // Edit: remove unnecessary '.*' from regex.
                // And btw, is regex needed at all in this case?
                bOK = !Regex.Match(Dest, "(?i)" + Regex.Escape(m.Groups[1].Value)).Success;
                m = m.NextMatch();
            }
            return bOK;
        }
    }
}
