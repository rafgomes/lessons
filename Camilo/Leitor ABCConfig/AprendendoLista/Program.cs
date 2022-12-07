// See https://aka.ms/new-console-template for more information




namespace MyApp // Note: actual namespace depends on the project name.
{
   
    class DummyClass
    {
        public string Name { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public override string ToString() { return $"{Name} - {X} {Y}"; }
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            AprendedoSobreListas.Teste();

            AprendedoSobreListas obj = new AprendedoSobreListas();

            obj.AlocaListasNaMemoria();


        }
    }


    public class AprendedoSobreListas
    {
        public static void Teste()
        {
            Console.WriteLine("TEste");
        }

        public void AlocaListasNaMemoria()
        {
            Console.WriteLine("Hello, World!");

             List<string> nomeLista = new List<string>();

            //nomeLista.Add(2000);  //Error: nao eh possivel converter int para string

            List<int> listaDeInts = new List<int>();


            //listaDeInts.Add( 20.3 ) //Error: nao en possivel converter double para int


            List<double> listaDeDoubles = new List<double>();

            listaDeDoubles.Add(1.5);
            listaDeDoubles.Add(20);
            listaDeDoubles.Add(30);


            DummyClass obj1 = new DummyClass()
            {
                Name = "Camilo",
                X = 1,
                Y = 2
            };

            DummyClass obj2 = new DummyClass()
            {
                Name = "Girino",
                X = 1,
                Y = 2
            };

            DummyClass obj3 = new DummyClass()
            {
                Name = "Girino3",
                X = 1,
                Y = 2
            };

            List<DummyClass> dummyClasses = new List<DummyClass>();
            dummyClasses.Add(obj1);
            dummyClasses.Add(obj2);
            dummyClasses.Add(obj3);


            DummyClass pegueiOPriemiro = dummyClasses.First();

            List<Object> listaDeObjectos = new List<Object>();

            listaDeObjectos.Add(obj1);
            listaDeObjectos.Add("teste");
            listaDeObjectos.Add(1324);
            listaDeObjectos.Add(new DummyClass()
            {
                Name = "Um Valor",
                Y = 1,
                X = 2

            });

            string retorno = obj1.ToString();

            foreach(object item in listaDeObjectos)
            {

                Console.WriteLine(item);
                
            }
            Console.ReadLine();




        }



    }





}




