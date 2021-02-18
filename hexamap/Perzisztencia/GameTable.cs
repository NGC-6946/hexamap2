using hexamap.Modell;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace hexamap.Perzisztencia
{
   // public enum Field { Gabona, Erc, Fa, Tegla, Legelo, Sivatag }
   // public enum Product { Gabona, Erc, Fa, Tegla, Gyapju }

    public class GameTable
    {
        private List<int> _fieldNumbers; //dobókockával dobható számok
        private List<Field> _fieldResources; //mezők értékei

        //inicializáláshoz
        public GameTable()
        {

            init();

        }

        private void init()
        {
            _fieldNumbers = new List<int>();
            _fieldResources = new List<Field>();

            List<int> numbers = new List<int> { 0, 3, 3, 4, 4, 5, 5, 6, 6, 8, 8, 9, 9, 10, 10, 11, 11, 2, 12 };
            List<Field> resources = new List<Field>
            {
                Field.Gabona, Field.Gabona, Field.Gabona, Field.Gabona,
                Field.Erc, Field.Erc, Field.Erc,
                Field.Fa, Field.Fa, Field.Fa, Field.Fa,
                Field.Tegla, Field.Tegla, Field.Tegla,
                Field.Legelo, Field.Legelo,Field.Legelo,Field.Legelo,
                Field.Sivatag
            };


            Random rand = new Random();
            int index;
            while (numbers.Count > 0)
            {
                index = rand.Next(0, numbers.Count - 1);
                _fieldNumbers.Add(numbers[index]);
                numbers.RemoveAt(index);

            }
            int i = 0;
            while (resources.Count > 0)
            {
                index = rand.Next(0, resources.Count - 1);
                var randResource = resources[index];

                if (_fieldNumbers[i] == 0)
                {
                    while (randResource == Field.Sivatag)
                    {
                        index = rand.Next(0, resources.Count - 1);
                        randResource = resources[index];
                    }
                }

                _fieldResources.Add(randResource);
                resources.RemoveAt(index);
                i++;

            }

           ;
        }

        public int GetFieldNumber(int i)
        {
            return _fieldNumbers[i];
        }

        public Field GetFieldResource(int i)
        {
            return _fieldResources[i];
        }

        public void SetFieldResource(int i, Field res)
        {
            _fieldResources[i] = res;
        }


        //egy listában vissza adja azoknak a mezőknek az értékét, melyeken az adott szám szerepel
        public List<int> GetResourcesWithNumber(int number)
        {
            List<int> _indexes = new List<int>();
            for(int i = 0;  i< _fieldNumbers.Count; i++)
            {
                if (_fieldNumbers[i] == number)
                {
                    _indexes.Add(i);
                }
            }

            return _indexes;
        }


    }
}
