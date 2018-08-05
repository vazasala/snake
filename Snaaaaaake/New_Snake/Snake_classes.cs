using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using S_Constants;


//TODO: разобраться с получением новых координат башки змеи, переделать все в ооп шный лад, скрыть члены. да все это самое
// Вынести все константы
// Здесь определены все классы, напианные для игры 
namespace New_Snake
{

    // Перечисление - направление движения
    public enum E_Direction { Left, Up, Right, Down };
    // Перечисление - зажает цвет для рисования
    public enum E_Colors_to_paint { Part_of_snake, Food, Void };
    public struct S_Location // Класс, который реализует координату на игровой доске
    {

        // Координаты
        private int x;
        private int y;
        public int X { get { return x; } set { x = value; } }
        public int Y { get { return y; } set { y = value; } }
        // Конструкторы
        public S_Location(int a_x, int a_y)
        {
            x = a_x;
            y = a_y;
        }
        public S_Location(S_Location a_Location)
        {
            x = a_Location.X;
            y = a_Location.Y;
        }

        public static bool operator ==(S_Location a_Loc1, S_Location a_Loc2)
        {
            if ((a_Loc1.X == a_Loc2.X) & (a_Loc1.Y == a_Loc2.Y))
                return true;
            else
                return false;
        }
        public static bool operator !=(S_Location a_Loc1, S_Location a_Loc2)
        {
            if ((a_Loc1.X != a_Loc2.X) || (a_Loc1.Y != a_Loc2.Y))
                return true;
            else
                return false;
        }
    }

    public class S_Point_on_Board // Структура, которая представляет клетку на игровой доске
    {
        // То есть - это кооридната
        private S_Location m_Location;
        public S_Location Location { get { return m_Location; } }
        // И значение, свободна ли сейчас это клетка
        private bool m_f_isFree;
        public bool Is_Free { get { return m_f_isFree; } set { m_f_isFree = value; } }
        public S_Point_on_Board(S_Location aLocation, bool a_f_isfree = true)
        {
            m_Location = aLocation;
            m_f_isFree = a_f_isfree;
        }



    }

    public static class S_Drawning // Класс, который будет прорисовывать объекты на игровой доске
    {
        private static Graphics s_Graphic_comp; // Компонента для рисования.
        private const int s_nsize_of_obj = S_Constants.S_Constants.c_n_size_of_objects_edge; // Размер грани одного квадратика на доске
        // Кисти для рисования
        private static Brush s_black_brush = new SolidBrush(Color.Black);
        private static Brush s_white_brush = new SolidBrush(Color.White);
        private static Brush s_red_brush = new SolidBrush(Color.Red);

        public static int Size_of_obj { get { return s_nsize_of_obj; } }

        // Функция рисования
        public static void Draw_an_item(S_Location a_Location, E_Colors_to_paint a_col)
        {
            Brush brush_to_paint = null;
            switch (a_col)
            {
                case E_Colors_to_paint.Part_of_snake:
                    brush_to_paint = s_black_brush;
                    break;
                case E_Colors_to_paint.Food:
                    brush_to_paint = s_red_brush;
                    break;
                case E_Colors_to_paint.Void:
                    brush_to_paint = s_white_brush;
                    break;

            }

            s_Graphic_comp.FillRectangle(brush_to_paint, a_Location.X * s_nsize_of_obj, a_Location.Y * s_nsize_of_obj, s_nsize_of_obj, s_nsize_of_obj);
        }
        public static void Set_graph_comp(Graphics a_graph_comp)
        {
            s_Graphic_comp = a_graph_comp;
        }
    }

    public abstract class S_Object_on_board
    {

        private S_Location m_Location; // Координаты объекта на игровой доске
        public S_Location Location { get { return m_Location; } set { m_Location = value; } }

        public S_Object_on_board(S_Location aLocation)
        {
            m_Location = aLocation;
            Draw_the_object();
        }
        public abstract void Draw_the_object();

    }

    // Класс, представляющий еду  и  Класс, представляющий "отход" змеи.
    // Эти 2 класса отличаются только цветом их "кисточки"

    public class S_Food : S_Object_on_board
    {
        public S_Food(S_Location aLocation)
            : base(aLocation)
        {
        }

        public override void Draw_the_object()
        {
            S_Drawning.Draw_an_item(Location, E_Colors_to_paint.Food);
        }


    }

    public class S_Remain : S_Object_on_board
    {

        public S_Remain(S_Location aLocation)
            : base(aLocation)
        { }
        public override void Draw_the_object()
        {
            S_Drawning.Draw_an_item(Location, E_Colors_to_paint.Part_of_snake);
        }
    }

    // Базовый класс для "частички" змеи

    public class S_Body_Block : S_Object_on_board
    {
        // Направление движение этого блока
        private E_Direction m_Direction;
        // Ссылка на следующий к голове кусок тела змеи, у головы она равна null
        private S_Body_Block m_Next_Block;
        public S_Body_Block Next_Block { get { return m_Next_Block; } }
        public E_Direction Direction { get { return m_Direction; } set { m_Direction = value; } }

        public S_Body_Block(S_Location a_Location, E_Direction a_Direction, S_Body_Block a_Next_Body_Block = null)
            : base(a_Location)
        {
            m_Direction = a_Direction;
            m_Next_Block = a_Next_Body_Block;

        }

        // Функция, которая задает данному блоку координаты следующего к голове блока, вызов идет рекурсивно
        public virtual void Set_New_Coords()
        {
            if (m_Next_Block == null)
                return;
            else
            {
                Location = m_Next_Block.Location;
                m_Next_Block.Set_New_Coords();
            }
           
        }
        public override void Draw_the_object()
        {
            S_Drawning.Draw_an_item(Location, E_Colors_to_paint.Part_of_snake);
        }
    }

    // Голова змеи
    public class S_Head : S_Body_Block
    {
        public S_Head(S_Location a_Location, E_Direction a_Direction)
            : base(a_Location, a_Direction)
        { }
        // С учетом текущего направления выдает новую позицию голову змеи
        public S_Location Get_New_Heads_Coords()
        {
            S_Location New_Location = Location;
            switch (Direction)
            {
                case E_Direction.Left:
                    New_Location.X--;
                    break;
                case E_Direction.Right:
                    New_Location.X++;
                    break;
                case E_Direction.Down:
                    New_Location.Y++;
                    break;
                case E_Direction.Up:
                    New_Location.Y--;
                    break;

            }
            return New_Location;
        }
        // Устанавливает новое направление голове 
        public void Set_New_Direction(E_Direction a_direction)
        {
            bool if_conceded_direction = false;
            switch (a_direction)
            {
                case E_Direction.Left:
                    if (Direction != E_Direction.Right)
                        if_conceded_direction = true;
                    break;
                case E_Direction.Right:
                    if (Direction != E_Direction.Left)
                        if_conceded_direction = true;
                    break;
                case E_Direction.Up:
                    if (Direction != E_Direction.Down)
                        if_conceded_direction = true;
                    break;
                case E_Direction.Down:
                    if (Direction != E_Direction.Up)
                        if_conceded_direction = true;
                    break;

            }
            if (if_conceded_direction)
                Direction = a_direction;
        }

        public void Set_Free_False(S_Game_Board a_game_board)
        {
            a_game_board.Set_Points_Value(false, Location);
        }
    }
        // Хвост змеи
        public class S_Tail : S_Body_Block
        {
            // Последняя позиция хвоста, когда он уже ушел и черный квадрат надо стереть
            private S_Location m_Last_Position;
            public S_Location Last_Position { get { return m_Last_Position; } }
            public S_Tail(S_Location a_Location, E_Direction a_Direction, S_Body_Block a_Next_Body_Block = null)
                : base(a_Location, a_Direction, a_Next_Body_Block)
            { }
            // Функция, здесь перегруженная, pапускает переназначение координат от хвоста до первого элемента за головой. сделана, чтобы обозначить логику происходящего
            public override void Set_New_Coords()

            {
                m_Last_Position = Location;
                Location = Next_Block.Location;
                Next_Block.Set_New_Coords();
            }
            // Стирает ушедший хвост
            public void Erase_the_Tail()
            {
                S_Drawning.Draw_an_item(m_Last_Position, E_Colors_to_paint.Void);
            }
            public void Set_Free_True(S_Game_Board a_game_board)
            {
                a_game_board.Set_Points_Value(true,m_Last_Position);
            }
        }

        public class S_Snake
        {
            // Голова
            private S_Head m_head;
            public S_Head Head { get { return m_head; } set { m_head = value; } }
            // Хвост
            private S_Tail m_tail;
            public S_Tail Tail { get { return m_tail; } set { m_tail = value; } }

            public S_Snake(E_Direction a_start_direction, params S_Location[] a_start_locations)
            {
                // В начале игры змейка выстраивается в линию
                // На данный момент решено что число начальных блоков строго равно четырем, поэтому этот код не обрабатывается через цикл, а кол-во не вынесено константным значением
                m_head = new S_Head(a_start_locations[0], a_start_direction);
                S_Body_Block BB1 = new S_Body_Block(a_start_locations[1], a_start_direction, m_head);
                S_Body_Block BB2 = new S_Body_Block(a_start_locations[2], a_start_direction, BB1);
                m_tail = new S_Tail(a_start_locations[3], a_start_direction, BB2);

            }
            // Задать новое направление голове
            public void Set_New_Direction(E_Direction a_direction)
            {
                m_head.Set_New_Direction(a_direction);
            }
            // С учетом текущего направления выдает новую позицию голову змеи
            public S_Location Get_New_Heads_Coords()
            {
                return m_head.Get_New_Heads_Coords();
            }
            public void Begin_To_Replace_Coords()
            {
                m_tail.Set_New_Coords();
            }

            // Задать голове новые координаты

            public void Set_Heads_New_Coords(S_Location a_location)
            {
                m_head.Location = a_location;
            }

            // Переназначить значение заблокированности клеток
            public void Set_New_Values_to_Fields(S_Game_Board a_game_board)
            {
                m_head.Set_Free_False(a_game_board);
                m_tail.Set_Free_True(a_game_board);
            }

            // После перераспределения координат нужно перерисовать змею - а именно: нарисовать черный квадрат там где теперь голова
            // и белый, там где на предыдущей итерации был хвост
           
            public void Draw_Objects()
            {
                m_head.Draw_the_object();
                m_tail.Erase_the_Tail();
            }

        }

        // Класс, который представляет игровую доску

        public class S_Game_Board
        {
            // границы доски, верхняя и правая грань так же кол-во строк и столбцов соответственно в доске
            private int m_n_left_bound;
            private int m_n_right_bound;
            private int m_n_up_bound;
            private int m_n_low_bound;
            public int Left_Bound { get { return m_n_left_bound; } }
            public int Right_Bound { get { return m_n_right_bound; } }
            public int Up_bound { get { return m_n_up_bound; } }
            public int Low_bound { get { return m_n_low_bound; } }
            // лист со всеми полями на доске
            private List<S_Point_on_Board> m_List_of_Points;

            public S_Point_on_Board this[int index]
            {
                get { return m_List_of_Points[index]; }
            }



            public S_Game_Board(Panel a_panel, int a_n_size_of_block)
            {
                m_n_left_bound = 0;
                m_n_right_bound = a_panel.Width / a_n_size_of_block - 1;
                m_n_up_bound = 0;
                m_n_low_bound = a_panel.Height / a_n_size_of_block - 1;

                // Начнем заполнять лист всеми возможными точками на доске

                m_List_of_Points = new List<S_Point_on_Board>();
                for (int i = 0; i <= m_n_low_bound; i++)
                {
                    for (int j = 0; j <= m_n_right_bound; j++)
                    {
                        S_Location New_Location = new S_Location(j, i);
                        S_Point_on_Board New_Point = new S_Point_on_Board(New_Location);
                        m_List_of_Points.Add(New_Point);
                    }
                }
            }

            // Функция, которая по заданным координатам возвращает клетку из списка

            public S_Point_on_Board Return_Point(S_Location a_location)
            {
                // через координаты можем посчитать номер этой клетки в списке.
                int number = (a_location.Y) * (m_n_right_bound+1) + (a_location.X);

               
                    return m_List_of_Points[number];
              
                    MessageBox.Show("Game over");
                
            }
            // Функция , которая задает значение занятости клеткам на доске
            public void Set_Points_Value(bool a_f_value, params S_Location[] a_locations)
            {
                S_Point_on_Board Point_to_Change_Value;
                foreach (S_Location Location in a_locations)
                {
                    // меняем значение
                    Point_to_Change_Value = Return_Point(Location);
                    Point_to_Change_Value.Is_Free = a_f_value;

                }

           


        }

            // Возвращает все свободные локации
            public S_Location[] Get_All_Free_Locations_Fileds()
            {
                S_Location[] array_of_free_locations = (from Fields in m_List_of_Points where Fields.Is_Free == true select Fields.Location).ToArray();
             //   S_Location[] array_of_free_locations = m_List_of_Points.Where(Field => Field.Is_Free == true).Select(Field => Field.Location).ToArray();
                return array_of_free_locations;
            }
        }


        // Класс, который представляет собой игру - все данные для нее.

        public class S_Game 
        {

            private S_Main_Wnd m_Main_Window;
            
            private S_Snake m_Snake;
            private S_Game_Board m_Board;
            private S_Food m_Food;
            private int m_n_number_of_points;

            private delegate void Speed_Mode_Delegate();
            private event Speed_Mode_Delegate Speed_Mode;
            // Различные вещи, которые нужны для игры: таймеры,рандомы...
            // Решил обойтись без мьютексов, так как как таковых двух параллельных потоков тут нет, и нет задачи параллелього выполнения
            // Поэтому задержка после нажатия клавиши(чтобы снова не нажать клавишу и увести змейку в себя(такой баг)), а так же невозможность сменить направлени в то время когда идет перерисовка и перераспределение
            // Будет выполняться посредством таймеров и "флагов"
            private Random m_random_for_food;
            private Timer m_Timer_for_One_Iteration;
            private Timer m_Timer_for_Delay;
            private bool m_f_is_Delay_Over;
            private bool m_f_is_Replace_over;

            public bool Is_Replace_Over { get { return m_f_is_Replace_over; } }
            public bool Is_Delay_Over { get { return m_f_is_Delay_Over; } }
            public S_Game(S_Main_Wnd a_Window)
            {
                m_Main_Window = a_Window;

                m_Main_Window.chkb_Check_speed_mode.Enabled = false;
                Graphics graph_comp = m_Main_Window.P_Game_board.CreateGraphics();
                graph_comp.Clear(m_Main_Window.P_Game_board.BackColor);
                S_Drawning.Set_graph_comp(graph_comp);
                m_Timer_for_One_Iteration = new Timer();
                m_Timer_for_One_Iteration.Tick += m_Timer_for_One_Iteration_Tick;
                m_Timer_for_One_Iteration.Interval = S_Constants.S_Constants.c_n_interval_of_game_timer;
                m_Timer_for_Delay = new Timer();
                m_Timer_for_Delay.Interval = S_Constants.S_Constants.c_n_iterval_of_delay;
                m_Timer_for_Delay.Tick += m_Timer_for_Delay_Tick;
               
                m_f_is_Delay_Over = m_f_is_Replace_over = true;

                Speed_Mode_Delegate SMD;
                if (m_Main_Window.chkb_Check_speed_mode.Checked)
                    SMD = Increase_Speed;
                else
                    SMD = Do_Nothing;
                Speed_Mode += SMD;


                m_random_for_food = new Random();
                // Пока что делаем так, начальные значения в константы не выносятся
                S_Location S1 = new S_Location(15, 15);
                S_Location S2 = new S_Location(16, 15);
                S_Location S3 = new S_Location(17, 15);
                S_Location S4 = new S_Location(18, 15);
                m_Snake = new S_Snake(E_Direction.Right, S4, S3, S2, S1);

                m_Board = new S_Game_Board(a_Window.P_Game_board, S_Drawning.Size_of_obj);
                m_Board.Set_Points_Value(false, new S_Location[] { S1, S2, S3, S4 });

                Add_new_Food();

                m_n_number_of_points = 0;
                Display_the_Score();
                m_Timer_for_One_Iteration.Start();
            }

            void m_Timer_for_Delay_Tick(object sender, EventArgs e)
            {
                    m_f_is_Delay_Over = true;
                    m_Timer_for_Delay.Stop();
                    
            }

            void m_Timer_for_One_Iteration_Tick(object sender, EventArgs e)
            {
                bool f_reusult_of_iteration;
                f_reusult_of_iteration = One_iteration();
                if (f_reusult_of_iteration == false)
                {
                    m_Timer_for_One_Iteration.Stop();
                    
                    MessageBox.Show("Вы проиграли!");
                   
                }
            }


            public void Stop_Game()
            {

                m_Timer_for_One_Iteration.Stop();
                m_Main_Window.chkb_Check_speed_mode.Enabled = true;
            }
            // Одна "итерация " в процессе игры
            public bool One_iteration()
            {
                m_f_is_Replace_over = false;
                bool a_f_result = true;
                // Проверяем не наткнулась ли змея на чего
                S_Location New_Heads_Location = m_Snake.Get_New_Heads_Coords();
                a_f_result = if_Normal_Moving(New_Heads_Location, m_Snake.Head.Direction);
                if (a_f_result == false)
                    return false;

                // Если все норм, то перераспределем координаты, ставим клеткам на которыъ голова и хвост соответствующее значения о блокировке, и перерисовываем
               
                Set_New_Coords(New_Heads_Location);
                m_Snake.Draw_Objects();
                m_Snake.Set_New_Values_to_Fields(m_Board);

                // Проверяем, съдена ли еда
                bool f_got_food = Is_Eating_Food();
                if (f_got_food) // Если да, то надо пристроить еще один блок к телу змеи, в хвост
                {
                    Add_new_Block();
                    Add_new_Food();
                    Speed_Mode();
                }
                m_f_is_Replace_over = true;

                return a_f_result;
            }
            
            // Задать новое направление движения голове змеи
            public void Set_New_Direction(E_Direction a_direction)
            {
                m_Snake.Set_New_Direction(a_direction);
                m_f_is_Delay_Over = false;
                m_Timer_for_Delay.Start();
            }
            private bool if_Normal_Moving(S_Location a_location, E_Direction a_direction)
            {
                bool f_result = true;

                // Сперва, проверяем не уткнулась ли она в стены
                switch (a_direction)
                {
                    case E_Direction.Left:
                        if (a_location.X < m_Board.Left_Bound)
                            f_result = false;
                        break;
                    case E_Direction.Right:
                        if (a_location.X > m_Board.Right_Bound)
                            f_result = false;
                        break;
                    case E_Direction.Up:
                        if (a_location.Y < m_Board.Up_bound)
                            f_result = false;
                        break;
                    case E_Direction.Down:
                        if (a_location.Y > m_Board.Low_bound)
                            f_result = false;
                        break;

                }
                if (!f_result)
                    return f_result;
                // Проверим не уткнулась ли она в себя или в свои отходы
                if (m_Board.Return_Point(a_location).Is_Free == false)
                    f_result = false;
                return f_result;
            }//Проверка, не наткнулась ли змея на препятствия

           

            // Переназначаем координаты           
            private void Set_New_Coords(S_Location a_location)
            {
                // Начинаем с хвоста
                m_Snake.Begin_To_Replace_Coords();
                // а теперь точно даем голове ее новые координаты
                m_Snake.Set_Heads_New_Coords(a_location);


            }

            // Добавление новой части змеи

            private void Add_new_Block()
            {
                S_Body_Block New_Body_Block = m_Snake.Tail;
                
                m_Snake.Tail = new S_Tail(m_Snake.Tail.Last_Position, m_Snake.Tail.Direction,New_Body_Block);
                m_Board.Set_Points_Value(false, m_Snake.Tail.Location);
            }

            // Добавление нового куска еды
            private void Add_new_Food()
            {
                // Кстати, при добавлении еды, поле на котором она находится, занятым не считается
                S_Location[] array_of_free_locations = m_Board.Get_All_Free_Locations_Fileds();
                int index = m_random_for_food.Next(array_of_free_locations.Length);
                S_Location New_Foods_Location = array_of_free_locations[index];
                m_Food = new S_Food(New_Foods_Location);
            }

            // Проверка, не наткнулась ли змея на еду
            private bool Is_Eating_Food()
            {
                bool f_result = false;
                if (m_Food.Location==m_Snake.Head.Location)
                {
                    m_n_number_of_points++;
                    Display_the_Score();
                    f_result = true;
                }
                return f_result;
            }

            // Написать сколько очков
            private void Display_the_Score()
            {
                m_Main_Window.lbl_Score_info.Text = S_Constants.S_Constants.с_str_message_to_lbl + Convert.ToString(m_n_number_of_points);
            }

            private void Do_Nothing()
            {

            }

            private void Increase_Speed()
            {
                if (m_n_number_of_points % S_Constants.S_Constants.c_n_count_of_eaten_pieces_when_its_time_to_burst == 0)
                {
                    m_Timer_for_One_Iteration.Interval = Convert.ToInt32(Convert.ToDouble(m_Timer_for_One_Iteration.Interval) * S_Constants.S_Constants.c_d_scale_of_burst);
                   m_Timer_for_Delay.Interval = Convert.ToInt32(Convert.ToDouble(m_Timer_for_Delay.Interval) * S_Constants.S_Constants.c_d_scale_of_burst);

                }
            }
        
        }
           
            


    }