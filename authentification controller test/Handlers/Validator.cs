using Microsoft.IdentityModel.Tokens;

namespace authentification_controller_test.Handlers
{
    public class Validator
    {
        public static bool Validate(string str)
        {
            if(str.IsNullOrEmpty())
                return false;
            if (str.All(char.IsWhiteSpace))
                return false;
            return true;
        }
    }
}
