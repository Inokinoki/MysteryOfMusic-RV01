

using System.Collections.Generic;
/**
* A configuration class passing params between scenes
*/
public class Configuration {

    // For PandanGenerator
    public static float enermyGenerateMinInterval = 5.0f;
    public static float enermyGenerateMaxInterval = 15.0f;

    // For PlayerController
    public static List<int> availableNotes = new List<int>(new int[]{0, 2, 4, 5, 7, 9, 11});

}
