namespace LetsCode_APIProject_Investors;

public class Investors
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string InvestorProfile { get; set; } = string.Empty;
    public decimal Balance { get; set; }
    public string PreferredStock { get; set; } = string.Empty;
}