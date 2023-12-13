// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

Game.Start(m =>
{
    m.IronOre().Miner().IronIngot().Constructor().IronRod();
    var split = m.IronOre().Miner().IronIngot();//.Split();
});
