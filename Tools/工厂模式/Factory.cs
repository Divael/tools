using System;

namespace 工厂模式
{
    /// <summary>
    /// 汽车抽象类
    /// </summary>
    public abstract class Car
    {
        // 开始行驶
        public abstract void Go();
    }

    /// <summary>
    /// 红旗汽车
    /// </summary>
    public class HongQiCar : Car
    {
        public override void Go()
        {
            Console.WriteLine("红旗汽车开始行驶了！");
        }
    }

    /// <summary>
    /// 奥迪汽车
    /// </summary>
    public class AoDiCar : Car
    {
        public override void Go()
        {
            Console.WriteLine("奥迪汽车开始行驶了");
        }
    }

    /// <summary>
    /// 抽象工厂类
    /// </summary>
    public abstract class Factory
    {
        // 工厂方法
        public abstract Car CreateCar();
    }

    /// <summary>
    /// 红旗汽车工厂类
    /// </summary>
    public class HongQiCarFactory : Factory
    {
        /// <summary>
        /// 负责生产红旗汽车
        /// </summary>
        /// <returns></returns>
        public override Car CreateCar()
        {
            return new HongQiCar();
        }
    }

    /// <summary>
    /// 奥迪汽车工厂类
    /// </summary>
    public class AoDiCarFactory : Factory
    {
        /// <summary>
        /// 负责创建奥迪汽车
        /// </summary>
        /// <returns></returns>
        public override Car CreateCar()
        {
            return new AoDiCar();
        }
    }

    ///// <summary>
    ///// 客户端调用
    ///// </summary>
    //class Client
    //{
    //    static void Main(string[] args)
    //    {
    //        // 初始化创建汽车的两个工厂
    //        Factory hongQiCarFactory = new HongQiCarFactory();
    //        Factory aoDiCarFactory = new AoDiCarFactory();

    //        // 生产一辆红旗汽车
    //        Car hongQi = hongQiCarFactory.CreateCar();
    //        hongQi.Go();

    //        //生产一辆奥迪汽车
    //        Car aoDi = aoDiCarFactory.CreateCar();
    //        aoDi.Go();

    //        Console.Read();
    //    }
    //}
}