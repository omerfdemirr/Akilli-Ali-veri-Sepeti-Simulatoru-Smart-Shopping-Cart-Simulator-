using System;
using System.Collections.Generic;
using System.Threading;

namespace AlisverisSepetiSimulasyonu
{
    class Program
    {
        static void Main(string[] args)
        {
            AlisverisSepeti sepet = new AlisverisSepeti();
            sepet.Baslat();
        }
    }

    public class AlisverisSepeti
    {
        private Dictionary<string, decimal> Urunler = new Dictionary<string, decimal>
        {
            { "Elma", 3.50m },
            { "Muz", 8.00m },
            { "Ekmek", 5.00m },
            { "Süt", 15.00m },
            { "Peynir", 50.00m },
            { "Çikolata", 12.00m }
        };

        private Dictionary<string, int> Sepet = new Dictionary<string, int>();

        public void Baslat()
        {
            Console.Clear();
            Console.WriteLine("***** Akıllı Alışveriş Sepeti Simülatörüne Hoş Geldiniz! *****");
            Thread.Sleep(1000);
            AnaMenu();
        }

        private void AnaMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("*** Ana Menü ***");
                Console.WriteLine("1. Ürün Listele");
                Console.WriteLine("2. Sepete Ürün Ekle");
                Console.WriteLine("3. Sepetten Ürün Çıkar");
                Console.WriteLine("4. Sepeti Görüntüle");
                Console.WriteLine("5. Alışverişi Tamamla");
                Console.WriteLine("6. Çıkış");
                Console.Write("Seçiminizi yapın (1-6): ");

                string secim = Console.ReadLine();
                switch (secim)
                {
                    case "1":
                        UrunListele();
                        break;
                    case "2":
                        SepeteEkle();
                        break;
                    case "3":
                        SepettenCikar();
                        break;
                    case "4":
                        SepetiGoruntule();
                        break;
                    case "5":
                        AlisverisiTamamla();
                        return;
                    case "6":
                        Console.WriteLine("Çıkış yapılıyor. Bizi tercih ettiğiniz için teşekkürler!");
                        Thread.Sleep(1000);
                        return;
                    default:
                        Console.WriteLine("Hatalı bir seçim yaptınız. Lütfen tekrar deneyin.");
                        Thread.Sleep(2000);
                        break;
                }
            }
        }

        private void UrunListele()
        {
            Console.Clear();
            Console.WriteLine("*** Mevcut Ürünler ve Fiyatları ***");
            foreach (var urun in Urunler)
            {
                Console.WriteLine($"- {urun.Key}: {urun.Value:C}");
            }

            Console.WriteLine("\nDevam etmek için bir tuşa basın...");
            Console.ReadKey();
        }

        private void SepeteEkle()
        {
            Console.Clear();
            Console.WriteLine("*** Sepete Ürün Ekleme ***");
            Console.Write("Eklenecek ürünün adını giriniz: ");
            string urunAdi = Console.ReadLine();

            if (Urunler.ContainsKey(urunAdi))
            {
                Console.Write("Kaç adet eklemek istiyorsunuz?: ");
                if (int.TryParse(Console.ReadLine(), out int adet) && adet > 0)
                {
                    if (Sepet.ContainsKey(urunAdi))
                        Sepet[urunAdi] += adet;
                    else
                        Sepet[urunAdi] = adet;

                    Console.WriteLine($"✓ {adet} adet {urunAdi} sepete eklendi.");
                }
                else
                {
                    Console.WriteLine("Hatalı giriş yaptınız. Lütfen geçerli bir sayı girin.");
                }
            }
            else
            {
                Console.WriteLine($"X {urunAdi} isimli bir ürün bulunamadı.");
            }

            Thread.Sleep(2000);
        }

        private void SepettenCikar()
        {
            Console.Clear();
            Console.WriteLine("*** Sepetten Ürün Çıkarma ***");
            Console.Write("Çıkarılacak ürünün adını giriniz: ");
            string urunAdi = Console.ReadLine();

            if (Sepet.ContainsKey(urunAdi))
            {
                Console.Write("Kaç adet çıkarmak istiyorsunuz?: ");
                if (int.TryParse(Console.ReadLine(), out int adet) && adet > 0)
                {
                    if (Sepet[urunAdi] > adet)
                    {
                        Sepet[urunAdi] -= adet;
                        Console.WriteLine($"✓ {adet} adet {urunAdi} sepetten çıkarıldı.");
                    }
                    else
                    {
                        Sepet.Remove(urunAdi);
                        Console.WriteLine($"✓ {urunAdi} tamamen sepetten çıkarıldı.");
                    }
                }
                else
                {
                    Console.WriteLine("Hatalı giriş yaptınız. Lütfen geçerli bir sayı girin.");
                }
            }
            else
            {
                Console.WriteLine($"X {urunAdi} isimli bir ürün sepetinizde bulunamadı.");
            }

            Thread.Sleep(2000);
        }

        private void SepetiGoruntule()
        {
            Console.Clear();
            Console.WriteLine("*** Sepetiniz ***");
            if (Sepet.Count == 0)
            {
                Console.WriteLine("Sepetiniz boş.");
            }
            else
            {
                decimal toplamFiyat = 0;
                foreach (var urun in Sepet)
                {
                    decimal urunFiyati = Urunler[urun.Key] * urun.Value;
                    Console.WriteLine($"- {urun.Key}: {urun.Value} adet, Toplam: {urunFiyati:C}");
                    toplamFiyat += urunFiyati;
                }

                Console.WriteLine($"\nToplam Fiyat: {toplamFiyat:C}");
            }

            Console.WriteLine("\nDevam etmek için bir tuşa basın...");
            Console.ReadKey();
        }

        private void AlisverisiTamamla()
        {
            Console.Clear();
            Console.WriteLine("*** Alışveriş Tamamlanıyor... ***");
            if (Sepet.Count == 0)
            {
                Console.WriteLine("Sepetiniz boş. Lütfen alışveriş yapmadan tamamlamayın!");
            }
            else
            {
                SepetiGoruntule();
                Console.WriteLine("\nBizi tercih ettiğiniz için teşekkürler! İyi günler dileriz!");
            }

            Thread.Sleep(3000);
        }
    }
}
