using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Solved_Problems
{
    class Problems_Stacks_AnimalShelter
    {
        public class AnimalShelter
        {
            public enum AnimalType { dog, cat }
            public class Animal
            {
                public int Arrival { get; set; }
                public AnimalType Type { get; set; }
                public Animal(int arrival, AnimalType type)
                {
                    Arrival = arrival;
                    Type = type;
                }

                public override string ToString()
                {
                    return Type.ToString() + " - (" + Arrival + ")";
                }
            }

            List<Animal> animals = new List<Animal>();

            public int Count { get { return animals.Count; } }

            public void Enqueue(Animal a)
            {
                for (int x=0; x < animals.Count; x++)
                {
                    if (a.Arrival > animals[x].Arrival)
                    {                        
                        animals.Insert(x, a);
                        return;
                    }
                }

                animals.Add(a);
            }

            public Animal DeQueueAny()
            {
                Animal ret = animals.First();
                animals.RemoveAt(0);
                return ret;                
            }

            public Animal DeQueueDog()
            {
                for (int x=0; x < animals.Count; x++)
                {
                    if (animals[x].Type == AnimalType.dog)
                    {
                        Animal ret = animals[x];
                        animals.RemoveAt(x);
                        return ret;
                    }
                }

                return null;
            }

            public Animal DeQueueCat()
            {
                for (int x = 0; x < animals.Count; x++)
                {
                    if (animals[x].Type == AnimalType.cat)
                    {
                        Animal ret = animals[x];
                        animals.RemoveAt(x);
                        return ret;
                    }
                }

                return null;
            }

        }

        public class AnimalShelter2
        {
            public enum AnimalType { dog, cat }
            public class Animal : IComparable
            {
                public int Arrival { get; set; }
                public AnimalType Type { get; set; }
                public Animal(int arrival, AnimalType type)
                {
                    Arrival = arrival;
                    Type = type;
                }

                public override string ToString()
                {
                    return Type.ToString() + " - (" + Arrival + ")";
                }

                public int CompareTo(object obj)
                {
                    Animal a = (Animal)obj;
                    return this.Arrival.CompareTo(a.Arrival);
                }
            }

            Heap<Animal> dog = new Heap<Animal>(Heap<Animal>.heapTypeEnum.max);
            Heap<Animal> cat = new Heap<Animal>(Heap<Animal>.heapTypeEnum.max);

            public int Count { get { return dog.size + cat.size; } }

            public void Enqueue(Animal a)
            {
                if (a.Type == AnimalType.dog)
                    dog.Enqueue(a);
                else cat.Enqueue(a);
            }

            public Animal DeQueueAny()
            {
                if (dog.size > 0 && cat.size > 0)
                {
                    if (dog.Peek().Arrival > cat.Peek().Arrival)
                        return dog.Dequeue();
                    else return cat.Dequeue();
                }
                else
                {
                    if (dog.size > 0)
                        return dog.Dequeue();
                    return cat.Dequeue();
                }
            
            }

            public Animal DeQueueDog()
            {
                if (dog.size > 0)
                    return dog.Dequeue();
                return null;
            }

            public Animal DeQueueCat()
            {
                if (cat.size > 0)
                    return cat.Dequeue();

                return null;
            }

        }



        public void Run()
        {
            AnimalShelter shelter = new AnimalShelter();

            shelter.Enqueue(new AnimalShelter.Animal(5, AnimalShelter.AnimalType.dog));
            shelter.Enqueue(new AnimalShelter.Animal(3, AnimalShelter.AnimalType.dog));
            shelter.Enqueue(new AnimalShelter.Animal(10, AnimalShelter.AnimalType.dog));
            shelter.Enqueue(new AnimalShelter.Animal(2, AnimalShelter.AnimalType.dog));
            shelter.Enqueue(new AnimalShelter.Animal(11, AnimalShelter.AnimalType.dog));
            shelter.Enqueue(new AnimalShelter.Animal(6, AnimalShelter.AnimalType.dog));
            shelter.Enqueue(new AnimalShelter.Animal(9, AnimalShelter.AnimalType.cat));
            shelter.Enqueue(new AnimalShelter.Animal(21, AnimalShelter.AnimalType.cat));
            shelter.Enqueue(new AnimalShelter.Animal(15, AnimalShelter.AnimalType.cat));
            shelter.Enqueue(new AnimalShelter.Animal(19, AnimalShelter.AnimalType.cat));
            shelter.Enqueue(new AnimalShelter.Animal(1, AnimalShelter.AnimalType.cat));
            shelter.Enqueue(new AnimalShelter.Animal(3, AnimalShelter.AnimalType.cat));


            Console.WriteLine(shelter.DeQueueAny());
            Console.WriteLine(shelter.DeQueueAny());
            Console.WriteLine(shelter.DeQueueCat());
            Console.WriteLine(shelter.DeQueueDog());
            Console.WriteLine(shelter.DeQueueDog());
            Console.WriteLine(shelter.DeQueueCat());
            Console.WriteLine(shelter.DeQueueCat());

            while (shelter.Count > 0)
                Console.WriteLine(shelter.DeQueueAny());

        }

        public void Run2()
        {
            AnimalShelter2 shelter = new AnimalShelter2();

            shelter.Enqueue(new AnimalShelter2.Animal(5, AnimalShelter2.AnimalType.dog));
            shelter.Enqueue(new AnimalShelter2.Animal(3, AnimalShelter2.AnimalType.dog));
            shelter.Enqueue(new AnimalShelter2.Animal(10, AnimalShelter2.AnimalType.dog));
            shelter.Enqueue(new AnimalShelter2.Animal(2, AnimalShelter2.AnimalType.dog));
            shelter.Enqueue(new AnimalShelter2.Animal(11, AnimalShelter2.AnimalType.dog));
            shelter.Enqueue(new AnimalShelter2.Animal(6, AnimalShelter2.AnimalType.dog));
            shelter.Enqueue(new AnimalShelter2.Animal(9, AnimalShelter2.AnimalType.cat));
            shelter.Enqueue(new AnimalShelter2.Animal(21, AnimalShelter2.AnimalType.cat));
            shelter.Enqueue(new AnimalShelter2.Animal(15, AnimalShelter2.AnimalType.cat));
            shelter.Enqueue(new AnimalShelter2.Animal(19, AnimalShelter2.AnimalType.cat));
            shelter.Enqueue(new AnimalShelter2.Animal(1, AnimalShelter2.AnimalType.cat));
            shelter.Enqueue(new AnimalShelter2.Animal(3, AnimalShelter2.AnimalType.cat));


            Console.WriteLine(shelter.DeQueueAny());
            Console.WriteLine(shelter.DeQueueAny());
            Console.WriteLine(shelter.DeQueueCat());
            Console.WriteLine(shelter.DeQueueDog());
            Console.WriteLine(shelter.DeQueueDog());
            Console.WriteLine(shelter.DeQueueCat());
            Console.WriteLine(shelter.DeQueueCat());

            while (shelter.Count > 0)
                Console.WriteLine(shelter.DeQueueAny());

        }
    }
}
