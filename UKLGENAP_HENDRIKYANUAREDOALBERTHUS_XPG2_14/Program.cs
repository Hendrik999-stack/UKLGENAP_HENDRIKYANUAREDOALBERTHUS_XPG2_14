List<Stand> dataStand = new List<Stand>()
{
 new StandOutdoor("Outdoor-1", 400000),
 new StandOutdoor("Outdoor-2", 500000),
 new StandIndoor("Indoor-1", 700000),
 new StandIndoor("Indoor-2", 800000),
 new StandPremium("Premium-1", 1800000),
 new StandPremium("Premium-2", 2000000)
};


while (true)
{
    Console.Clear();

    Console.WriteLine("=== Moklet Expo Management Center ===");
    Console.WriteLine("\nDaftar Stand Tersedia");
    foreach (var Stand in dataStand)
    {
        Stand.TampilInfo();
    }
    Console.WriteLine("1. Sewa Stand\n2. Akhiri Sewa Stand\n3. Keluar");
    Console.Write("Pilihan: ");
    string pilihan = Console.ReadLine();
    if (pilihan == "1")
    {

        Console.Write("Nama Stand Yang Disewa: ");
        string stand_sewa = Console.ReadLine();

        var cari_Stand = dataStand.FirstOrDefault(ck => string.Equals(stand_sewa, ck.namaStand, StringComparison.OrdinalIgnoreCase));

        if (cari_Stand == null)
        {

            Console.WriteLine($"\n Stand dengan nama `{stand_sewa}` Tidak Ditemukan. ");

        }
        else if (cari_Stand.isAvailable)
        {
            Console.Write("\nInput jumlah hari: ");
            int hari = int.Parse(Console.ReadLine());

            double total_sewa = cari_Stand.HitungTotalSewa(hari);

            Console.WriteLine($"Total Pembayaran Sewa: Rp {total_sewa} ");

            cari_Stand.UbahStatus();
        }
        else
        {
            Console.WriteLine($"\n Stand dengan nama `{cari_Stand.namaStand}` Tidak Tersedia");
        }
    }
    else if (pilihan == "2")
    {
        Console.Write("Nama stand yang ingin diakhiri: ");
        string stand_sewa = Console.ReadLine();

        var cari_Stand = dataStand.FirstOrDefault(ck => string.Equals(stand_sewa, ck.namaStand, StringComparison.OrdinalIgnoreCase));

        if (cari_Stand == null)
        {

            Console.WriteLine($"\n Stand tidak ditemukan");

        }
        else if (!cari_Stand.isAvailable)
        {
            cari_Stand.UbahStatus();
            Console.WriteLine($"\n Sewa stand `{cari_Stand.namaStand}` berhasil diakhiri");
        }

    }
    else if (pilihan == "3")
    {
        Console.WriteLine("\nTerima kasih telah menggunakan Moklet Expo Management Center!");
        break;
    }
    else
    {
        Console.WriteLine("\nPilihan Invalid");
    }

    Console.WriteLine("\nTekan ENTER untuk mengulang");
    Console.ReadLine();

}


class Stand
{
    protected string _namaStand;
    protected double _hargaSewaPerHari;
    protected bool _isAvailable;



    public Stand(string namaStand, double hargaSewaPerHari)
    {
        _namaStand = namaStand;
        _hargaSewaPerHari = hargaSewaPerHari;
        _isAvailable = true;
    }
    public string namaStand
    {
        get { return _namaStand; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                Console.WriteLine("Nama stand tidak boleh kosong!");
            }
            else
            {
                _namaStand = value;
            }
        }
    }
    public double hargaSewaPerHari
    {
        get { return _hargaSewaPerHari; }
        set
        {
            if (value < 0)
            {
                Console.WriteLine("Harga sewa tidak boleh negatif!");
            }
            else
            {
                _hargaSewaPerHari = value;
            }
        }
    }

    public bool isAvailable
    {
        get { return _isAvailable; }
    }


    public void TampilInfo()
    {
        string status = _isAvailable ? "Tersedia" : "Tidak Tersedia";
        Console.WriteLine($" {_namaStand} \t | Rp {_hargaSewaPerHari} / hari \t {status}");
    }

    public void UbahStatus()
    {
        _isAvailable = false;
    }

    public virtual double HitungTotalSewa(int jumlahHari)
    {
        return _hargaSewaPerHari * jumlahHari;
    }
}

class StandOutdoor : Stand
{
    protected double _biayatenda;

    public StandOutdoor(string namaStand, double hargaSewaPerHari) : base(namaStand, hargaSewaPerHari)
    {
        _biayatenda = 75000;
    }

    public override double HitungTotalSewa(int jumlahHari)
    {
        return (base.HitungTotalSewa(jumlahHari)) + (_biayatenda * jumlahHari);
    }
}

class StandIndoor : Stand
{
    protected double _biayalistrik;

    public StandIndoor(string namaStand, double hargaSewaPerHari) : base(namaStand, hargaSewaPerHari)
    {
        _biayalistrik = 100000;
    }

    public override double HitungTotalSewa(int jumlahHari)
    {
        return (base.HitungTotalSewa(jumlahHari)) + (_biayalistrik * jumlahHari);
    }
}

class StandPremium : Stand
{
    protected double _biayaKeamanan;

    public StandPremium(string namaStand, double hargaSewaPerHari) : base(namaStand, hargaSewaPerHari)
    {
        _biayaKeamanan = 300000;
    }

    public override double HitungTotalSewa(int jumlahHari)
    {
        return (base.HitungTotalSewa(jumlahHari)) + (_biayaKeamanan * jumlahHari);
    }
}
