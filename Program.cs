using System;

namespace NortonCommanderMockup
{
    class Program
    {
        const int W = 80;
        const int H = 25;
        static char[,] sc = new char[H, W];
        static ConsoleColor[,] fc = new ConsoleColor[H, W];
        static ConsoleColor[,] bc = new ConsoleColor[H, W];

        static void Set(int x, int y, char c, ConsoleColor f, ConsoleColor b)
        {
            if (x >= 0 && x < W && y >= 0 && y < H)
            { sc[y, x] = c; fc[y, x] = f; bc[y, x] = b; }
        }

        static void Txt(int x, int y, string s, ConsoleColor f, ConsoleColor b)
        {
            for (int i = 0; i < s.Length; i++) Set(x + i, y, s[i], f, b);
        }

        static string TruncPad(string s, int w)
        {
            if (s.Length <= w) return s.PadRight(w);
            return s.Substring(0, w - 1) + "~";
        }

        // Имя слева, расширение прижато к правому краю
        static string FormatFile(string name, string ext, int w)
        {
            if (string.IsNullOrEmpty(ext)) return TruncPad(name, w);
            if (ext.Length >= w) return TruncPad(ext, w);
            int nameW = w - ext.Length - 1;
            if (nameW < 1) return TruncPad(name + " " + ext, w);
            return TruncPad(name, nameW) + " " + ext;
        }

        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.CursorVisible = false;
            try { Console.SetBufferSize(W, H); } catch { }
            try { Console.SetWindowSize(W, H); } catch { }

            var WB = ConsoleColor.White;
            var BG = ConsoleColor.Blue;
            for (int y = 0; y < H; y++)
                for (int x = 0; x < W; x++)
                { sc[y, x] = ' '; fc[y, x] = WB; bc[y, x] = BG; }

            DrawMenu();
            DrawTopBorders();
            DrawLeftPanel();
            DrawRightPanel();
            DrawBottomBorders();
            DrawPrompt();
            DrawFuncBar();

            Render();
            Console.ReadKey(true);
        }

        static void DrawMenu()
        {
            var cy = ConsoleColor.Cyan;
            var ye = ConsoleColor.Yellow;
            for (int x = 0; x < W; x++) Set(x, 0, ' ', ye, cy);

            Txt(1, 0, "Левая", ye, cy);
            Txt(8, 0, "Файл", ye, cy);
            Txt(15, 0, "Диск", ye, cy);
            Txt(22, 0, "Команды", ye, cy);
            Txt(32, 0, "Правая", ye, cy);
            Txt(75, 0, "8 30", ye, cy);
        }

        static void DrawTopBorders()
        {
            var w = ConsoleColor.White;
            var b = ConsoleColor.Blue;
            var cy = ConsoleColor.Cyan;
            var bl = ConsoleColor.Black;

            Set(0, 1, '╔', w, b);
            for (int x = 1; x <= 16; x++) Set(x, 1, '═', w, b);
            Txt(17, 1, " C:\\NC ", bl, cy);
            for (int x = 24; x <= 38; x++) Set(x, 1, '═', w, b);
            Set(39, 1, '╗', w, b);

            Set(40, 1, '╔', w, b);
            for (int x = 41; x <= 56; x++) Set(x, 1, '═', w, b);
            Txt(57, 1, " C:\\NC ", bl, cy);
            for (int x = 64; x <= 78; x++) Set(x, 1, '═', w, b);
            Set(79, 1, '╗', w, b);
        }

        
        // Три колонки: 1-12, 14-25, 27-38. Разделители │ на 13 и 26.
        static void DrawLeftPanel()
        {
            var w = ConsoleColor.White;
            var b = ConsoleColor.Blue;

            // --- Шапка ---
            Set(0, 2, '║', w, b);
            Txt(1, 2, "C:\\", w, b);
            Set(4, 2, ' ', w, b);
            Txt(5, 2, "ИМЯ", w, b);
            for (int x = 8; x <= 12; x++) Set(x, 2, ' ', w, b);
            Set(13, 2, '│', w, b);
            Txt(14, 2, "ИМЯ", w, b);
            for (int x = 17; x <= 25; x++) Set(x, 2, ' ', w, b);
            Set(26, 2, '│', w, b);
            Txt(27, 2, "ИМЯ", w, b);
            for (int x = 30; x <= 38; x++) Set(x, 2, ' ', w, b);
            Set(39, 2, '║', w, b);

            // --- Разделитель ---
            Set(0, 3, '╟', w, b);
            Set(13, 3, '┼', w, b);
            Set(26, 3, '┼', w, b);
            Set(39, 3, '╢', w, b);
            for (int x = 1; x <= 38; x++)
                if (x != 13 && x != 26) Set(x, 3, '─', w, b);

            // --- Данные ( 4..20, 17 строк) ---
            var col1 = new (string n, string e)[]
            {
                ("..", ""),         ("Ajaccgdo::", ""), ("nc", "cfg"),
                ("nc_exit", "com"), ("telemax", "dat"), ("nc_exit", "doc"),
                ("123view", "exe"), ("arcview", "exe"), ("bitmap", "exe"),
                ("clp2dib", "exe"), ("dbview", "exe"),  ("draw2wmf", "exe"),
                ("drw2wmf", "exe"), ("ico2dib", "exe"), ("msp2dib", "exe"),
                ("nc", "exe"),      ("ncclean", "exe"),
            };
            var col2 = new (string n, string e)[]
            {
                ("ncdd", "exe"),       ("ncedit", "exe"), ("ncff", "exe"),
                ("nclabel", "exe"),    ("ncmain", "exe"),    ("ncnet", "exe"),
                ("ncsf", "exe"),       ("ncsi", "exe"),      ("nczip", "exe"),
                ("packer", "exe"),     ("paraview", "exe"),  ("pct2dib", "exe"),
                ("playwave", "exe"),("q&aview", "exe"),("rbview", "exe"),
                ("refview", "exe"), ("saver", "exe"),
            };
            var col3 = new (string n, string e)[]
            {
                ("telemax", "exe"), ("tif2dib", "exe"), ("vector", "exe"),
                ("wpb2dib", "exe"), ("wpv2wmf", "exe"), ("wpview", "exe"),
                ("nc", "ext"),      ("nc", "fil"),        ("ncpscrip", "hdr"),
                ("nc", "hlp"),        ("ncff", "hlp"),       ("telemax", "hlp"),
                ("nc", "lco"),      ("nc", "ini"),        ("ncclean", "ini"),
                ("norton", "ini"),  ("telemax", "ini"),
            };

            for (int i = 0; i < 17; i++)
            {
                int y = 4 + i;
                Set(0, y, '║', w, b);
                Set(13, y, '│', w, b);
                Set(26, y, '│', w, b);
                Set(39, y, '║', w, b);

                Txt(1, y, FormatFile(col1[i].n, col1[i].e, 12), w, b);
                Txt(14, y, FormatFile(col2[i].n, col2[i].e, 12), w, b);
                Txt(27, y, FormatFile(col3[i].n, col3[i].e, 12), w, b);
            }

            // --- КАТАЛОГ ) ---
            Set(0, 21, '║', w, b);
            Set(39, 21, '║', w, b);
            string kat = "..           ►КАТАЛОГ◄ 11.10.02  19:48";
            Txt(1, 21, kat + new string(' ', 38 - kat.Length), w, b);
        }

        // Колонки: имя 41-56, размер 58-63, дата 65-72, время 74-78
        // Разделители │ на 57, 64, 73.
        static void DrawRightPanel()
        {
            var w = ConsoleColor.White;
            var b = ConsoleColor.Blue;
            var cy = ConsoleColor.Cyan;
            var bl = ConsoleColor.Black;

            // --- Шапка ---
            Set(40, 2, '║', w, b);
            Txt(41, 2, "C:\\", w, b);
            Set(44, 2, ' ', w, b);
            Txt(45, 2, "ИМЯ", w, b);
            for (int x = 48; x <= 56; x++) Set(x, 2, ' ', w, b);
            Set(57, 2, '│', w, b);
            for (int x = 58; x <= 63; x++) Set(x, 2, ' ', bl, cy);
            Txt(58, 2, "Размер", bl, cy);
            Set(64, 2, '│', w, b);
            for (int x = 65; x <= 72; x++) Set(x, 2, ' ', bl, cy);
            Txt(65, 2, " Дата  ", bl, cy);
            Set(73, 2, '│', w, b);
            for (int x = 74; x <= 78; x++) Set(x, 2, ' ', bl, cy);
            Txt(74, 2, "Время", bl, cy);
            Set(79, 2, '║', w, b);

            // --- Разделитель ---
            Set(40, 3, '╟', w, b);
            Set(57, 3, '┼', w, b);
            Set(64, 3, '┼', w, b);
            Set(73, 3, '┼', w, b);
            Set(79, 3, '╢', w, b);
            for (int x = 41; x <= 78; x++)
                if (x != 57 && x != 64 && x != 73) Set(x, 3, '─', w, b);

            // --- Строка родительской папки) ---
            Set(40, 4, '║', w, b);
            Txt(41, 4, "..", w, b);
            for (int x = 45; x <= 56; x++) Set(x, 4, ' ', bl, cy);
            Txt(45, 4, "►КАТАЛОГ◄", bl, cy);
            Set(57, 4, '│', w, b);
            Set(64, 4, '│', w, b);
            Set(73, 4, '│', w, b);
            Txt(65, 4, "11.10.02", w, b);
            Txt(74, 4, "19:48", w, b);
            Set(79, 4, '║', w, b);

            // --- Данные ( 5..20, 16 файлов) ---
            var files = new (string n, string e, string size, string date, string time)[]
            {
                ("123view",   "exe", "128380", "25.05.95", " 5:00"),
                ("4372ansi",  "set", "   255", "25.05.95", " 5:00"),
                ("8502ansi",  "set", "   255", "25.05.95", " 5:00"),
                ("8632ansi",  "set", "   255", "25.05.95", " 5:00"),
                ("8652ansi",  "set", "   255", "25.05.95", " 5:00"),
                ("8662ansi",  "set", "   255", "25.05.95", " 5:00"),
                ("Ajaccgdo::","",    "417392", "12.10.02", " 9:02"),
                ("ansi2437",  "set", "   255", "25.05.95", " 5:00"),
                ("ansi2850",  "set", "   255", "25.05.95", " 5:00"),
                ("ansi2863",  "set", "   255", "25.05.95", " 5:00"),
                ("ansi2865",  "set", "   255", "25.05.95", " 5:00"),
                ("ansi2866",  "set", "   255", "25.05.95", " 5:00"),
                ("arcview",   "exe", " 81738", "25.05.95", " 5:00"),
                ("bitmap",    "exe", " 54805", "25.05.95", " 5:00"),
                ("bug",       "nss", " 16133", "25.05.95", " 5:00"),
                ("bungee",    "nss", " 41914", "25.05.95", " 5:00"),
            };

            for (int i = 0; i < 16; i++)
            {
                int y = 5 + i;
                Set(40, y, '║', w, b);
                Set(57, y, '│', w, b);
                Set(64, y, '│', w, b);
                Set(73, y, '│', w, b);
                Set(79, y, '║', w, b);

                Txt(41, y, "    ", w, b);
                Txt(45, y, FormatFile(files[i].n, files[i].e, 12), w, b);
                Txt(58, y, files[i].size, w, b);
                Txt(65, y, files[i].date, w, b);
                Txt(74, y, files[i].time, w, b);
            }

            // --- КАТАЛОГ ---
            Set(40, 21, '║', w, b);
            Set(79, 21, '║', w, b);
            string kat = "..           ►КАТАЛОГ◄ 11.10.02  19:48";
            Txt(41, 21, kat + new string(' ', 38 - kat.Length), w, b);
        }

        static void DrawBottomBorders()
        {
            var w = ConsoleColor.White;
            var b = ConsoleColor.Blue;

            Set(0, 22, '╚', w, b);
            for (int x = 1; x <= 38; x++) Set(x, 22, '═', w, b);
            Set(39, 22, '╝', w, b);

            Set(40, 22, '╚', w, b);
            for (int x = 41; x <= 78; x++) Set(x, 22, '═', w, b);
            Set(79, 22, '╝', w, b);
        }

        static void DrawPrompt()
        {
            Txt(0, 23, "C:\\NC>_", ConsoleColor.White, ConsoleColor.Blue);
        }

        static void DrawFuncBar()
        {
            var cy = ConsoleColor.Cyan;
            var bl = ConsoleColor.Black;
            var b = ConsoleColor.Blue;

            for (int x = 0; x < W; x++) Set(x, 24, ' ', bl, b);

            var items = new (string num, string label)[]
            {
                ("1",  "Помощь"), ("2",  "Вызов"),  ("3",  "Чтение"),
                ("4",  "Правка"), ("5",  "Копия"),  ("6",  "НовИмя"),
                ("7",  "НовКат"), ("8",  "Удал-е"), ("9",  "Меню"),
                ("10", "Выход"),
            };

            int pos = 0;
            foreach (var it in items)
            {
                if (pos >= W) break;
                Txt(pos, 24, it.num, bl, cy); pos += it.num.Length;
                Txt(pos, 24, it.label, bl, cy); pos += it.label.Length;
                if (pos < W) { Set(pos, 24, ' ', bl, b); pos++; }
            }
        }

        static void Render()
        {
            var curF = ConsoleColor.Gray;
            var curB = ConsoleColor.Black;

            for (int y = 0; y < H; y++)
            {
                Console.SetCursorPosition(0, y);
                for (int x = 0; x < W; x++)
                {
                    if (fc[y, x] != curF || bc[y, x] != curB)
                    {
                        curF = fc[y, x]; curB = bc[y, x];
                        Console.ForegroundColor = curF;
                        Console.BackgroundColor = curB;
                    }
                    Console.Write(sc[y, x]);
                }
            }
            Console.ResetColor();
        }
    }
}