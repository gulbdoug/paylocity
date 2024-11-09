namespace Api.Dtos.Payroll
{
    public interface IDeductionDto
    {
        string Name { get; set; }
        decimal Amount { get; set; }
    }
}