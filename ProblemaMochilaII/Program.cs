using System;
using System.Collections.Generic;

namespace ProblemaMochilaII
{
    public class Element
    {
        public string Article { get; set; }
        public int Volume { get; set; }
        public int Benefit { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var items = new List<Element>
            {
                new Element{Article="A",Volume=1,Benefit=2},
                new Element{Article="B",Volume=2,Benefit=5},
                new Element{Article="C",Volume=4,Benefit=6 },
                new Element{Article="D",Volume=5,Benefit=10},
                new Element{Article="E",Volume=7,Benefit=13},
                new Element{Article="F",Volume=8,Benefit=16 }
            };

            Console.WriteLine(SolvingProblem(8, items));
        }

        public static int SolvingProblem(int bagCapacity, List<Element> elements)
        {
            var itemCount = elements.Count;

            int[,] matrix = new int[itemCount + 1, bagCapacity + 1];

            //Bucle para los elementos
            for (int i = 0; i <= itemCount; i++)
            {
                //bucle para la capacidad de la mochila  
                for (int w = 0; w <= bagCapacity; w++)
                {
                    //elemento 0 de la matriz 
                    if (i == 0 || w == 0)
                    {
                        matrix[i, w] = 0;
                        continue;
                    }

                    //trabajamos con elemento menos uno para trabajar mejor 
                    var currentElementIndex = i - 1;
                    var currentElement = elements[currentElementIndex];
                    //¿El peso de la elemento actual es menor que W 
                    //(por ejemplo, ¿podríamos encontrar un lugar para ponerla en la bolsa si tuviéramos que hacerlo, aunque vaciáramos otra cosa?)
                    if (currentElement.Volume <= w)
                    {
                        //Si tomara esta elemento ahora mismo, y la combinara con otras gemas
                        //En otras palabras, si W es 50, y yo peso 30. Si me uniera con otra elemento que fuera de 20 (O múltiples que pesen 20, o ninguna)
                        //Si no es así, entonces sólo pon el valor que haya pasado con el último elemento 
                        //(puede que haya encajado, puede que haya hecho lo mismo y no haya encajado y haya obtenido lo anterior, etc). 
                        matrix[i, w] = Math.Max(currentElement.Benefit + matrix[i - 1, w - currentElement.Volume]
                                                , matrix[i - 1, w]);
                    }
                    //Esta elemento no puede encajar, así que adelanta lo que era el último valor porque sigue siendo el "mejor" ajuste que tenemos. 
                    else
                        matrix[i, w] = matrix[i - 1, w];
                }
            }

            //Porque llevamos todo adelante, el último elemento de ambos índices es nuestro valor máximo
            return matrix[itemCount, bagCapacity];
        }
    }
}
