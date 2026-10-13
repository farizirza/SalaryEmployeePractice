namespace Practice1;

public static class MenuHandler
{
    
    /*
     * Menampilkan menu tambahan untuk filter karyawan berdasarkan status pernikahan
     * Menu terus ditampilkan sampai user memilih keluar (opsi 3
     * Menerapkan false-first pattern: periksa keluar dulu, filter, lalu invalid.
     */

    public static void ShowAdditionalMenu(Employee[] employees, decimal[] netSalaries)
    {
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("========================================");
            Console.WriteLine("           MENU TAMBAHAN               ");
            Console.WriteLine("========================================");
            Console.WriteLine("1. Lihat karyawan sudah menikah");
            Console.WriteLine("2. Lihat karyawan belum menikah");
            Console.WriteLine("3. Keluar");
            Console.WriteLine("----------------------------------------");
            Console.Write("Pilih (1-3): ");

            string choice = (Console.ReadLine() ?? "").Trim();
            Console.WriteLine();

            // False-first pattern: periksa keluar dulu
            if (choice == "3")
            {
                Console.WriteLine("Terima kasih. Program selesai.");
                break;
            }

            // Periksa opsi filter valid
            if (choice is "1" or "2")
            {
                DisplayEmployeesByMaritalStatus(employees, choice == "1");
                continue;
            }

            // Pilihan tidak valid
            Console.WriteLine("Pilihan tidak valid. Masukkan 1-3.");
        }
    }

    
    /*
     * Helper method untuk menampilkan karyawan berdasarkan status pernikahan
     * Parameter searchMarried: true = cari yang menikah, false = cari yang belum menikah.
     */

    private static void DisplayEmployeesByMaritalStatus(Employee[] employees, bool searchMarried)
    {
        string label = searchMarried ? "SUDAH MENIKAH" : "BELUM MENIKAH";

        Console.WriteLine("========================================");
        Console.WriteLine("     KARYAWAN " + label);
        Console.WriteLine("========================================");

        bool found = false;
        for (int i = 0; i < employees.Length; i++)
        {
            if (employees[i].IsMarried == searchMarried)
            {
                Console.WriteLine("- " + employees[i].Name);
                found = true;
            }
        }

        // Guard clause: jika tidak ada yang sesuai, tampilkan pesan
        if (!found)
        {
            Console.WriteLine("Tidak ada karyawan " + label.ToLower());
        }

        Console.WriteLine("========================================");
    }
}
