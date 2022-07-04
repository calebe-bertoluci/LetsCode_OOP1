using System.Formats.Asn1;
namespace DesafioPOO
{
    class Automovel
    {
        public string placa;
        public string modelo;
        public static byte GASOLINA = 1;
        public static byte ALCOOL = 2;
        public static byte DIESEL = 3;
        public static byte GAS = 4;
        public string cor;
        public int ano;
        public double valorCarro;
        public string combustivelSelecionado;
        public bool luxo = false;
        public bool direcaoHidraulica;
        public bool arCondicionado;
        public bool vidrosEletricos;
        public bool computadorBordo;

        public virtual void pedirCarro(int opcaoSelecionada)
        {

            Console.Write("Digite o Modelo: ");
            this.modelo = Console.ReadLine();

            Console.Write("Digite a Cor: ");
            this.cor = Console.ReadLine();

            Console.Write("Digite o Ano: ");
            this.ano = Convert.ToInt32(Console.ReadLine());

            Console.Write("Digite a Placa: ");
            this.placa = Console.ReadLine();

            if (opcaoSelecionada == GASOLINA)
            {
                this.valorCarro += 12000d;
                this.combustivelSelecionado = "Gasolina";
            }
            else if (opcaoSelecionada == ALCOOL)
            {
                this.valorCarro += 10500;
                this.combustivelSelecionado = "Alcool";
            }
            else if (opcaoSelecionada == DIESEL)
            {
                this.valorCarro += 11000;
                this.combustivelSelecionado = "Diesel";
            }
            else if (opcaoSelecionada == GAS)
            {
                this.valorCarro += 13000;
                this.combustivelSelecionado = "Gás";
            }
        }
    }

    class AutomovelLuxo : Automovel
    {


        public static void ImprimirDados(string modelo, string cor, int ano, string placa,
            string combustivelSelecionado, bool luxo, bool direcaoHidraulica, bool arCondicionado, bool vidrosEletricos,
            double valorCarro,int i)
        {
            Console.Clear();
            
            Console.WriteLine();
            if (i%2 == 0)
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
            else if (i%2 == 1)
                Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine($"MODELO: {modelo}");
            Console.WriteLine($"COR: {cor}");
            Console.WriteLine($"ANO: {ano}");
            Console.WriteLine($"PLACA: {placa}");
            Console.WriteLine($"COMBUSTÍVEL: {combustivelSelecionado}");
            Console.WriteLine($"MODELO LUXO: {luxo}");
            if (luxo)
            {
                Console.WriteLine($"DIREÇÃO HIDRÁULICA: {direcaoHidraulica}");
                Console.WriteLine($"DIREÇÃO HIDRÁULICA: {arCondicionado}");
                Console.WriteLine($"DIREÇÃO HIDRÁULICA: {vidrosEletricos}");
                Console.WriteLine("COMPUTADOR DE BORDO: true");
            }

            Console.WriteLine();
            Console.WriteLine("VALOR TOTAL DO VEÍCULO: R$" + valorCarro);
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("----------------------------------");
            Console.WriteLine();
        }

        public bool escolheOpcionais(ConsoleKeyInfo teclaPressionada)
        {
            if (teclaPressionada.Key == ConsoleKey.S)
                return true;
            else return false;
        }

        public override void pedirCarro(int opcaoSelecionada)
        {
            base.pedirCarro(opcaoSelecionada);

            this.luxo = true;

            Console.WriteLine("Quer Direção Hidráulica? [S/N]");
            this.direcaoHidraulica = escolheOpcionais(Console.ReadKey(true));

            Console.WriteLine("Quer Ar Condicionado? [S/N]");
            this.arCondicionado = escolheOpcionais(Console.ReadKey(true));

            Console.WriteLine("Quer Vidros Elétricos? [S/N]");
            this.vidrosEletricos = escolheOpcionais(Console.ReadKey(true));

            if (direcaoHidraulica)
                this.valorCarro += 2000;
            if (this.direcaoHidraulica)
                this.valorCarro += 1500;
            if (this.vidrosEletricos)
                this.valorCarro += 800;
        }


        class Program
        {
            static void AnimacaoCadastrar()
            {
                Console.Write("Objeto Carro");
                Thread.Sleep(500);
                Console.Write(".");
                Thread.Sleep(500);
                Console.Write(".");
                Thread.Sleep(500);
                Console.Write(".");
                Thread.Sleep(500);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("[CRIADO COM SUCESSO]");
                Console.ResetColor();
                Thread.Sleep(2500);
                Console.Clear();
            }

            public static void Main(string[] args)
            {

                Console.WriteLine("Quantos Automóveis Criar? ");
                int qtdAutomoveis = Convert.ToInt32(Console.ReadLine());
                Automovel[] a1 = new Automovel[qtdAutomoveis];
                for (int i = 0; i < qtdAutomoveis; i++)
                {
                    Console.WriteLine("Automóvel [N]ormal ou de [L]uxo?");
                    if (Console.ReadKey(true).KeyChar.ToString().ToUpper() == "N")
                    {
                        Console.WriteLine("[1]Gasolina - [2]Alcool - [3]Diesel - [4]Gas");
                        a1[i] = new Automovel();
                        a1[i].pedirCarro(Convert.ToInt32(Console.ReadKey(true).KeyChar.ToString()));
                    }
                    else
                    {
                        a1[i] = new AutomovelLuxo();
                        Console.WriteLine("[1]Gasolina - [2]Alcool - [3]Diesel - [4]Gas");
                        a1[i].pedirCarro(Convert.ToInt32(Console.ReadKey(true).KeyChar.ToString()));
                    }

                    AnimacaoCadastrar();
                }


                for (int i = 0; i < qtdAutomoveis; i++)
                {
                    ImprimirDados(a1[i].modelo, a1[i].cor, a1[i].ano, a1[i].placa, a1[i].combustivelSelecionado,
                        a1[i].luxo, a1[i].direcaoHidraulica, a1[i].arCondicionado, a1[i].vidrosEletricos,
                        a1[i].valorCarro,i);
                }
            }
        }
    }
}