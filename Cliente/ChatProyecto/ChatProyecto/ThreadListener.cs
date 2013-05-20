using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatProyecto
{
    class ThreadListener
    {
        StreamReader readStream;
        Form1 form;
        ClienteTCP con;

        public ThreadListener(ClienteTCP con, Form1 form)
        {
            this.con = con;
            this.form = form;
        }

        public void runThreadListener()
        {
            
                while (true)
                {

                    try
                    {
                        //Crea un stream de lectura
                        readStream = new StreamReader(con.ClientStream);

                        //Recoge el tamaño del objeto serializado
                        int size = int.Parse(readStream.ReadLine());

                        //Recoge el objeto serializado
                        char[] data = new char[size];
                        readStream.Read(data, 0, size);
                        
                        //Convierte el string recibido a un objeto
                        Message message = Message.SerializeToObject(new string(data));

                            //Si el objeto es de tipo mensaje invoca el metodo del formulario
                            if (message.Code == 'M')
                            {
                                form.recibirMensaje(message);
                            }

                            
                        
                    }
                    catch (Exception e)
                    {

                        Console.WriteLine("ERROR: Error while reading a stream.");
                        Console.WriteLine();
                    
                    }
                 
                }
            }
           
        }
    }

