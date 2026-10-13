# Penjelasan Project Practice1: Kelas `Employe` dan `Program`

Dokumen ini menjelaskan kode dalam `Employee.cs` dan `Program.cs` secara berurutan. Penjelasan berbahasa Indonesia dan mencakup tujuan setiap pernyataan, alur program, serta cara perhitungan gaji.

## Gambaran umum aplikasi

Practice1 adalah aplikasi konsol penggajian sederhana. Program meminta jumlah karyawan (1 sampai 5), menerima data setiap karyawan, menghitung dan mencetak slip gaji, membuat rekap gaji bersih, lalu menyediakan menu untuk memfilter karyawan berdasarkan status pernikahan.

Rumus yang digunakan:

- Gaji pokok ditentukan oleh golongan.
- Tunjangan menikah = 10% dari gaji pokok jika karyawan menikah.
- Upah lembur = jumlah jam lembur × Rp25.000.
- Gaji kotor = gaji pokok + tunjangan + upah lembur.
- Potongan BPJS = 3% dari gaji kotor.
- Gaji bersih = gaji kotor − potongan BPJS.

---

## `SalaryEmployee/Employee.cs`

### Namespace dan deklarasi kelas

`namespace Practice1;`

Menempatkan tipe-tipe di file ini ke dalam namespace `Practice1`. Namespace membantu mengelompokkan kode dan mencegah benturan nama dengan tipe lain.

`public class Employe`

Mendeklarasikan kelas publik bernama `Employe`. Kelas ini merepresentasikan satu karyawan beserta data dan operasi terkait perhitungan gajinya. Ejaan `Employe` digunakan oleh kode saat ini; nama bahasa Inggris yang umum adalah `Employee`.

### Data karyawan

`public string Name = "";`

Membuat field publik `Name` untuk menyimpan nama karyawan. Nilai awalnya string kosong, bukan `null`.

`public int Tier;`

Field untuk menyimpan golongan karyawan, yang pada program ini diharapkan bernilai 1, 2, atau 3.

`public int OvertimeHours;`

Field untuk jumlah jam lembur. Program membatasi nilai input dari 0 sampai 40.

`public bool IsMarried;`

Field status pernikahan. `true` berarti menikah dan `false` berarti belum menikah.

Field-field ini bersifat `public`, sehingga kode lain dapat membaca atau mengubahnya secara langsung. Metode input program memakai `SetData` untuk mengisinya.

### Konstanta perhitungan

`private const decimal OVERTIME_RATE = 25000m;`

Konstanta privat untuk tarif lembur sebesar Rp25.000 per jam. `private` membatasi akses ke dalam kelas ini; `const` menyatakan nilainya tetap; akhiran `m` membuat literal bertipe `decimal`.

`private const decimal MARRIAGE_ALLOWANCE_RATE = 0.10m;`

Konstanta tunjangan menikah sebesar 0,10 atau 10% dari gaji pokok.

`private const decimal BPJS_RATE = 0.03m;`

Konstanta potongan BPJS sebesar 0,03 atau 3% dari gaji kotor.

Tipe `decimal` digunakan untuk nominal uang agar perhitungan desimal lebih sesuai untuk nilai finansial daripada tipe floating-point seperti `double`.

### `SetData`

`public void SetData(string name, int tier, int overtimeHours, bool isMarried)`

Mendeklarasikan metode publik yang menerima empat nilai data karyawan. `void` berarti metode ini tidak mengembalikan hasil.

- `Name = name;` menyalin parameter nama ke field `Name`.
- `Tier = tier;` menyalin golongan ke field `Tier`.
- `OvertimeHours = overtimeHours;` menyimpan jumlah jam lembur.
- `IsMarried = isMarried;` menyimpan status pernikahan.

Metode ini dipanggil setelah objek `Employe` dibuat agar field-fieldnya terisi.

### `GetBaseSalary`

`public decimal GetBaseSalary()`

Metode yang menentukan dan mengembalikan gaji pokok berdasarkan nilai field `Tier`.

`switch (Tier)`

Memilih cabang berdasarkan golongan karyawan.

- `case 1: return 4000000m;` mengembalikan Rp4.000.000 untuk golongan 1.
- `case 2: return 5500000m;` mengembalikan Rp5.500.000 untuk golongan 2.
- `case 3: return 7000000m;` mengembalikan Rp7.000.000 untuk golongan 3.
- `default: return 0m;` mengembalikan nol bila nilai golongan di luar kasus yang ditentukan.

Input normal telah divalidasi di `Program`, tetapi cabang `default` tetap menjadi perilaku cadangan jika objek diisi dengan nilai yang tidak valid dari tempat lain.

### `GetOvertimePay`

`public decimal GetOvertimePay()`

Metode untuk menghitung upah lembur.

`return OvertimeHours * OVERTIME_RATE;`

Mengalikan jumlah jam lembur dengan tarif tetap Rp25.000 per jam dan mengembalikan hasilnya sebagai `decimal`.

### `GetDeduction`

`public decimal GetDeduction(decimal gross)`

Metode menerima nilai gaji kotor melalui parameter `gross` dan mengembalikan nilai potongan.

`return gross * BPJS_RATE;`

Menghitung 3% dari gaji kotor. Metode ini hanya menghitung potongan; hasilnya belum dikurangkan dari gaji kotor di sini.

### `CalculateSlip`

`public (decimal baseSalary, decimal allowance, decimal overtime, decimal gross, decimal deduction, decimal net) CalculateSlip()`

Mendeklarasikan metode yang menghitung seluruh komponen slip. Nilai kembali berupa tuple dengan enam elemen `decimal` bernama. Nama elemen memungkinkan pemanggil membaca hasil seperti `result.net` dan `result.gross`.

`decimal baseSalary = GetBaseSalary();`

Mengambil gaji pokok berdasarkan golongan.

`decimal allowance = IsMarried ? baseSalary * MARRIAGE_ALLOWANCE_RATE : 0m;`

Operator kondisional `? :` memilih hasil berdasarkan status pernikahan. Jika `IsMarried` bernilai `true`, tunjangan adalah 10% gaji pokok; jika `false`, tunjangan nol.

`decimal overtime = GetOvertimePay();`

Menghitung total upah lembur melalui metode `GetOvertimePay`.

`decimal gross = baseSalary + allowance + overtime;`

Menjumlahkan gaji pokok, tunjangan, dan lembur menjadi gaji kotor.

`decimal deduction = GetDeduction(gross);`

Menghitung potongan 3% dari gaji kotor.

`decimal net = gross - deduction;`

Menghitung gaji bersih setelah potongan.

`return (baseSalary, allowance, overtime, gross, deduction, net);`

Mengembalikan enam nilai sebagai tuple. Urutannya sesuai dengan nama elemen tuple pada deklarasi metode.

### `PrintSlip`

`public void PrintSlip(decimal baseSalary, decimal allowance, decimal overtime, decimal gross, decimal deduction, decimal net)`

Metode ini menerima enam nilai hasil perhitungan lalu menampilkannya. Ia mengakses `Name`, `Tier`, `IsMarried`, dan `OvertimeHours` langsung dari objek.

Setiap `Console.WriteLine(...)` menuliskan satu baris ke konsol. Garis sama dengan dan tanda minus membentuk batas visual, sedangkan teks seperti `SLIP GAJI KARYAWAN` dan `Gaji Pokok` adalah label.

- `"Nama          : " + Name` menampilkan nama karyawan.
- `"Golongan      : " + Tier` menampilkan nomor golongan.
- `"Status        : " + (IsMarried ? "Menikah" : "Belum Menikah")` mengubah nilai boolean menjadi label yang mudah dibaca.
- `"Jam Lembur    : " + OvertimeHours` menampilkan jumlah jam lembur.
- Baris gaji menampilkan masing-masing nominal yang diterima sebagai parameter.
- `ToString("N0")` memformat angka sebagai angka dengan pemisah kelompok ribuan dan tanpa angka desimal. Tanda pemisah yang dipakai bergantung pada kultur aktif komputer.
- Awalan `Rp ` adalah teks yang menunjukkan mata uang rupiah; format angka tidak mengatur simbol mata uang secara otomatis.
- `Console.WriteLine();` terakhir menambahkan satu baris kosong setelah slip agar tampilan berikutnya terpisah.

---

## `SalaryEmployee/Program.cs`

### Namespace, kelas, dan titik masuk

`namespace Practice1;`

Menempatkan kelas `Program` di namespace yang sama dengan `Employe`, sehingga kelas tersebut dapat digunakan tanpa penulisan nama namespace lengkap.

`class Program`

Mendeklarasikan kelas yang menjadi wadah kode utama aplikasi. Tidak ditulis `public`, sehingga aksesibilitas kelas mengikuti default kelas top-level, yaitu `internal`.

`static void Main(string[] args)`

Metode `Main` adalah titik masuk aplikasi konsol.

- `static` berarti metode dapat dipanggil tanpa membuat objek `Program`.
- `void` berarti metode tidak mengembalikan nilai.
- `string[] args` dapat berisi argumen baris perintah; kode ini belum menggunakannya.

### Batas jumlah karyawan dan validasi input

`const int MIN_EMPLOYEES = 1;` dan `const int MAX_EMPLOYEES = 5;`

Menentukan jumlah karyawan minimum dan maksimum yang diterima. Karena konstan, nilainya tidak dapat diubah selama program berjalan.

`int employeeCount;`

Mendeklarasikan variabel untuk menyimpan jumlah karyawan yang dimasukkan. Nilainya diisi ketika input berhasil diproses oleh `int.TryParse`.

`do { ... } while (...);`

Menjalankan isi blok setidaknya sekali, lalu mengulang selama jumlah karyawan di luar batas.

`Console.Write("Jumlah karyawan (1-5): ");`

Menampilkan pertanyaan tanpa berpindah baris setelahnya, sehingga input pengguna berada pada baris yang sama.

`string input = Console.ReadLine() ?? "";`

Membaca satu baris input. `Console.ReadLine()` dapat menghasilkan `null` saat input berakhir; operator null-coalescing `??` menggantinya dengan string kosong.

`if (!int.TryParse(input, out employeeCount) || employeeCount < MIN_EMPLOYEES || employeeCount > MAX_EMPLOYEES)`

Memeriksa tiga kondisi:

1. `int.TryParse` gagal mengubah teks menjadi bilangan bulat (`!` berarti membalik hasil boolean).
2. Jumlah kurang dari minimum.
3. Jumlah lebih dari maksimum.

Operator `||` berarti cukup salah satu kondisi bernilai benar agar seluruh kondisi `if` benar. Parameter `out` membuat hasil konversi ditaruh ke `employeeCount`.

`Console.WriteLine("Input tidak valid. Masukkan angka 1-5.");`

Memberi tahu pengguna bahwa input harus bilangan bulat dalam rentang 1 sampai 5.

Kondisi pada akhir `do-while` mengulang bila `employeeCount` kurang dari 1 atau lebih dari 5. Jika parsing gagal, `TryParse` menetapkan nilai `out` ke nilai default integer (0), sehingga kondisi pengulangan tetap meminta input lagi.

### Membuat array

`Employe[] employees = new Employe[employeeCount];`

Membuat array dengan kapasitas sesuai jumlah karyawan. Setiap elemen akan menyimpan referensi ke objek `Employe`.

`decimal[] netSalaries = new decimal[employeeCount];`

Membuat array berukuran sama untuk menyimpan gaji bersih setiap karyawan. Elemen array `decimal` yang belum diisi bernilai nol.

### Memproses setiap karyawan

`for (int i = 0; i < employeeCount; i++)`

Mengulangi blok untuk setiap karyawan. `i` dimulai dari 0 dan bertambah satu sampai `i` mencapai `employeeCount - 1`. Indeks array C# dimulai dari nol.

`Console.WriteLine();`

Mencetak baris kosong sebagai pemisah visual.

`Console.WriteLine("=== Karyawan ke-" + (i + 1) + " ===");`

Menampilkan nomor karyawan yang sedang dimasukkan. `i + 1` dipakai karena nomor untuk pengguna biasanya dimulai dari 1, bukan 0.

`Console.Write("Nama: ");`

Meminta nama tanpa mengakhiri baris.

`string name = Console.ReadLine() ?? "";`

Membaca nama dan mengganti `null` dengan string kosong. Nama tidak memiliki validasi tambahan; input kosong dapat diterima.

#### Validasi golongan

`int tier;`

Mendeklarasikan variabel untuk golongan.

Loop `do-while` berikut meminta golongan sampai valid.

`Console.Write("Golongan (1-3): ");`

Menampilkan permintaan golongan.

`string input = Console.ReadLine() ?? "";`

Membaca input dan menangani hasil `null`.

`if (!int.TryParse(input, out tier) || tier < 1 || tier > 3)`

Menolak teks yang bukan bilangan bulat serta angka di luar rentang 1–3. Jika parsing gagal, `tier` menjadi 0 sehingga syarat pengulangan juga tetap terpenuhi.

`Console.WriteLine("Input tidak valid. Masukkan angka 1-3.");`

Menampilkan pesan kesalahan.

Kondisi `while (tier < 1 || tier > 3)` mengulang selama nilainya di luar rentang. Setelah loop selesai, `tier` berada pada rentang valid.

#### Validasi jam lembur

`int overtimeHours;`

Mendeklarasikan jumlah jam lembur.

Loop `do-while` memiliki struktur yang sama dengan validasi golongan.

`Console.Write("Jam lembur (0-40): ");`

Meminta jumlah jam lembur.

`string input = Console.ReadLine() ?? "";`

Membaca teks input dan menangani `null`.

`if (!int.TryParse(input, out overtimeHours) || overtimeHours < 0 || overtimeHours > 40)`

Memastikan input berupa bilangan bulat dalam rentang 0–40.

`Console.WriteLine("Input tidak valid. Masukkan angka 0-40.");`

Memberi pesan bila validasi gagal.

Kondisi pengulangan `overtimeHours < 0 || overtimeHours > 40` menyebabkan prompt muncul kembali sampai nilainya valid.

#### Validasi status menikah

`bool isMarried;`

Mendeklarasikan variabel status pernikahan. Nilainya ditentukan di dalam loop sebelum dipakai.

`while (true)`

Membuat loop yang terus berulang sampai salah satu cabang valid menjalankan `break`.

`Console.Write("Status menikah (y/n): ");`

Meminta pengguna memasukkan `y` atau `n`.

`string input = (Console.ReadLine() ?? "").ToLower();`

Membaca input, mengganti `null` dengan string kosong, lalu mengubahnya ke huruf kecil. Dengan begitu, `Y` juga diterima sebagai `y`. `Trim()` tidak dipakai, sehingga spasi di awal atau akhir dapat membuat input tidak cocok.

`if (input == "y")`

Jika input tepat `y`, menetapkan `isMarried = true`.

`break;`

Menghentikan loop status setelah nilai valid ditemukan.

`else if (input == "n")`

Jika input tepat `n`, menetapkan `isMarried = false`.

`else`

Menangani input selain `y` dan `n`.

`Console.WriteLine("Input tidak valid. Masukkan 'y' atau 'n'.");`

Menjelaskan format input yang diterima. Karena belum ada `break` pada cabang ini, loop berulang lagi.

#### Membuat objek dan menghitung slip

`Console.WriteLine();`

Mencetak pemisah kosong sebelum hasil karyawan.

`Employe employee = new Employe();`

Membuat objek baru untuk mewakili karyawan yang sedang diproses.

`employee.SetData(name, tier, overtimeHours, isMarried);`

Mengisi field objek dengan input yang sudah divalidasi.

`employees[i] = employee;`

Menyimpan referensi objek ke posisi `i` di array karyawan agar dapat dipakai pada rekap dan menu.

`var result = employee.CalculateSlip();`

Menghitung slip. `var` membuat compiler menyimpulkan tipe variabel dari hasil metode; dalam hal ini tipenya adalah tuple bernama dengan enam nilai `decimal`.

`employee.PrintSlip(result.baseSalary, result.allowance, result.overtime, result.gross, result.deduction, result.net);`

Mengirim keenam komponen hasil perhitungan ke metode yang mencetak slip. Nama elemen tuple membuat setiap nilai mudah dikenali.

`netSalaries[i] = result.net;`

Menyimpan gaji bersih pada indeks yang sama dengan karyawan di array `employees`.

### Rekap payroll

Setelah loop karyawan selesai, program menampilkan bagian rekap.

`Console.WriteLine();`

Memberi jarak sebelum judul rekap.

Tiga pemanggilan `Console.WriteLine` berikut mencetak garis pembatas dan judul `REKAP PAYROLL`.

`decimal totalPayroll = 0;`

Menginisialisasi akumulator total gaji bersih dengan nol.

`for (int i = 0; i < employeeCount; i++)`

Mengunjungi semua karyawan dan nilai gaji bersihnya.

`Console.WriteLine(employees[i].Name + " : Rp " + netSalaries[i].ToString("N0"));`

Menampilkan nama karyawan dan gaji bersihnya yang diformat tanpa desimal.

`totalPayroll += netSalaries[i];`

Menambahkan gaji bersih karyawan saat ini ke total. Ini sama artinya dengan `totalPayroll = totalPayroll + netSalaries[i]`.

Baris-baris `Console.WriteLine` setelah loop menampilkan garis pemisah dan total payroll yang diformat dengan `N0`.

`decimal average = totalPayroll / employeeCount;`

Menghitung rata-rata gaji bersih. Pembagian aman dari nol karena jumlah karyawan sudah divalidasi minimal 1.

`Console.WriteLine("Rata-rata Gaji Bersih: Rp " + average.ToString("N0"));`

Menampilkan rata-rata tanpa angka desimal. Format `N0` membulatkan tampilan ke angka bulat; nilai internal `average` tetap bertipe `decimal`.

### Mencari gaji tertinggi

`int highestIndex = 0;`

Menganggap karyawan pertama sebagai pemilik gaji tertinggi sementara. Jumlah karyawan minimal 1, jadi indeks 0 tersedia.

`for (int i = 1; i < employeeCount; i++)`

Memulai pencarian dari karyawan kedua karena karyawan pertama sudah menjadi nilai pembanding awal.

`if (netSalaries[i] > netSalaries[highestIndex])`

Membandingkan gaji bersih saat ini dengan gaji tertinggi yang ditemukan sejauh ini.

`highestIndex = i;`

Memperbarui indeks tertinggi bila gaji saat ini lebih besar. Jika ada nilai seri, indeks yang pertama ditemukan tetap dipertahankan karena perbandingan memakai `>` bukan `>=`.

Baris `Console.WriteLine` sesudah loop menampilkan nama karyawan pada `highestIndex` dan nominal gaji bersih tertinggi.

### Menu tambahan

`while (true)`

Memulai loop menu yang terus ditampilkan sampai pengguna memilih opsi keluar.

Beberapa `Console.WriteLine` berikut mencetak garis, judul menu, dan pilihan:

1. Tampilkan karyawan yang menikah.
2. Tampilkan karyawan yang belum menikah.
3. Keluar.

`Console.Write("Pilih (1-3): ");`

Meminta pilihan pengguna.

`string choice = (Console.ReadLine() ?? "").Trim();`

Membaca pilihan, mengganti `null` dengan string kosong, lalu menghapus spasi di awal dan akhir. Berbeda dengan input status pernikahan, pilihan menu menerima spasi di sekitar angka karena menggunakan `Trim()`.

`Console.WriteLine();`

Mencetak baris kosong untuk memisahkan prompt dari hasil.

#### Pilihan 1 atau 2

`if (choice == "1" || choice == "2")`

Memeriksa apakah pengguna memilih salah satu opsi filter status.

`bool searchMarried = choice == "1";`

Mengubah pilihan menjadi boolean pencarian. Opsi 1 menghasilkan `true` (cari yang menikah); opsi 2 menghasilkan `false` (cari yang belum menikah).

`string label = searchMarried ? "SUDAH MENIKAH" : "BELUM MENIKAH";`

Membuat label tampilan menggunakan operator kondisional berdasarkan nilai `searchMarried`.

Tiga pemanggilan `Console.WriteLine` sesudahnya mencetak pembatas dan judul kategori yang akan ditampilkan.

`bool found = false;`

Menandai bahwa belum ada karyawan yang cocok ditemukan.

`for (int i = 0; i < employeeCount; i++)`

Memeriksa seluruh karyawan.

`if (employees[i].IsMarried == searchMarried)`

Membandingkan status pernikahan karyawan dengan status yang sedang dicari.

`Console.WriteLine("- " + employees[i].Name);`

Menampilkan nama karyawan yang cocok.

`found = true;`

Mengingat bahwa setidaknya satu hasil telah ditemukan.

`if (!found)`

Memeriksa apakah tidak ada hasil (`!` membalik nilai boolean).

`Console.WriteLine("Tidak ada karyawan " + label.ToLower());`

Menampilkan pesan bila daftar hasil kosong. `ToLower()` mengubah label kategori menjadi huruf kecil.

Baris pembatas setelahnya menutup tampilan hasil filter.

#### Pilihan keluar dan pilihan tidak valid

`else if (choice == "3")`

Menangani pilihan keluar.

`Console.WriteLine("Terima kasih. Program selesai.");`

Menampilkan pesan penutup.

`break;`

Mengakhiri loop menu. Setelah keluar dari loop, metode `Main` selesai dan program berakhir.

`else`

Menangani semua input menu selain `1`, `2`, dan `3`.

`Console.WriteLine("Pilihan tidak valid. Masukkan 1-3.");`

Menampilkan pesan kesalahan. Karena tidak ada `break`, loop menu kembali ditampilkan.

---

## Contoh perhitungan

Misalnya karyawan bergolongan 2, bekerja lembur 4 jam, dan menikah:

1. Gaji pokok = Rp5.500.000.
2. Tunjangan menikah = 10% × Rp5.500.000 = Rp550.000.
3. Lembur = 4 × Rp25.000 = Rp100.000.
4. Gaji kotor = Rp5.500.000 + Rp550.000 + Rp100.000 = Rp6.150.000.
5. Potongan BPJS = 3% × Rp6.150.000 = Rp184.500.
6. Gaji bersih = Rp6.150.000 − Rp184.500 = Rp5.965.500.

## Ringkasan alur program

1. Meminta jumlah karyawan dan memvalidasi angka 1–5.
2. Membuat array untuk objek karyawan dan gaji bersih.
3. Untuk setiap karyawan, meminta nama, golongan, jam lembur, dan status menikah dengan validasi.
4. Membuat objek, menghitung slip, mencetak slip, dan menyimpan gaji bersih.
5. Menampilkan daftar gaji bersih, total, rata-rata, dan gaji tertinggi.
6. Menampilkan menu untuk memfilter status pernikahan atau keluar.
