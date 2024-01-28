using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ProfitTest_Cafeteria.API
{
	public class AuthOptions
	{
		public const string PARAMNAME = ".cafetid";
		public const string ISSUER = "CafeteriaAPI";
		public const string AUDIENCE = "CafeteriaClient";
		const string KEY = "fr5uasQldo12x!_sevuvklerfj_ferenr";
		public const int LIFETIME = 60;
		public static SymmetricSecurityKey GetSymmetricSecurityKey()
		{
			return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
		}
	}
}
