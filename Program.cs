using System;
using System.IO;


namespace Parcial1
{
    class Program
    {
        static void Main(string[] args) 
        {
            // Declaramos las variables
            long contador = 0;
            int op;
            string[] datoscontactos = new string[10000];
            string nombre, telefono, linea, sn, buscar;
            int i = 0, e = 0;

            try
            {   
                StreamWriter archivo; // Declaramos la Clase para escribir en el archivo
                StreamReader inicio; // Declaramos la Clase para leer en el archivo 
                if (!File.Exists("datos.txt")) // Si no existe realiza esto...
                {
                    archivo = File.CreateText("datos.txt");// Creamos el archivo llamado "datos"
                    archivo.Close();// Cerramos el archivo..
                }
                else
                {
                    inicio = File.OpenText("datos.txt"); // Leemos el archivo..
                    do
                    {
                        linea = inicio.ReadLine();
                        datoscontactos[i] = linea;
                        i += 1;
                        contador += 1;
                    }
                    while (linea != null);
                    inicio.Close(); // Cerramos el lector del archivo..
                    contador -= 1;
                    Console.WriteLine("{0}", contador);
                }


                do   // Creamos un do-While para generar las opciones de nuestra agenda telefonica
                {
                    Console.WriteLine(" *********************");
                    Console.WriteLine(" *AGENDA TELEFÓNICA*");
                    Console.WriteLine(" *********************\n");
                    Console.WriteLine(" 1- Agregar contacto");
                    Console.WriteLine(" 2- Mostrar contactos");
                    Console.WriteLine(" 3- Buscar contacto");
                    Console.WriteLine(" 4- Borrar contacto");
                    Console.WriteLine(" 5- Borrar lista completa");
                    Console.WriteLine(" 6- Modificar los contactos");
                    Console.WriteLine(" 7- Salir");
                    Console.Write(" - Digite una opcion: ");
                    op = int.Parse(Console.ReadLine());

                    switch (op)
                    {
                        case 1:
                            Console.Clear(); //Limpiamos pantalla
                            StreamWriter datos;// Declaramos la Clase para escribir en el archivo
                            Console.WriteLine("\n AGREGAR CONTACTOS");
                            Console.Write("\n-Nombre del contacto: "); //pedimos los datos del nombre
                            nombre = Convert.ToString(Console.ReadLine());
                            Console.Write("-Telefono: "); //pedimos el correspondiente telefono
                            telefono = Convert.ToString(Console.ReadLine());

                            datoscontactos[contador] = nombre +";"+telefono; //asi se guardarán los datos en el archivo
                            datos = File.AppendText("datos.txt");// Abrimos para editar el archivo..
                            datos.WriteLine("{0} ", datoscontactos[contador]); // el {0} significa insertar el primer parámetro después de la cadena de formato
                            datos.Close(); //cerramos el archivo
                            contador += 1;  //incrementará                          
                            break;

                        case 2:
                            Console.Clear();
                            Console.WriteLine("\nLISTA DE CONTACTOS\n");
                            StreamReader rdatos;// Declaramos la clase para leer archivos   
                            rdatos = File.OpenText("datos.txt");// Abrimos el archivo para leer linea por linea.

                            do //realizamos la busqueda de todos los contactos para mostrar
                            {
                                linea = rdatos.ReadLine();
                                if (linea != null)
                                    Console.WriteLine(linea);
                            }
                            while (linea != null);
                            rdatos.Close();
                            break;

                        case 3:
                            Console.Clear();
                            Console.WriteLine("\nBUSQUEDA DE CONTACTOS");
                            Console.Write("\n-Nombre o Telefono a buscar: ");
                            buscar = Convert.ToString(Console.ReadLine());

                            for (i = 0; i < contador; i++) //se crea un for para buscar un contacto en especifico
                            {
                                if (datoscontactos[i].IndexOf(buscar) >= 0)
                                    Console.WriteLine("{0}", datoscontactos[i]);
                            }
                            break;

                        case 4:
                            Console.Clear();
                            StreamWriter datoss;// Declaramos la Clase para escribir en el archivo
                            Console.WriteLine("\nELIMINAR UN CONTACTO");
                            Console.Write("\n-Nombre o Telefono a eliminar: ");
                            buscar = Convert.ToString(Console.ReadLine());

                            for (i = 0; i < contador; i++) //buscamos el dato a borrar
                            {
                                if (datoscontactos[i].IndexOf(buscar) >= 0)
                                {
                                    int s;
                                    for (s = i; s < contador - 1; s++)
                                        datoscontactos[s] = datoscontactos[s + 1];
                                    contador--;
                                    datoscontactos[contador] = " ";
                                }
                            }
                            sn = Convert.ToString(Console.ReadLine());
                            archivo = File.CreateText("datos.txt");// Creamos el archivo..
                            archivo.Close();// Cerramos el archivo..

                            for (e = 0; e < contador; e++)
                            {
                                datoss = File.AppendText("datos.txt");// Abrimos para editar el archivo..
                                datoss.WriteLine("{0} ", datoscontactos[e]);
                                datoss.Close();

                            }
                            Console.WriteLine("Contacto eliminado correctamente\n");
                            break;

                        case 5:
                            Console.Clear();
                            Console.WriteLine("\nELIMINACION DE LISTA DE CONTACTOS");
                            Console.Write("\n-Desea eliminar toda la lista de contactos??.... s/n: ");
                            sn = Convert.ToString(Console.ReadLine());
                            if (sn == "s")
                            {
                                archivo = File.CreateText("datos.txt");// Creamos el archivo..
                                archivo.Close();// Cerramos el archivo..
                                Array.Clear(datoscontactos, 0, datoscontactos.Length); //se borraran todos los datos de nuestro array
                                contador = 0;
                            }
                            break;
                        case 6:
                            Console.Clear();
                            Console.WriteLine("\nMODIFICACION DE CONTACTOS\n");
                            string nuevotelefono, resp, cadena;
                            StreamReader Lectura;
                            StreamWriter Temporal;
                            try
                            {
                                Lectura = File.OpenText("datos.txt"); //abrimos el archivo original
                                Temporal = File.CreateText("tmp.txt"); //creamos uno temporal al que vamos a guardar las modificaciones
                                Console.Write(" -Introduzca el nombre que desea modificarle el telefono: ");
                                nombre = Console.ReadLine();
                                int k = 0;
                                do // se crea un do-while para realizar la busqueda del nombre al que le vamos a modificar el telefono
                                {
                                    linea = Lectura.ReadLine();
                                    if (linea != null)
                                    {
                                        string[] campos = linea.Split(";"); //Split me permitirá leer los datos como los tenemos almacenados
                                        if (datoscontactos[k].IndexOf(nombre) >= 0)
                                        {
                                            Console.WriteLine("\n El contacto " + nombre + " encontrado con numero de telefono: "+ campos[1].Trim()); //El .Trim()  Devuelve una nueva cadena en la que se eliminan todas las apariciones iniciales y finales de un conjunto de caracteres especificados de la cadena actual.
                                            Console.Write("- \n Es el registro que buscabas?  s/n: ");
                                            resp = Console.ReadLine();
                                            if (resp.Equals("s"))
                                            {
                                                Console.Write("\n- Ingrese el nuevo numero de telefono: ");
                                                nuevotelefono = Console.ReadLine();
                                                Temporal.WriteLine(campos[0] + ";" + nuevotelefono); //la modificacion que se hizo me la almacena en el archivo temporal
                                                Console.WriteLine(" \nTelefono modificado...");
                                            }
                                        }
                                        else
                                        {
                                            Temporal.WriteLine(linea);
                                        }
                                        k = k + 1;
                                    }
                                } while (linea != null);

                                Lectura.Close();
                                Temporal.Close();
                                File.Delete("datos.txt");
                                File.Move("tmp.txt", "datos.txt");
                            }
                            catch (FileNotFoundException exc)
                            {
                                Console.WriteLine(" Se presento un error!!" + exc.Message);
                            }
                            catch (Exception ec)
                            {
                                Console.WriteLine(" Se presento un error!!" + ec.Message);
                            }
                            break;

                        case 7:
                            Console.WriteLine("\n EL programa ha finalizado...");
                            break;

                        default:
                            Console.WriteLine("\n Digite una opcion valida!!");
                            break;  
                    }

                }
                while (op!=7);
            }
            catch (Exception exc)
            {
                Console.WriteLine("Se produjo una excepcion! ", exc.Message);
            }
        }
        
    }
}

