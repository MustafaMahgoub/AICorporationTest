using System.Text;
public static class Utils
{    
    public static string BuildString(
                        string m_szFirstVariable,
                        string m_szSecondVariable,
                        string m_szThirdVariable,
                        string m_szForthVariable,
                        string m_szFifthVariable,
                        string m_szSixthVariable,
                        string m_szSeventhVariable,
                        string m_szEighthVariable)
    {
        StringBuilder builder = new StringBuilder();
        builder.Append(m_szFirstVariable);
        builder.Append(m_szSecondVariable);
        builder.Append(m_szThirdVariable);
        builder.Append(m_szForthVariable);
        builder.Append(m_szFifthVariable);
        builder.Append(m_szSixthVariable);
        builder.Append(m_szSeventhVariable);
        builder.Append(m_szEighthVariable);
        return builder.ToString();
    }
}
