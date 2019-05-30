using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace EnvioDeTextoSocketServidor
{
    class Program
    {
        static void Main(string[] args)
        {
            abrirServer();
        }

        private static void abrirServer()
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint miDireccion = new IPEndPoint(IPAddress.Any, 1234);
            string textoRecibido = "", texto = "";
            byte[] textoAEnviar, ByRec;

            try
            {
                //Se conecta
                socket.Bind(miDireccion);
                socket.Listen(1);
                Console.WriteLine("Escuchando...");
                Socket Escuchar = socket.Accept();
                Console.WriteLine("Conectado con exito");

                menu();
                string caca = Console.ReadLine();
                switch (caca)
                {
                    case "1":
                        while (texto != "exit")
                        {
                            //Envia
                            texto = Console.ReadLine();
                            textoAEnviar = Encoding.Default.GetBytes(texto); //Paso a arrayDeBits el texto
                            Escuchar.Send(textoAEnviar, 0, textoAEnviar.Length, 0);
                        }
                        break;
                    case "2":
                        while(true){
                            //Recibe
                            ByRec = new byte[255];
                            int a = Escuchar.Receive(ByRec, 0, ByRec.Length, 0);
                            Array.Resize(ref ByRec, a);
                            textoRecibido = Encoding.Default.GetString(ByRec);
                            Console.WriteLine("Cliente: " + textoRecibido); //Mostramos lo recibido
                            if (textoRecibido == "exit")
                            {
                                break;
                            }
                        }
                        break;
                }
                
                socket.Close();
                Console.WriteLine("termino");
            }
            catch (Exception error)
            {
                Console.WriteLine("Error: {0}", error.ToString());
            }
            Console.WriteLine("Presione cualquier tecla para continuar");
            Console.ReadLine();
        }

        static void menu()
        {
            Console.Clear();
            Console.WriteLine("1. Enviar Datos 2.Recibir Datos");
        }
    }
}
//do
/*while (texto != "exit")
{

    //Recibe
    ByRec = new byte[255];
    int a = Escuchar.Receive(ByRec, 0, ByRec.Length, 0);
    Array.Resize(ref ByRec, a);
    textoRecibido = Encoding.Default.GetString(ByRec);
    Console.WriteLine("Cliente: " + textoRecibido); //Mostramos lo recibido
    if (textoRecibido == "exit")
    {
        break;
    }

    //Envia
    texto = Console.ReadLine();
    textoAEnviar = Encoding.Default.GetBytes(texto); //Paso a arrayDeBits el texto
    Escuchar.Send(textoAEnviar, 0, textoAEnviar.Length, 0);
}*/
