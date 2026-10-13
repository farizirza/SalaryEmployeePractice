namespace Practice1;

class Program
{
    static void Main(string[] args)
    {
        int employeeCount = InputHandler.InputEmployeeCount();

        Employee[] employees = new Employee[employeeCount];
        decimal[] netSalaries = new decimal[employeeCount];

        PayrollProcessor.ProcessAllEmployees(employees, netSalaries);
        PayrollProcessor.DisplayPayrollSummary(employees, netSalaries);
        MenuHandler.ShowAdditionalMenu(employees, netSalaries);
    }
}