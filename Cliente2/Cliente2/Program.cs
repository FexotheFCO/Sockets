using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace EnvioDeTextoSocket
{
    //Cliente
    class Program
    {
        static void Main(string[] args) 
        {
            Conectar();
        }

        private static void Conectar()
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint miDireccion = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1234);
            string texto = "", textoRecibido = "";
            byte[] textoAEnviar, ByRec;


            try
            {
                socket.Connect(miDireccion);
                Console.WriteLine("Conectado con exito");

                menu();
                string opcion = Console.ReadLine();
                switch (opcion)
                {
                    case "1":
                        while(texto != "exit")
                        {
                            //Enviar
                            texto = Console.ReadLine();
                            textoAEnviar = Encoding.Default.GetBytes(texto); //Paso a arrayDeBits el texto
                            socket.Send(textoAEnviar, 0, textoAEnviar.Length, 0);
                        }
                        break;
                    case "2":
                        while (true)
                        {
                            //Recibe
                            ByRec = new byte[255];
                            int a = socket.Receive(ByRec, 0, ByRec.Length, 0);
                            Array.Resize(ref ByRec, a);
                            textoRecibido = Encoding.Default.GetString(ByRec);
                            Console.WriteLine("Servidor: " + textoRecibido); //Mostramos lo recibido
                            if (textoRecibido == "exit")
                            {
                                break;
                            }
                        }
                        break;
                }
                
                socket.Close();
                Console.WriteLine("Termino");
            }
            catch (Exception error)
            {
                Console.WriteLine("Error: {0}", error.ToString());
                Console.WriteLine("Hubo un error");
            }
            Console.WriteLine("Presione cualquier tecla para terminar");
            Console.ReadLine();
        }

        static void menu()
        {
            Console.Clear();
            Console.WriteLine("1. Enviar Datos 2.Recibir Datos");
        }
    }
    /*while (textoRecibido != "exit")
                {
                    //Enviar
                    texto = Console.ReadLine();
                    textoAEnviar = Encoding.Default.GetBytes(texto); //Paso a arrayDeBits el texto
                    socket.Send(textoAEnviar, 0, textoAEnviar.Length, 0);
                    if (texto == "exit")
                    {
                        break;
                    }

                    //Recibe
                    ByRec = new byte[255];
                    int a = socket.Receive(ByRec, 0, ByRec.Length, 0);
                    Array.Resize(ref ByRec, a);
                    textoRecibido = Encoding.Default.GetString(ByRec);
                    Console.WriteLine("Servidor: " + textoRecibido); //Mostramos lo recibido

                }*/
}