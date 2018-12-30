
using System.Collections;

using System.Collections.Generic;

using UnityEngine;



public class TestAccord : MonoBehaviour
{



    private int[][] accords = new int[10][];



    private int[] littlestar;



    private int[] molihua;



    private int[] sakura;



    private int index = 0;



    private MIDIPlayer player;



    private int lastSecond = -1;

    private int lastNote = -1;



    // Use this for initialization

    void Start()
    {

        this.accords[0] = new int[3];

        this.accords[0][0] = 60;

        this.accords[0][0] = 64;

        this.accords[0][0] = 67;



        this.littlestar = new int[]{

            60, 60, 67, 67, 69, 69, 67, -1,

            65, 65, 64, 64, 62, 62, 60, -1,

            67, 67, 65, 65, 64, 64, 62, -1,

            67, 67, 65, 65, 64, 64, 62, -1,

            60, 60, 67, 67, 69, 69, 67, -1,

            65, 65, 64, 64, 62, 62, 60, -1,

        };



        this.molihua = new int[]{

            NOTES.E3,       -1, NOTES.E3, NOTES.G3, NOTES.A3, NOTES.C4, NOTES.C4, NOTES.A3,

            NOTES.G3,       -1, NOTES.G3, NOTES.A3, NOTES.G3,       -1,       -1,       -1,

            NOTES.E3,       -1, NOTES.E3, NOTES.G3, NOTES.A3, NOTES.C4, NOTES.C4, NOTES.A3,

            NOTES.G3,       -1, NOTES.G3, NOTES.A3, NOTES.G3,       -1,       -1,       -1,

            NOTES.G3,       -1, NOTES.G3,       -1, NOTES.G3,       -1, NOTES.E3, NOTES.G3,

            NOTES.A3,       -1, NOTES.A3,       -1, NOTES.G3,       -1,       -1,       -1,

            NOTES.E3,       -1, NOTES.D3, NOTES.E3, NOTES.G3,       -1, NOTES.E3, NOTES.D3,

            NOTES.C3,       -1, NOTES.C3, NOTES.D3, NOTES.C3,       -1,       -1,       -1,

            NOTES.E3, NOTES.D3, NOTES.C3, NOTES.E3, NOTES.D3,       -1,       -1, NOTES.D3,

            NOTES.G3,       -1, NOTES.A3, NOTES.C4, NOTES.G3,       -1,       -1,       -1,

            NOTES.D3,       -1, NOTES.E3, NOTES.G3, NOTES.D3, NOTES.E3, NOTES.C3, NOTES.A2,

            NOTES.G2,       -1,       -1,       -1, NOTES.A2,       -1, NOTES.C3,       -1,

            NOTES.D3,       -1,       -1, NOTES.E3, NOTES.C3, NOTES.D3, NOTES.C3, NOTES.A2,

            NOTES.G2,       -1,

        };



        this.sakura = new int[]{

            NOTES.A3, -1, NOTES.A3,       -1, NOTES.B3,       -1,       -1, -1,

            NOTES.A3, -1, NOTES.A3,       -1, NOTES.B3,       -1,       -1, -1,

            NOTES.A3, -1, NOTES.B3,       -1, NOTES.C4,       -1, NOTES.B3, -1,

            NOTES.A3, -1, NOTES.B3, NOTES.A3, NOTES.F3,       -1,       -1, -1,

            NOTES.E3, -1, NOTES.C3,       -1, NOTES.E3,       -1, NOTES.F3, -1,

            NOTES.E3, -1, NOTES.E3, NOTES.C3, NOTES.B2,       -1,       -1, -1,

            NOTES.A3, -1, NOTES.B3,       -1, NOTES.C4,       -1, NOTES.B3, -1,

            NOTES.A3, -1, NOTES.B3, NOTES.A3, NOTES.F3,       -1,       -1, -1,

            NOTES.E3, -1, NOTES.C3,       -1, NOTES.E3,       -1, NOTES.F3, -1,

            NOTES.E3, -1, NOTES.E3, NOTES.C3, NOTES.B2,       -1,       -1, -1,

            NOTES.A3, -1, NOTES.A3,       -1, NOTES.B3,       -1,       -1, -1,

            NOTES.A3, -1, NOTES.A3,       -1, NOTES.B3,       -1,       -1, -1,

            NOTES.E3, -1, NOTES.F3,       -1, NOTES.B3, NOTES.A3, NOTES.F3, -1,

            NOTES.E3, -1,

        };



        this.player = this.GetComponent<MIDIPlayer>();

    }



    // Update is called once per frame

    void Update()
    {

        // int second = (int)(Time.fixedTime * 3);



        /*if (second < this.littlestar.Length && lastSecond != second)

        {

            player.setNote(this.littlestar[second]);

            lastSecond = second;

            lastNote = this.littlestar[second];

        }*/



        /*if (second < this.molihua.Length && lastSecond != second)

        {

            player.setNote(this.molihua[second]);

            lastSecond = second;

            lastNote = this.molihua[second];

        }*/



        /*
        if (second < this.sakura.Length && lastSecond != second)
        {

            player.setNote(this.sakura[second]);

            lastSecond = second;

            lastNote = this.sakura[second];

        }
        */



    }

}
