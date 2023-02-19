
using FinalTaskMarketSimulation;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalTaskMarketSimulation
{
    public class Market
    {

        public double rating = 1;//market ratingi
        public double marketMoney = 1200;//default money
        public double dayCash = 0;//gundelik gelir
        public int countWorker = 2;//ishci sayi
        public Market() { }

        public Stack<Vegetable> xiyar = new();
        public Stack<Vegetable> pomidor = new();
        public Stack<Vegetable> kok = new();
        public Stack<Vegetable> kartof = new();
        public Stack<Vegetable> badimcan = new();
        public Dictionary<string, Stack<Vegetable>> dict = new();
        public Queue<Employee> employeesXiyar = new();
        public Queue<Employee> employeesPomidor = new();
        public Queue<Employee> employeesKok = new();
        public Queue<Employee> employeesKartof = new();
        public Queue<Employee> employeesBadimcan = new();

        Stack<Vegetable> vegetableStack = new();
    }
}