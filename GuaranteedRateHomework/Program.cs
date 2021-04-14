using System;
using System.Threading.Tasks;

namespace GuaranteedRateHomework
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            //make some Sample Data
            await DataGenerator.generateFile(10, " | ", "SampleInputPipe.txt");
            await DataGenerator.generateFile(10, ", ", "SampleInputComma.txt");
            await DataGenerator.generateFile(10, " ", "SampleInputSpace.txt");
        }
    }
}
