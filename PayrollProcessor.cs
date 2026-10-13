namespace Practice1;

public static class PayrollProcessor
{

     /* 
      * Memproses input dan perhitungan slip gaji untuk semua karyawan.
      * Mengisi array employees dan netSalaries dengan data yang sudah dihitung.
      */
    public static void ProcessAllEmployees(Employee[] employees, decimal[] netSalaries)
    {
        for (int i = 0; i < employees.Length; i++)
        {
            Console.WriteLine();
            Console.WriteLine("=== Karyawan ke-" + (i + 1) + " ===");

            // Input data karyawan melalui InputHandler
            var (name, tier, overtimeHours, isMarried) = InputHandler.InputEmployeeData();

            Console.WriteLine();

            // Membuat dan mengisi data karyawan
            Employee employee = new Employee();
            employee.SetData(name, tier, overtimeHours, isMarried);
            employees[i] = employee;

            // Menghitung slip gaji
            var result = employee.CalculateSlip();

            // Menampilkan slip gaji
            employee.PrintSlip(result.baseSalary, result.allowance, result.overtime, result.gross, result.deduction, result.net);

            // Menyimpan gaji bersih untuk rekap
            netSalaries[i] = result.net;
        }
    }


    /*
     * Menampilkan rekap payroll yang berisi
     * - Daftar gaji bersih setiap 
     * - Total 
     * - Rata-rata gaji 
     * - Gaji tertinggi dan nama pemiliknya
    */
    public static void DisplayPayrollSummary(Employee[] employees, decimal[] netSalaries)
    {
        Console.WriteLine();
        Console.WriteLine("========================================");
        Console.WriteLine("           REKAP PAYROLL                ");
        Console.WriteLine("========================================");

        // Menampilkan daftar gaji bersih dan menghitung total
        decimal totalPayroll = 0;
        for (int i = 0; i < employees.Length; i++)
        {
            Console.WriteLine(employees[i].Name + " : Rp " + netSalaries[i].ToString("N0"));
            totalPayroll += netSalaries[i];
        }

        // Menampilkan total payroll
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("Total Payroll: Rp " + totalPayroll.ToString("N0"));

        // Menampilkan rata-rata gaji bersih
        decimal average = totalPayroll / employees.Length;
        Console.WriteLine("Rata-rata Gaji Bersih: Rp " + average.ToString("N0"));

        // Mencari gaji tertinggi
        int highestIndex = 0;
        for (int i = 1; i < employees.Length; i++)
        {
            if (netSalaries[i] > netSalaries[highestIndex])
            {
                highestIndex = i;
            }
        }

        // Menampilkan gaji tertinggi
        Console.WriteLine("Gaji Tertinggi: " + employees[highestIndex].Name + " - Rp " + netSalaries[highestIndex].ToString("N0"));
        Console.WriteLine("========================================");
    }
}
