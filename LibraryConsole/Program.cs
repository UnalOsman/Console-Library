using System.Linq;
using System.Security.Cryptography.X509Certificates;
internal class Program
{

    static List<Kitap> kitaps = new List<Kitap>();
    static List<OduncKitap> oduncKitaps = new List<OduncKitap>();
    static List<OduncKitap> gecmisKitapListesi = new List<OduncKitap>();

    /* Merhaba sayın Tolga hocam.Böyle bir programın içinde bulunduğum için size teşekkürlerimi iletiyorum.
     * Kendimdeki eksikleri bir nebze olsa da görmüş bulunmaktayım.Constructor yapısını tam olarak 
     * bilmediğimi farkettim.Birkaç gün ona çalıştım.Bazı kod satırları için ChatGPT 'den yardım aldığımı
     * söylemek isterim.Lambda ve DateTime satırlarında.Olabildiğince kendim yapmaya çalıştım tabiki.
     * Tekrardan teşekkürlerimi ve saygılarımı sunarım,gerçekten hayatıma etki eden insanlardan birisiniz.
     */


    static void Main()
    {
        Soru();

        static void Soru()
        {
            Console.WriteLine("Yapılacak işlemi seçiniz.");
            Console.WriteLine("1-Kitap ekle");
            Console.WriteLine("2-Kitapları görüntüle");
            Console.WriteLine("3-Kitap ara");
            Console.WriteLine("4-Kitap ödünç al");
            Console.WriteLine("5-Kitap iade et");
            Console.WriteLine("6-Süresi geçmiş kitapları görüntüle");
            Console.WriteLine("0-çıkış");
            islem();
        }

        static void islem()
        {
            switch (Console.ReadLine())
            {
                case "1":
                    Console.Clear();
                    kitapEkle();
                    Console.ReadLine();
                    break;
                case "2":
                    Console.Clear();
                    kitapGoruntule(); Console.ReadLine();
                    break;
                case "3":
                    Console.Clear();
                    kitapAra();
                    break;
                case "4":
                    Console.Clear();
                    oduncAl();
                    break;
                case "5":
                    Console.Clear();
                    iadeEt();
                    break;
                case "6":
                    Console.Clear();GecmisKitapGoruntule();
                    break;
                case "0":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Hatalı giriş");
                    break;
            }
        }


        static void kitapEkle()
        {
            Console.Write("Kitap ID numarasını yazınız : ");
            string kitapId = Console.ReadLine();
            Console.Write("Kitap adını yazınız : ");
            string kitapAdi = Console.ReadLine();
            Console.Write("Kitap yazarını yazınız : ");
            string yazar = Console.ReadLine();
            Console.Write("Kitap kopya sayısını giriniz : ");
            int kopyaSayisi=int.Parse(Console.ReadLine());

           
            
            if(kitaps.Any(kitap => kitap.KitapId==kitapId))
            {
                Console.WriteLine("Aynı ID numarasına sahip başka bir kitap eklenemez!!");
            }
            else
            {
                Kitap yeniKitap = new Kitap(kitapAdi, kitapId, yazar, kopyaSayisi, 0);
                kitaps.Add(yeniKitap);
                Console.WriteLine("Kitap başarıyla eklendi!!");
            }

            
            Console.WriteLine("b-Ana menüye dön");
            if(Console.ReadLine()=="b")
            {
                Console.Clear() ;
                Soru();
            }
        }

        static void kitapGoruntule()
        {
            if(kitaps.Count == 0)
            {
                Console.WriteLine("Kütüphanede ekli kitap bulunamadı!!");
            }
            else
            {
                foreach (Kitap k in kitaps)
                {
                    Console.WriteLine(k.ToString());
                }
            }

            Console.WriteLine("b-Ana menüye dön");
            if (Console.ReadLine() == "b")
            {
                Console.Clear(); 
                Soru();
            }
        }

        static void kitapAra()
        {

            Console.WriteLine("1-Kitabı ID numarasına göre ara");
            Console.WriteLine("2-Kitabı ismine göre ara");
            Console.WriteLine("b-Ana menüye dön");
            string numara=Console.ReadLine();

            if(numara=="1")
            {
                Console.Clear();
                Console.Write("Kitap ID numarasını giriniz : ");
                string kitapId=Console.ReadLine().ToLower();
               Kitap arananKitap=kitaps.Find(kitap => kitap.KitapId.ToLower()== kitapId);
                if (arananKitap != null)
                {
                    Console.WriteLine(arananKitap);
                }
                else
                {
                    Console.WriteLine("Kitap bulunamadı!!");
                }
            }
            else if(numara=="2")
            {
                Console.Clear (); 
                Console.Write("Kitap ismi giriniz : ");
                string kitapAdi = Console.ReadLine().ToLower();
                Kitap arananKitap=kitaps.Find(kitap => kitap.KitapAdi.ToLower()== kitapAdi);
                if (arananKitap != null)
                {
                    Console.WriteLine(arananKitap);
                }
                else
                {
                    Console.WriteLine("Kitap bulunamadı!!");
                }
            }
            else if (numara == "b")
            {
                Console.Clear();
                Soru();
            }

            

            Console.WriteLine("b-Ana menüye dön");
            if (Console.ReadLine() == "b")
            {
                Console.Clear();
                Soru();
            }

        }

        static void oduncAl()
        {
            Console.Write("Ödünç alınacak kitabın ID numarasını giriniz : ");
            string kitapId=Console.ReadLine();

            Kitap oduncAlinanKitap=kitaps.Find(kitap => kitap.KitapId == kitapId);

            if(oduncAlinanKitap != null && oduncAlinanKitap.KopyaSayisi > 0 )
            {
                oduncAlinanKitap.KopyaSayisi--;
                oduncAlinanKitap.OduncAlinanKitap++;

                DateTime oduncTarihi=DateTime.Now;
                DateTime iadeTarihi=oduncTarihi.AddMinutes(1);

                OduncKitap oduncKitap = new OduncKitap(oduncAlinanKitap,oduncTarihi,iadeTarihi);
                oduncKitaps.Add(oduncKitap);

               

                Console.WriteLine($"Kitap ödünç alındı. Kalan kopya sayısı : {oduncAlinanKitap.KopyaSayisi} " +
                    $"- İade tarihi : {iadeTarihi}");
            }
            else
            {
                Console.WriteLine("Kitap bulunamadı veya Kitap kopya sayısı yetersiz!!");
            }

            Console.WriteLine("b-Ana menüye dön");
            if (Console.ReadLine() == "b")
            {
                Console.Clear();
                Soru();
            }
        }

        static void iadeEt()
        {
            Console.Write("İade edilecek kitabın ID numarasını giriniz : ");
            string kitapId=Console.ReadLine();

            OduncKitap oduncKitap = oduncKitaps.Find(oduncKitap => oduncKitap.Kitap.KitapId==kitapId);

            if( oduncKitap != null )
            {
                oduncKitaps.Remove(oduncKitap);


                if(DateTime.Now <= oduncKitap.IadeTarihi)
                {
                    oduncKitap.Kitap.KopyaSayisi++;
                    oduncKitap.Kitap.OduncAlinanKitap--;

                    Console.WriteLine($"Kitap iade edildi. Yeni kopya sayısı : {oduncKitap.Kitap.KopyaSayisi}");
                }
                else
                {
                    Console.WriteLine("Kitap iade süresini geçtiği için iade edilmedi.Süresi geçmiş kitaplara eklendi.");
                    GecmisKitapEkle(oduncKitap);
                }

                
            }
            else
            {
                Console.WriteLine("İade edilecek kitap bulunamadı!!");
            }


            Console.WriteLine("b-Ana menüye dön");
            if (Console.ReadLine() == "b")
            {
                Console.Clear();
                Soru();
            }
        }

        static void GecmisKitapEkle(OduncKitap oduncKitap)
        {
           if(DateTime.Now > oduncKitap.IadeTarihi )
            {
                Console.WriteLine($"Süresi geçmiş kitap : {oduncKitap.Kitap}");
                gecmisKitapListesi.Add( oduncKitap );
            }
        }

        static void GecmisKitapGoruntule()
        {

            if (gecmisKitapListesi.Count == 0)
            {
                Console.WriteLine("Süresi geçmiş kitap bulunamadı!!");
            }
            else
            {

                Console.WriteLine("Süresi geçmiş kitaplar : ");

                foreach(OduncKitap i in gecmisKitapListesi)
                {
                    Console.WriteLine(i.Kitap);
                }
            }

            Console.WriteLine("b-Ana menüye dön");
            if (Console.ReadLine() == "b")
            {
                Console.Clear();
                Soru();
            }
        }
    }
}

class Kitap
{
    public string KitapAdi;
    public string KitapId;
    public string Yazar;
    private int kopyaSayisi;
    public int KopyaSayisi
    {
        get
        {
            return (int)kopyaSayisi;
        }
        set
        {
            if (value < 0)
                this.kopyaSayisi = 0;
            else
                this.kopyaSayisi = value;
        }
    }
    public int OduncAlinanKitap;

    
    public Kitap(string kitapAdi,string kitapId,string yazar,int kopyaSayisi=1,int oduncAlinanKitap=0)
    {
        KitapAdi = kitapAdi;
        KitapId = kitapId;
        Yazar = yazar;
        KopyaSayisi = kopyaSayisi;
        OduncAlinanKitap = oduncAlinanKitap;
        
    }

    override public string ToString()
    {
        return $"{KitapId} - {KitapAdi} - {Yazar} - {KopyaSayisi} (Kopya Sayısı)" +
            $" - {OduncAlinanKitap} (Ödünç Alınan kitap sayısı))";
    }
}

class OduncKitap
{
    public Kitap Kitap;
    public DateTime IadeTarihi;
    public DateTime OduncAlmaTarihi;


    public OduncKitap(Kitap kitap,DateTime oduncAlmaTarihi,DateTime iadeTarihi)
    {
        Kitap = kitap;
        OduncAlmaTarihi = oduncAlmaTarihi;
        IadeTarihi = iadeTarihi;
    }

}
