using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Above_Premiere.Modelo
{
    public class User
    {
        string name;
        string password;
        string key;

        public User(string nombre, string password)
        {
            this.name = nombre;
            this.password = password;

        }

        public string Name { get => name; set => name = value; }
        public string Password { get => password; set => password = value; }
        public string Key { get => key; set => key = value; }

        public void setKey()
        {
            if (this.key == null)
            {
                this.key = new CreatorKey().getKey();
            }
        }


        private class CreatorKey
        {

            List<string> keys;
            const int NUMBER_OF_CHARACTERS_PER_SECTION = 4;
            const int QUANTITY_OF_SECTIONS = 5;
            public CreatorKey()
            {
                keys = new List<string>();
                generarClave();
            }

            private int sizeKeys()
            {
                return keys.Count();
            }

            private void generarClave()
            {
                Random rnd = new Random();
                string caracteres = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                string numeros = "1234567890";
                char letra;
                int longitudDeCadaSeccionContrasenia = NUMBER_OF_CHARACTERS_PER_SECTION;

                //FORMATO : AAAA-BBBB-1111-2222-3333 
                while (sizeKeys() < QUANTITY_OF_SECTIONS)
                {
                    string contraseniaAleatoria = string.Empty;

                    if (sizeKeys() >= 0 && sizeKeys() < 2)
                    {
                        int longitudLetras = caracteres.Length;
                        for (int i = 0; i < longitudDeCadaSeccionContrasenia; i++)
                        {
                            letra = caracteres[rnd.Next(longitudLetras)];
                            contraseniaAleatoria += letra.ToString();
                        }
                    }
                    else
                    {
                        int longitudNumeros = numeros.Length;
                        for (int i = 0; i < longitudDeCadaSeccionContrasenia; i++)
                        {
                            letra = numeros[rnd.Next(longitudNumeros)];
                            contraseniaAleatoria += letra.ToString();
                        }
                    }

                    if (QUANTITY_OF_SECTIONS - 1 != sizeKeys())
                    {
                        contraseniaAleatoria += "-";
                    }
                    keys.Add($"{contraseniaAleatoria}");
                }
            }

            public string getKey()
            {
                string claveTotal = string.Empty;
                for (int i = 0; i < sizeKeys(); i++)
                {
                    claveTotal += $"{keys[i]}";
                }
                return claveTotal;
            }
        }
    }
}
