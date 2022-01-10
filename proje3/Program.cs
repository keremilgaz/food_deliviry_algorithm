using System;
using System.Collections;
using System.Collections.Generic;

namespace proje3
{
    internal class Program
    {
        class yemekSinifi
        {
            int teslimat_adet;
            int yemek_fiyat;
            string yemek_adi;

            Random rnd = new Random();

            public yemekSinifi()
            {
                int yemek_secim = rnd.Next(0, yemekler.Length);
                yemek_adi = yemekler[yemek_secim]; //Rastgele olarak yemekler listesinden yemek adı çekildi.
                teslimat_adet = rnd.Next(1, 8); // teslimat adedi 1 ile 15 arası rastgele olarak oluruldu.
                yemek_fiyat = fiyatlar[yemek_secim];
            }
            public void Set_yemek_fiyat(int value)
            {
                yemek_fiyat = value;
            }

            string[] yemekler = { "Pide", "Pizza", "Lahmacun", "Hamburger", "Kebab",
                                "Simit", " Kızartma", "Börek","Ayran", "Kola","Şalgam" };
            int[] fiyatlar = { 80, 70, 90, 10, 11, 12, 13, 140, 80, 6, 10 };
            public string toString()
            {
                return " Teslimat Adedi:" + teslimat_adet.ToString() + " Yemeğin Birim Fiyatı:" + yemek_fiyat.ToString()+ " Yemek Adı: " + yemek_adi;  // teslimat edilecek yemeğin adını ve adedini yazdıran metod.
            }

            public int fiyat()
            {
                return teslimat_adet * yemek_fiyat;
            }

            public string yiyecek_adi()
            {
                return yemek_adi;
            }

            public int urun_adet()
            {
                return teslimat_adet;
            }

            public int yemek_fiyat_dondur()
            {
                return yemek_fiyat;
            }

        }
        class TreeNode
        {
            public List<yemekSinifi> liste;
            public TreeNode leftChild;
            public TreeNode rightChild;
            public String mahalle_isim;

            public void DisplayNode()
            {
                Console.WriteLine(" Mahalle Adı: " + mahalle_isim );
                foreach(var c in liste)
                {
                    Console.WriteLine(c.toString());
                }
            }

            public string mahalle_isim_dondur()
            {
                return mahalle_isim;
            }

        }

        class Tree
        {
            public TreeNode root;

            public Tree()
            {
                root = null;
            }

            public void insert(String isim, List<yemekSinifi> liste)
            {
                TreeNode newNode = new TreeNode();
                newNode.liste = liste;
                newNode.mahalle_isim = isim;
                if (root == null)
                {
                    root = newNode;
                }
                else
                {
                    TreeNode current = root;
                    TreeNode parent;
                    while (true)
                    {
                        parent = current;
                        if (String.Compare(isim, current.mahalle_isim) < 0) // BURAYA DÖN.
                        {
                            current = current.leftChild;
                            if (current == null)
                            {
                                parent.leftChild = newNode;
                                break;
                            }
                        }
                        else
                        {
                            current = current.rightChild;
                            if (current == null)
                            {
                                parent.rightChild = newNode;
                                break;
                            }
                        }
                    }
                }


            }

            public void preOrder(TreeNode localRoot) // Tree yi preorder bir şekilde dolaşıp yazdıran metod.
            {
                if (localRoot != null)
                {
                    localRoot.DisplayNode();
                    preOrder(localRoot.leftChild);
                    preOrder(localRoot.rightChild);
                }
            }
            public void yuzelliUstu(TreeNode localRoot, String arananMahalleisim) // treeyi baştan sona dolaştım.
            {
                if (localRoot != null)
                {
                    if(arananMahalleisim == localRoot.mahalle_isim)
                    {
                        foreach (var c in localRoot.liste)
                        {
                            if (c.fiyat() > 150)
                            {
                                Console.WriteLine(c.toString());
                            }
                        }
                    }               
                    yuzelliUstu(localRoot.leftChild, arananMahalleisim);
                    yuzelliUstu(localRoot.rightChild, arananMahalleisim);
                }
            }

            public static int toplam_yiyecek_adet = 0;
            public int toplam_yiyecek_adet_dondur()
            {
                return toplam_yiyecek_adet;
            }
            public void yiyecekAdet(TreeNode localRoot, String yiyecek_isim) // treeyi baştan sona dolaştım.
            {
                if (localRoot != null)
                {
                    foreach (var c in localRoot.liste)
                    {
                       if (c.yiyecek_adi() == yiyecek_isim)
                       {
                            c.Set_yemek_fiyat(c.yemek_fiyat_dondur() - (c.yemek_fiyat_dondur() / 10));
                            toplam_yiyecek_adet += c.urun_adet();
                       }
                    }
                    yiyecekAdet(localRoot.leftChild, yiyecek_isim);
                    yiyecekAdet(localRoot.rightChild, yiyecek_isim);
                }
            }
            

            public int GetTreeDepth() // Max derinlik bulmak için çağrılacak metod.
            {
                Console.Write("Max Derinlik:");
                return this.GetTreeDepth(this.root) -1;
            }

            private int GetTreeDepth(TreeNode parent)
            {
                return parent == null ? 0 : Math.Max(GetTreeDepth(parent.leftChild), GetTreeDepth(parent.rightChild)) + 1; // Recursive şekilde ağacın yapraklarına gidildi ve en derin nokta döndürüldü.
            }
        }

        static void Main(string[] args)
        {
            Tree tree = new Tree();
            Random rnd = new Random();
            

            String[] mahalleler = { "Evka 3", "Özkanlar", "Atatürk", "Erzene", "Kazımdirik"};
            for(int i=0; i < mahalleler.Length; i++)
            {
                int yemek_secim = rnd.Next(2, 7);
                List<yemekSinifi> siparisListe = new List<yemekSinifi>();
                for(int j=0; j < yemek_secim; j++)
                {
                    siparisListe.Add(new yemekSinifi());
                }
                tree.insert(mahalleler[i], siparisListe);
            }

            tree.preOrder(tree.root);
            Console.WriteLine(tree.GetTreeDepth());

            Console.Write("Mahalle Adını Giriniz(150 üstü siparişleri bulmak için.):");
            string siparis_150_ustu = Console.ReadLine();

            Console.WriteLine("150 lira üstü siparişi olan mahallenin sipariş bilgileri: ");
            tree.yuzelliUstu(tree.root, siparis_150_ustu);


            Console.Write("Yiyecek/İçecek adı giriniz(Tüm ağaçtaki o ürünün adeti yazılır ve ürünün fiyatına %10 indirim uygulanır.):");
            string yiyecek_isim = Console.ReadLine();
            tree.yiyecekAdet(tree.root, yiyecek_isim);
            Console.WriteLine(tree.toplam_yiyecek_adet_dondur());

            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("Adı verilen ürüne %10 indirim uygulandıktan sonra tree:");
            tree.preOrder(tree.root);

        }
    }
}
