namespace Bookworm_Desktop.UI
{
    static class StringExtensions
    {
        public static string ApplyRGMask(this string str)
        {
            string sstr(int a, int b) => str.Substring(a, b);
            return $"{sstr(0, 2)}.{sstr(2, 3)}.{sstr(5, 3)}-{sstr(8, 1)}";
        }

        public static string ApplyCPFMask(this string str)
        {
            string sstr(int a, int b) => str.Substring(a, b);
            return $"{sstr(0, 3)}.{sstr(3, 3)}.{sstr(6, 3)}-{sstr(8, 2)}";
        }

        public static string ApplyTelMask(this string str)
        {
            string sstr(int a, int b) => str.Substring(a, b);
            return $"({sstr(0, 2)}) {sstr(2, 5)}-{sstr(7, 4)}";
        }
    }
}
