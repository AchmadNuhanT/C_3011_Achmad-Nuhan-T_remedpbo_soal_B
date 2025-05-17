using System;
using System.Collections.Generic;
using System.Linq;

public class Book
{
    public int ID { get; set; }
    public string Judul { get; set; }
    public string Penulis { get; set; }
    public int TahunTerbit { get; set; }
    public bool Status { get; set; } 

    public Book(int id, string judul, string penulis, int tahunTerbit)
    {
        ID = id;
        Judul = judul;
        Penulis = penulis;
        TahunTerbit = tahunTerbit;
        Status = true; 
    }

    public virtual void DisplayInfo()
    {
        Console.WriteLine($"ID: {ID}");
        Console.WriteLine($"Judul: {Judul}");
        Console.WriteLine($"Penulis: {Penulis}");
        Console.WriteLine($"Tahun Terbit: {TahunTerbit}");
        Console.WriteLine($"Status: {(Status ? "Tersedia" : "Dipinjam")}");
        Console.WriteLine();
    }
}

public class ReferenceBook : Book
{
    public string Kategori { get; set; }

    public ReferenceBook(int id, string judul, string penulis, int tahunTerbit, string kategori)
        : base(id, judul, penulis, tahunTerbit)
    {
        Kategori = kategori;
    }

    public override void DisplayInfo()
    {
        base.DisplayInfo();
        Console.WriteLine($"Kategori: {Kategori}");
        Console.WriteLine();
    }
}

public class Perpustakaan
{
    public string Nama { get; set; }
    public string Alamat { get; set; }
    private List<Book> KoleksiBuku { get; set; }

    public Perpustakaan(string nama, string alamat)
    {
        Nama = nama;
        Alamat = alamat;
        KoleksiBuku = new List<Book>();
    }

    public void TambahBuku(Book buku)
    {
        KoleksiBuku.Add(buku);
        Console.WriteLine("Buku berhasil ditambahkan!");
    }

    public void TampilkanSemuaBuku()
    {
        if (KoleksiBuku.Count == 0)
        {
            Console.WriteLine("Belum ada buku dalam koleksi.");
            return;
        }

        Console.WriteLine($"\nDaftar Buku di {Nama}:");
        foreach (var buku in KoleksiBuku)
        {
            buku.DisplayInfo();
        }
    }

    public Book CariBukuByID(int id)
    {
        return KoleksiBuku.FirstOrDefault(b => b.ID == id);
    }

    public List<Book> CariBukuByJudul(string judul)
    {
        return KoleksiBuku.Where(b => b.Judul.ToLower().Contains(judul.ToLower())).ToList();
    }

    public void UpdateBuku(int id, string judul, string penulis, int tahunTerbit)
    {
        var buku = CariBukuByID(id);
        if (buku != null)
        {
            buku.Judul = judul;
            buku.Penulis = penulis;
            buku.TahunTerbit = tahunTerbit;
            Console.WriteLine("Informasi buku berhasil diupdate!");
        }
        else
        {
            Console.WriteLine("Buku tidak ditemukan.");
        }
    }

    public void HapusBuku(int id)
    {
        var buku = CariBukuByID(id);
        if (buku != null)
        {
            KoleksiBuku.Remove(buku);
            Console.WriteLine("Buku berhasil dihapus!");
        }
        else
        {
            Console.WriteLine("Buku tidak ditemukan.");
        }
    }

    public void PinjamBuku(int id)
    {
        var buku = CariBukuByID(id);
        if (buku != null)
        {
            if (buku.Status)
            {
                buku.Status = false;
                Console.WriteLine("Buku berhasil dipinjam!");
            }
            else
            {
                Console.WriteLine("Buku sedang tidak tersedia.");
            }
        }
        else
        {
            Console.WriteLine("Buku tidak ditemukan.");
        }
    }

    public void KembalikanBuku(int id)
    {
        var buku = CariBukuByID(id);
        if (buku != null)
        {
            if (!buku.Status)
            {
                buku.Status = true;
                Console.WriteLine("Buku berhasil dikembalikan!");
            }
            else
            {
                Console.WriteLine("Buku ini tidak sedang dipinjam.");
            }
        }
        else
        {
            Console.WriteLine("Buku tidak ditemukan.");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
      
        Perpustakaan perpustakaan = new Perpustakaan("Perpustakaan Kota", "Jl. Merdeka No. 1");

       
        perpustakaan.TambahBuku(new Book(1, "Pemrograman C#", "John Doe", 2020));
        perpustakaan.TambahBuku(new Book(2, "Struktur Data", "Jane Smith", 2019));
        perpustakaan.TambahBuku(new ReferenceBook(3, "Ensiklopedia Sains", "Tim Penulis", 2021, "Sains"));

        bool isRunning = true;
        while (isRunning)
        {
            Console.WriteLine("\n=== Sistem Manajemen Perpustakaan ===");
            Console.WriteLine("1. Tambah Buku");
            Console.WriteLine("2. Tampilkan Semua Buku");
            Console.WriteLine("3. Cari Buku (ID)");
            Console.WriteLine("4. Cari Buku (Judul)");
            Console.WriteLine("5. Update Buku");
            Console.WriteLine("6. Hapus Buku");
            Console.WriteLine("7. Pinjam Buku");
            Console.WriteLine("8. Kembalikan Buku");
            Console.WriteLine("9. Keluar");
            Console.Write("Pilih menu: ");

            string input = Console.ReadLine();
            Console.Clear();

            switch (input)
            {
                case "1": // Tambah buku
                    Console.WriteLine("=== Tambah Buku Baru ===");
                    Console.Write("ID: ");
                    int id = int.Parse(Console.ReadLine());
                    Console.Write("Judul: ");
                    string judul = Console.ReadLine();
                    Console.Write("Penulis: ");
                    string penulis = Console.ReadLine();
                    Console.Write("Tahun Terbit: ");
                    int tahun = int.Parse(Console.ReadLine());

                    Console.Write("Apakah buku referensi? (y/n): ");
                    if (Console.ReadLine().ToLower() == "y")
                    {
                        Console.Write("Kategori: ");
                        string kategori = Console.ReadLine();
                        perpustakaan.TambahBuku(new ReferenceBook(id, judul, penulis, tahun, kategori));
                    }
                    else
                    {
                        perpustakaan.TambahBuku(new Book(id, judul, penulis, tahun));
                    }
                    break;

                case "2": 
                    perpustakaan.TampilkanSemuaBuku();
                    break;

                case "3": 
                    Console.Write("Masukkan ID buku: ");
                    int searchId = int.Parse(Console.ReadLine());
                    Book foundBook = perpustakaan.CariBukuByID(searchId);
                    if (foundBook != null)
                    {
                        foundBook.DisplayInfo();
                    }
                    else
                    {
                        Console.WriteLine("Buku tidak ditemukan.");
                    }
                    break;

                case "4": 
                    Console.Write("Masukkan judul buku: ");
                    string searchJudul = Console.ReadLine();
                    List<Book> foundBooks = perpustakaan.CariBukuByJudul(searchJudul);
                    if (foundBooks.Count > 0)
                    {
                        Console.WriteLine($"Hasil pencarian untuk '{searchJudul}':");
                        foreach (var buku in foundBooks)
                        {
                            buku.DisplayInfo();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Buku tidak ditemukan.");
                    }
                    break;

                case "5": 
                    Console.Write("Masukkan ID buku yang akan diupdate: ");
                    int updateId = int.Parse(Console.ReadLine());
                    Console.Write("Judul baru: ");
                    string newJudul = Console.ReadLine();
                    Console.Write("Penulis baru: ");
                    string newPenulis = Console.ReadLine();
                    Console.Write("Tahun terbit baru: ");
                    int newTahun = int.Parse(Console.ReadLine());
                    perpustakaan.UpdateBuku(updateId, newJudul, newPenulis, newTahun);
                    break;

                case "6": 
                    Console.Write("Masukkan ID buku yang akan dihapus: ");
                    int deleteId = int.Parse(Console.ReadLine());
                    perpustakaan.HapusBuku(deleteId);
                    break;

                case "7": 
                    Console.Write("Masukkan ID buku yang akan dipinjam: ");
                    int pinjamId = int.Parse(Console.ReadLine());
                    perpustakaan.PinjamBuku(pinjamId);
                    break;

                case "8": 
                    Console.Write("Masukkan ID buku yang akan dikembalikan: ");
                    int kembaliId = int.Parse(Console.ReadLine());
                    perpustakaan.KembalikanBuku(kembaliId);
                    break;

                case "9": 
                    isRunning = false;
                    Console.WriteLine("Terima kasih telah menggunakan sistem perpustakaan.");
                    break;

                default:
                    Console.WriteLine("Pilihan tidak valid. Silakan coba lagi.");
                    break;
            }

            Console.WriteLine("\nTekan tombol apa saja untuk melanjutkan...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}