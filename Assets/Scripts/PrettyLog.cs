namespace SMG
{
    public class PrettyLog
    {
        public static string GetMessage(string @class, string method, string message, object data)
        {
            return string.Format("<color=green>[{0}]</color>" +
                                 "<color=blue>[{1}]</color>" +
                                 "<b><color=#003653> {2} </color>" +
                                 "<color=red>{3}</color></b>", @class, method, message, data ?? string.Empty);
        }
    }
}