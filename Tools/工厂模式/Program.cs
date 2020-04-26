using System;
using System.Data.Common;

namespace 工厂模式
{
    /// <summary>
    /// 下面以不同系列房屋的建造为例子演示抽象工厂模式
    /// 因为每个人的喜好不一样，我喜欢欧式的，我弟弟就喜欢现代的
    /// 客户端调用
    /// </summary>
    class Client
    {
        static void Main(string[] args)
        {
            // 哥哥的欧式风格的房子
            AbstractFactory europeanFactory = new EuropeanFactory();
            europeanFactory.CreateRoof().Create();
            europeanFactory.CreateFloor().Create();
            europeanFactory.CreateWindow().Create();
            europeanFactory.CreateDoor().Create();


            //弟弟的现代风格的房子
            AbstractFactory modernizationFactory = new ModernizationFactory();
            modernizationFactory.CreateRoof().Create();
            modernizationFactory.CreateFloor().Create();
            modernizationFactory.CreateWindow().Create();
            modernizationFactory.CreateDoor().Create();
            Console.Read();

            DbProviderFactory dbProviderFactory;

        }
    }

    /// <summary>
    /// 抽象工厂类，提供创建不同类型房子的接口
    /// </summary>
    public abstract class AbstractFactory
    {
        // 抽象工厂提供创建一系列产品的接口，这里作为例子，只给出了房顶、地板、窗户和房门创建接口
        public abstract Roof CreateRoof();
        public abstract Floor CreateFloor();
        public abstract Window CreateWindow();
        public abstract Door CreateDoor();
    }

    /// <summary>
    /// 欧式风格房子的工厂，负责创建欧式风格的房子
    /// </summary>
    public class EuropeanFactory : AbstractFactory
    {
        // 制作欧式房顶
        public override Roof CreateRoof()
        {
            return new EuropeanRoof();
        }

        // 制作欧式地板
        public override Floor CreateFloor()
        {
            return new EuropeanFloor();
        }

        // 制作欧式窗户
        public override Window CreateWindow()
        {
            return new EuropeanWindow();
        }

        // 制作欧式房门
        public override Door CreateDoor()
        {
            return new EuropeanDoor();
        }
    }

    /// <summary>
    /// 现在风格房子的工厂，负责创建现代风格的房子
    /// </summary>
    public class ModernizationFactory : AbstractFactory
    {
        // 制作现代房顶
        public override Roof CreateRoof()
        {
            return new ModernizationRoof();
        }

        // 制作现代地板
        public override Floor CreateFloor()
        {
            return new ModernizationFloor();
        }

        // 制作现代窗户
        public override Window CreateWindow()
        {
            return new ModernizationWindow();
        }

        // 制作现代房门
        public override Door CreateDoor()
        {
            return new ModernizationDoor();
        }
    }

    /// <summary>
    /// 房顶抽象类，子类的房顶必须继承该类
    /// </summary>
    public abstract class Roof
    {
        /// <summary>
        /// 创建房顶
        /// </summary>
        public abstract void Create();
    }

    /// <summary>
    /// 地板抽象类，子类的地板必须继承该类
    /// </summary>
    public abstract class Floor
    {
        /// <summary>
        /// 创建地板
        /// </summary>
        public abstract void Create();
    }

    /// <summary>
    /// 窗户抽象类，子类的窗户必须继承该类
    /// </summary>
    public abstract class Window
    {
        /// <summary>
        /// 创建窗户
        /// </summary>
        public abstract void Create();
    }

    /// <summary>
    /// 房门抽象类，子类的房门必须继承该类
    /// </summary>
    public abstract class Door
    {
        /// <summary>
        /// 创建房门
        /// </summary>
        public abstract void Create();
    }

    /// <summary>
    /// 欧式地板类
    /// </summary>
    public class EuropeanFloor : Floor
    {
        public override void Create()
        {
            Console.WriteLine("创建欧式的地板");
        }
    }


    /// <summary>
    /// 欧式的房顶
    /// </summary>
    public class EuropeanRoof : Roof
    {
        public override void Create()
        {
            Console.WriteLine("创建欧式的房顶");
        }
    }


    /// <summary>
    ///欧式的窗户
    /// </summary>
    public class EuropeanWindow : Window
    {
        public override void Create()
        {
            Console.WriteLine("创建欧式的窗户");
        }
    }


    /// <summary>
    /// 欧式的房门
    /// </summary>
    public class EuropeanDoor : Door
    {
        public override void Create()
        {
            Console.WriteLine("创建欧式的房门");
        }
    }

    /// <summary>
    /// 现代的房顶
    /// </summary>
    public class ModernizationRoof : Roof
    {
        public override void Create()
        {
            Console.WriteLine("创建现代的房顶");
        }
    }

    /// <summary>
    /// 现代的地板
    /// </summary>
    public class ModernizationFloor : Floor
    {
        public override void Create()
        {
            Console.WriteLine("创建现代的地板");
        }
    }

    /// <summary>
    /// 现代的窗户
    /// </summary>
    public class ModernizationWindow : Window
    {
        public override void Create()
        {
            Console.WriteLine("创建现代的窗户");
        }
    }

    /// <summary>
    /// 现代的房门
    /// </summary>
    public class ModernizationDoor : Door
    {
        public override void Create()
        {
            Console.WriteLine("创建现代的房门");
        }
    }


}
