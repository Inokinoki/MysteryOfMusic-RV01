

using System.Collections.Generic;
/**
* A configuration class passing params between scenes
*/
public class Configuration {

    // For PandanGenerator
    public static float enermyGenerateMinInterval = 8.0f;
    public static float enermyGenerateMaxInterval = 15.0f;

    public static List<int> availablePanda = new List<int>(new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 });

    // For PlayerController
    public static List<int> availableNotes = new List<int>(new int[]{0, 2, 4, 5, 7, 9, 11});

}
