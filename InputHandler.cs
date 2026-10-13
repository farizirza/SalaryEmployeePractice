namespace Practice1;

public static class InputHandler
{
    private const int MIN_EMPLOYEES = 1;
    private const int MAX_EMPLOYEES = 5;
    private const int MIN_TIER = 1;
    private const int MAX_TIER = 3;
    private const int MIN_OVERTIME = 0;
    private const int MAX_OVERTIME = 40;

    // Meminta dan memvalidasi jumlah karyawan (1-5).
    public static int InputEmployeeCount()
    {
        int employeeCount;
        do
        {
            Console.Write("Jumlah karyawan (1-5): ");
            string input = Console.ReadLine() ?? "";

            // Guard clause: false-first pattern
            if (!int.TryParse(input, out employeeCount) || employeeCount < MIN_EMPLOYEES || employeeCount > MAX_EMPLOYEES)
            {
                Console.WriteLine("Input tidak valid. Masukkan angka 1-5.");
                continue;
            }

            break;
        } while (true);

        return employeeCount;
    }

    
    // Meminta nama karyawan tanpa validasi khusus.
    public static string InputEmployeeName()
    {
        Console.Write("Nama: ");
        return Console.ReadLine() ?? "";
    }

    // Meminta dan memvalidasi golongan karyawan (1-3).
    public static int InputTier()
    {
        int tier;
        do
        {
            Console.Write("Golongan (1-3): ");
            string input = Console.ReadLine() ?? "";

            // Guard clause: false-first pattern
            if (!int.TryParse(input, out tier) || tier < MIN_TIER || tier > MAX_TIER)
            {
                Console.WriteLine("Input tidak valid. Masukkan angka 1-3.");
                continue;
            }

            break;
        } while (true);

        return tier;
    }

    // Meminta dan memvalidasi jam lembur (0-40).
    public static int InputOvertimeHours()
    {
        int overtimeHours;
        do
        {
            Console.Write("Jam lembur (0-40): ");
            string input = Console.ReadLine() ?? "";

            // Guard clause: false-first pattern
            if (!int.TryParse(input, out overtimeHours) || overtimeHours < MIN_OVERTIME || overtimeHours > MAX_OVERTIME)
            {
                Console.WriteLine("Input tidak valid. Masukkan angka 0-40.");
                continue;
            }

            break;
        } while (true);

        return overtimeHours;
    }

    // Meminta dan memvalidasi status pernikahan (y/n) dengan false-first pattern.
    public static bool InputMarriedStatus()
    {
        while (true)
        {
            Console.Write("Status menikah (y/n): ");
            string input = (Console.ReadLine() ?? "").ToLower();

            // Guard clause: false-first pattern - periksa invalid dulu
            if (input != "y" && input != "n")
            {
                Console.WriteLine("Input tidak valid. Masukkan 'y' atau 'n'.");
                continue;
            }

            // Happy path: return boolean
            return input == "y";
        }
    }

    /*
     * Wrapper untuk input semua data karyawan dalam satu method.
     * Mengembalikan tuple berisi nama, tier, jam lembur, dan status menikah.
     */
    public static (string name, int tier, int overtimeHours, bool isMarried) InputEmployeeData()
    {
        string name = InputEmployeeName();
        int tier = InputTier();
        int overtimeHours = InputOvertimeHours();
        bool isMarried = InputMarriedStatus();

        return (name, tier, overtimeHours, isMarried);
    }
}
