using Raylib_CsLo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avaruuspeli
{
    internal class OptionsMenu
    {
        MenuCreator mc = new MenuCreator();
        public EventHandler BackPressed;
        public float volume = 1.0f;
        public int spinnerValue = 0;
        bool spinnerEdit;

        public unsafe void Draw()
        {

            // En tiedä miksi ei toimi ilman apumuuttujaa, mutta en jaksa ottaa selvää. Jos toimii niin toimii.
            int spinnerValue2 = spinnerValue;

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Raylib.BLACK);

            float x = Raylib.GetScreenWidth() * 0.5f;
            float y = Raylib.GetScreenHeight() * 0.25f;
            float menuWidth = 120;
            float menuHeight = 40;
            float between = 10;



            mc.StartMenu(x, y, menuWidth, menuHeight, between);

            mc.Label("Options");


            volume = mc.SliderBar($"volume {volume * 100:0}", "Silent", "Max", volume, 0, 1);

            if (mc.Spinner("Testi", &spinnerValue2, 0, 10, spinnerEdit))
            {
                spinnerEdit = !spinnerEdit;
            }

            spinnerValue = spinnerValue2;

            if (mc.Button("Back"))
            {
                Raylib.SetMasterVolume(volume);
                BackPressed.Invoke(this, EventArgs.Empty);
            }



            Raylib.EndDrawing();
        }
    }
}
