using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Model;

namespace WpfApp1.Logic
{
    internal class MatrixPlacement
    {
        public static List<int> pronadjiMjesto(double x, double y, MjestoMatrica[,] matrica)
        {
            int X = (Int32)x;
            int Y = (Int32)y;
            List<int> povratna = new List<int>();

            if (X >= 250)
            {
                X--;
            }
            else if (Y >= 250)
            {
                Y--;
            }
            if (matrica[X, Y].Polje == Mjesto.Slobodno)
            {
                matrica[X, Y].Polje = Mjesto.Zauzeto;
                povratna.Add(X);
                povratna.Add(Y);
                return povratna;
            }
            else if (matrica[X, Y].Polje == Mjesto.Zauzeto)
            {
                // granicne vrijednosti -> ivice prozora 
                if (X + 1 >= matrica.GetLength(0))
                {
                    X = X - 1;  // da ga vratim u centralnu poziciju obruca 
                }
                else if (X - 1 < 0)
                {
                    X = 1;
                }
                else if (Y + 1 >= matrica.GetLength(0))
                {
                    Y = Y - 1;
                }
                else if (Y - 1 < 0)
                {
                    Y = 1;
                }

                for (int i = X - 1; i <= X + 1; i++)
                {
                    for (int j = Y - 1; j <= Y + 1; j++)
                    {
                        if (matrica[i, j].Polje == Mjesto.Slobodno)
                        {
                            povratna.Add(X);
                            povratna.Add(Y);
                            matrica[i, j].Polje = Mjesto.Zauzeto;
                            return povratna;
                        }
                    }
                }
            }

            if (X + 2 >= matrica.GetLength(0))
            {
                X = X - 2;  // da ga vratim u centralnu poziciju obruca 
            }
            else if (X - 2 < 0)
            {
                X = 2;
            }
            else if (Y + 2 >= matrica.GetLength(0))
            {
                Y = Y - 2;
            }
            else if (Y - 2 < 0)
            {
                Y = 2;
            }

            for (int i = X - 2; i <= X + 2; i++)
            {
                for (int j = Y - 2; j <= Y + 2; j++)
                {
                    if (matrica[i, j].Polje == Mjesto.Slobodno)
                    {
                        povratna.Add(X);
                        povratna.Add(Y);
                        matrica[i, j].Polje = Mjesto.Zauzeto;
                        return povratna;
                    }
                }
            }
            if (X + 3 >= matrica.GetLength(0))
            {
                X = X - 3;  // da ga vratim u centralnu poziciju obruca 
            }
            else if (X - 3 < 0)
            {
                X = 3;
            }
            else if (Y + 3 >= matrica.GetLength(0))
            {
                Y = Y - 3;
            }
            else if (Y - 3 < 0)
            {
                Y = 3;
            }

            for (int i = X - 3; i <= X + 3; i++)
            {
                for (int j = Y - 3; j <= Y + 3; j++)
                {
                    if (matrica[i, j].Polje == Mjesto.Slobodno)
                    {
                        povratna.Add(X);
                        povratna.Add(Y);
                        matrica[i, j].Polje = Mjesto.Zauzeto;
                        return povratna;
                    }
                }
            }
            if (X + 4 >= matrica.GetLength(0))
            {
                X = X - 3;  // da ga vratim u centralnu poziciju obruca 
            }
            else if (X - 4 < 0)
            {
                X = 4;
            }
            else if (Y + 4 >= matrica.GetLength(0))
            {
                Y = Y - 4;
            }
            else if (Y - 4 < 0)
            {
                Y = 4;
            }

            for (int i = X - 4; i <= X + 4; i++)
            {
                for (int j = Y - 4; j <= Y + 4; j++)
                {
                    if (matrica[i, j].Polje == Mjesto.Slobodno)
                    {
                        povratna.Add(X);
                        povratna.Add(Y);
                        matrica[i, j].Polje = Mjesto.Zauzeto;
                        return povratna;
                    }
                }
            }
            return povratna;
        }

    }
}
