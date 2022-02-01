using BluinoNet;
using BluinoNet.Modules;
using System;
using System.Collections;
using System.Diagnostics;
using System.Threading;

namespace DemoESP32BasicShield
{
    public class Program
    {
        static ESP32BasicShield board;
        public static void Main()
        {
            bool isOn = false;
            //colok led ke pin 32, buzzer ke pin 16, button pin 13, relay pin 12
            board = new ESP32BasicShield(ESP32Pins.IO13, ESP32Pins.IO16, ESP32Pins.IO32, ESP32Pins.IO12);

            //play sound
            PlaySound();

            //button
            board.BoardButton.ButtonPressed += (a, b) => {
                //led
                board.BoardLed.TurnOn();
                //relay
                board.BoardRelay.TurnOn();
            };
            board.BoardButton.ButtonReleased += (a, b) => {
                board.BoardLed.TurnOff();
                board.BoardRelay.TurnOff();
            };

            Thread.Sleep(Timeout.Infinite);

            // Browse our samples repository: https://github.com/nanoframework/samples
            // Check our documentation online: https://docs.nanoframework.net/
            // Join our lively Discord community: https://discord.gg/gCyBu8T
        }
        //play sound
        #region buzzer
        static ArrayList music = new ArrayList();
        static void PlaySound()
        {

            Tunes.MusicNote note = new Tunes.MusicNote(Tunes.Tone.C4, 400);

            music.Add(note);

            //up
            PlayNote(Tunes.Tone.C4);
            PlayNote(Tunes.Tone.D4);
            PlayNote(Tunes.Tone.E4);
            PlayNote(Tunes.Tone.F4);
            PlayNote(Tunes.Tone.G4);
            PlayNote(Tunes.Tone.A4);
            PlayNote(Tunes.Tone.B4);
            PlayNote(Tunes.Tone.C5);

            // back down
            PlayNote(Tunes.Tone.B4);
            PlayNote(Tunes.Tone.A4);
            PlayNote(Tunes.Tone.G4);
            PlayNote(Tunes.Tone.F4);
            PlayNote(Tunes.Tone.E4);
            PlayNote(Tunes.Tone.D4);
            PlayNote(Tunes.Tone.C4);

            // arpeggio
            PlayNote(Tunes.Tone.E4);
            PlayNote(Tunes.Tone.G4);
            PlayNote(Tunes.Tone.C5);
            PlayNote(Tunes.Tone.G4);
            PlayNote(Tunes.Tone.E4);
            PlayNote(Tunes.Tone.C4);

            //tunes.Play();

            //Thread.Sleep(100);

            PlayNote(Tunes.Tone.E4);
            PlayNote(Tunes.Tone.G4);
            PlayNote(Tunes.Tone.C5);
            PlayNote(Tunes.Tone.G4);
            PlayNote(Tunes.Tone.E4);
            PlayNote(Tunes.Tone.C4);
            var notes = (Tunes.MusicNote[])music.ToArray(typeof(Tunes.MusicNote));
            board.BoardBuzzer.Play(notes);

        }
        static void PlayNote(Tunes.Tone tone)
        {
            Tunes.MusicNote note = new Tunes.MusicNote(tone, 200);

            music.Add(note);
        }
        #endregion
    }
}
