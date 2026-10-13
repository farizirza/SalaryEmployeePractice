namespace Practice1;

public class Employee
{
    public string Name = "";
    public int Tier;
    public int OvertimeHours;
    public bool IsMarried;


    private const decimal OVERTIME_RATE = 25000m;
    private const decimal MARRIAGE_ALLOWANCE_RATE = 0.10m;
    private const decimal BPJS_RATE = 0.03m;

    public void SetData(string name, int tier, int overtimeHours, bool isMarried)
    {
        Name = name;
        Tier = tier;
        OvertimeHours = overtimeHours;
        IsMarried = isMarried;
    }

    public decimal GetBaseSalary()
    {
        switch (Tier)
        {
            case 1:
                return 4000000m;
            case 2:
                return 5500000m;
            case 3:
                return 7000000m;
            default:
                return 0m;
        }
    }

    public decimal GetOvertimePay()
    {
        return OvertimeHours * OVERTIME_RATE;
    }

    public decimal GetDeduction(decimal gross)
    {
        return gross * BPJS_RATE;
    }

    public (decimal baseSalary, decimal allowance, decimal overtime, decimal gross, decimal deduction, decimal net) CalculateSlip()
    {
        decimal baseSalary = GetBaseSalary();
        decimal allowance = IsMarried ? baseSalary * MARRIAGE_ALLOWANCE_RATE : 0m;
        decimal overtime = GetOvertimePay();
        decimal gross = baseSalary + allowance + overtime;
        decimal deduction = GetDeduction(gross);
        decimal net = gross - deduction;

        return (baseSalary, allowance, overtime, gross, deduction, net);
    }

    public void PrintSlip(decimal baseSalary, decimal allowance, decimal overtime, decimal gross, decimal deduction, decimal net)
    {
        Console.WriteLine("========================================");
        Console.WriteLine("           SLIP GAJI KARYAWAN           ");
        Console.WriteLine("========================================");
        Console.WriteLine("Nama          : " + Name);
        Console.WriteLine("Golongan      : " + Tier);
        Console.WriteLine("Status        : " + (IsMarried ? "Menikah" : "Belum Menikah"));
        Console.WriteLine("Jam Lembur    : " + OvertimeHours);
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("Gaji Pokok    : Rp " + baseSalary.ToString("N0"));
        Console.WriteLine("Tunjangan     : Rp " + allowance.ToString("N0"));
        Console.WriteLine("Lembur        : Rp " + overtime.ToString("N0"));
        Console.WriteLine("Gaji Kotor    : Rp " + gross.ToString("N0"));
        Console.WriteLine("Potongan BPJS : Rp " + deduction.ToString("N0"));
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("GAJI BERSIH   : Rp " + net.ToString("N0"));
        Console.WriteLine("========================================");
        Console.WriteLine();
    }
}