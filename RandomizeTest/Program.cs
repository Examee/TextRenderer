


using Randomize;
Dictionary<int, int> distribution = new Dictionary<int,int>();
RandomContext context = new RandomContext();

for( int i = -10; i <= 10; i++) {
    distribution[i] = 0;
}

foreach (int i in context.NextN_Random_Method(1000,-10,11)) {
    if (distribution.ContainsKey(i)) {
        distribution[i]++;
    } else {
        distribution[i] = 1;
    }
    Console.WriteLine(i);
}

foreach (KeyValuePair<int,int> kvp in distribution) {
    Console.WriteLine("{0}: {1}", kvp.Key, kvp.Value);
}

List<int> list = new List<int> {12,13,14,45,3,6,7 };
var sample = context.SampleWithReplacement(list, 10);
foreach (int i in sample) {
    Console.WriteLine(i);
}

Dictionary<int, int> distribution2 = new Dictionary<int, int>();
for( int i = -10; i <= 10; i++) {
    distribution2[i] = 0;
}

foreach (int i in RandomContext.NextN_RandomGenerator_Method(100, -10, 10)) {
    if (distribution2.ContainsKey(i)) {
        distribution2[i]++;
    } else {
        distribution2[i] = 1;
    }
    Console.WriteLine(i);
}

foreach (KeyValuePair<int, int> kvp in distribution2) {
    Console.WriteLine("{0}: {1}", kvp.Key, kvp.Value);
}

Dictionary<int, int> distribution3 = new Dictionary<int, int>();
for (int i = -10; i <= 10; i++) {
    distribution3[i] = 0;
}

foreach (int i in RandomContext.NextN_MersenneTwister_Method(100, -10, 10)) {
    if (distribution3.ContainsKey(i)) {
        distribution3[i]++;
    } else {
        distribution3[i] = 1;
    }
    Console.WriteLine(i);
}

foreach (KeyValuePair<int, int> kvp in distribution3) {
    Console.WriteLine("{0}: {1}", kvp.Key, kvp.Value);
}