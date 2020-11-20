using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Web;

namespace Bookworm_Desktop.Services
{
	public class Authentication
	{
		static RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
		static int ITERATIONS = 10000;

		public static (string, string) RegisterUser(string plaintextPassword)
		{
			Rfc2898DeriveBytes hash;
			var generatedSaltBytes = new byte[16];

			rng.GetBytes(generatedSaltBytes);

			hash = new Rfc2898DeriveBytes(plaintextPassword, generatedSaltBytes, ITERATIONS);

			var digestedHashedPassword = BitConverter.ToString(hash.GetBytes(16)).Replace("-","").ToLower();

			hash.Dispose();
			var salt = BitConverter.ToString(generatedSaltBytes).Replace("-", "").ToLower();

			return (digestedHashedPassword, salt);
		}

		public static bool LogUserIn(string plaintextPassword, string digestedHash, string digestedSalt)
		{
			
			Rfc2898DeriveBytes hash;
			var saltBytes = StringToByteArray(digestedSalt);

			hash = new Rfc2898DeriveBytes(plaintextPassword, saltBytes, ITERATIONS);

			var digestedHashedPassword = BitConverter.ToString(hash.GetBytes(16)).Replace("-", "").ToLower();

			hash.Dispose();
			if (digestedHashedPassword != digestedHash)
				return false;
			return true;

		}

		public static string GetHash(string password, string salt)
		{
			var saltBytes = StringToByteArray(salt);

			using (var hash = new Rfc2898DeriveBytes(password, saltBytes, ITERATIONS))
				return BitConverter.ToString(hash.GetBytes(16)).Replace("-", "").ToLower();
		}

		private static byte[] StringToByteArray(string hex)
		{
			if (hex.Length % 2 == 1)
				throw new Exception("The binary key cannot have an odd number of digits");

			byte[] arr = new byte[hex.Length >> 1];

			for (int i = 0; i < hex.Length >> 1; ++i)
			{
				arr[i] = (byte)((GetHexVal(hex[i << 1]) << 4) + (GetHexVal(hex[(i << 1) + 1])));
			}

			return arr;
		}

		private static int GetHexVal(char hex)
		{
			int val = hex;
			return val - (val < 58 ? 48 : (val < 97 ? 55 : 87));
		}
	}
}