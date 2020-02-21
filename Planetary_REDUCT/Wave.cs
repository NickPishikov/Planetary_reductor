﻿//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Planetary_REDUCT
//{
//    class Wave
//    { private double U, Dgp; //sigmacz, sigmac, bwf, bwc, l, bk, hob, mfc;
//        //U - передаточное отношение волнового редуктора
//        //Dgp - диаметр гибкого подшипника
//        private double[] az = new double[8];//Массив геметрических параметров зацепления колес F и С
//                                            //-----------------Разметка массива геометрических параметров-----------------------------------------
//                                            //az[0] - Xf - коэф-т смещения колеса F; az[1] - Xc - коэф-т смещения колеса С
//                                            //az[2] - df - диаметр делительной окружности колеса F, 
//                                            //az[3] - daf - диаметр окружности вершин колесаF,
//                                            //az[4] - dff - диаметр окружности впадин колеса F
//                                            //az[5], az[6], az[7] - dc,dac,dfc - аналогичные параметры для колеса С

//        private double[] ak = new double[7];// массив конструкционных параметров зацепления
//                                            //---------Разметка массива конструкционных параметров-------------------------------------
//                                            //ak[0] - sigmacz - толщина стенки гибкого стакана под зубьями; 
//                                            //ak[1] - sigmac - толщина стенки стакана колеса;
//                                            //ak[2] - bwf -ширина венца гибкого колеса ; 
//                                            //ak[3] - bwc - ширина венца жесткого колеса; 
//                                            //ak[4] - bk - ширина пояска для снижения перекоса зубьев; 
//                                            //ak[5] - l - длина стакана гибкого колеса;
//                                            //ak[6] - hob - толщина обода жесткого колеса под зубьями;
//        private int Zf, Zc, Ngp;
//        //числа зубьев колес F, C
//        //Ngp - номер (обозначение) гибкого подшипника

//        private double modulfc;//modulfc - модуль ступени FC, определяемый на этапе конструирования объекта
//        private double haz, cz;// параметры исходного производящего контура

//        public Wave(int PR, double Tout, double nout,double Dr,int No,int Nk,double Haz, double Cz)
//            // начало конструктора объекта класса Wave ("Волновой редуктор")
//            //--------------------------АРГУМЕНТЫ----------------------------
//            //PR - номер схемы механнизма для выбора формулы расчета передаточного (будет выбираться по картинке и кнопке)
//            //Tout - вращательный момент нагрузки
//            //nout - частота вращения выходного вала
//            //Dr - минимальный габаритный размер внутренней полости гибкого колеса
//            //No - начальный номер поиска в массиве модулей; Nk - конечный номер диапазона поиска модуля в массиве модулей
//        {
//            //-----------------ЛОКАЛЬНЫЕ ПЕРЕМЕННЫЕ КОНСТРУКТОРА--------------------------
//            double haz, cz;// исходный производящий контур
//            int IP;//индиктор для расчетов
//            double dfpp;//проектный диаметр гибкого подшипника. определяется в блоке конструирования внизу
//            double Dp;//проектный наружный диаметр гибкого подшипника. определяется в блоке конструирования внизу
//            //double dp;//диаметр подшипника 19.02.20 добавлено и закомментировано
//            double ngp;//переменная частоты вращения подшипника. Используется для проверки допустимости подшипника
//            double ngpp;//переменная предельной частоты вращения выбранного подшипника. Используется для проверки допустимости подшипника.

//            double sm;//временный модуль. потом перепишется в mfc
            

//            // SVOL2 - выбор гибкого подшипника по ГОСТ

//            void svol2()
//            {
//                //Вспомогательные переменные для svol2
//                int [,] a = new int[11,5]; // array[1..4, 1..11] of integer ;
//                //int j;
//                //float qq; //отладочное
//                          // Определяем массивы
//                //int[] n = new int[12] { 0, 806, 808, 809, 811, 812, 815, 818, 822, 824, 830, 836 }; Закомментировал нахуй 19.02.20
//                //array[1..11] of integer=(806,808,809,811,812,815,818,822,824,830,836);
//                // массив из констант 
//                // ВСЕ МАССИВЫ начинаются с 0!!!!!
                

//                a[0,0] = 806;
//                a[1,0] = 808;
//                a[2,0] = 809;
//                a[3,0] = 811;
//                a[4,0] = 812;
//                a[5,0] = 815;
//                a[6,0] = 818;
//                a[7,0] = 822;
//                a[8,0] = 824;
//                a[9,0] = 830;
//                a[10,0] = 836;

//                a[0,1] = 42;
//                a[1,1] = 52;
//                a[2,1] = 62;
//                a[3,1] = 72;
//                a[4,1] = 80;
//                a[5,1] = 100;
//                a[6,1] = 120;
//                a[7,1] = 150;
//                a[8,1] = 160;
//                a[9,1] = 200;
//                a[10,1] = 240;

//                a[0,2] = 30;
//                a[1,2] = 40;
//                a[2,2] = 45;
//                a[3,2] = 55;
//                a[4,2] = 60;
//                a[5,2] = 75;
//                a[6,2] = 90;
//                a[7,2] = 110;
//                a[8,2] = 120;
//                a[9,2] = 150;
//                a[10,2] = 180;

//                a[0,3] = 7;
//                a[1,3] = 8;
//                a[2,3] = 9;
//                a[3,3] = 11;
//                a[4,3] = 13;
//                a[5,3] = 15;
//                a[6,3] = 18;
//                a[7,3] = 24;
//                a[8,3] = 24;
//                a[9,3] = 30;
//                a[10,3] = 35;
            
//                a[0,4] = 4000;
//                a[1,2] = 4000;
//                a[2,4] = 3500;
//                a[3,4] = 3500;
//                a[4,4] = 3500;
//                a[5,4] = 3000;
//                a[6,4] = 3000;
//                a[7,4] = 2500;
//                a[8,4] = 2000;
//                a[9,4] = 1600;
//                a[10,4] = 1600;

//                for (int i = 0; i <= 10; i++)
//                {
//                    //qq = a[1,j];//отладочное
//                    if ((Dp <= a[i, 1]) && (nout <= a[i, 4]))
//                    {
//                        Dgp = a[i, 1];
//                        Ngp = a[i, 0];
//                        ngpp = a[i, 4];
//                        break;
//                    }

//                    //np = n[j];
//                    //dp = a[1,j];
//                    //d = a[2,j];
//                    //b = a[3,j];
//                    //wpd = a[4,j];

//                }
//                return;

//            }

//            // SVOL3 - расчет числа зубьев гибкого колеса

//            void svol3()
//            {
//               // double  n, kf; //внутренние переменные для svol3
//                //int m;
//                //zf = (Dgp / sm - 5.88 + 1.96 * (haz + cz));
//                Zf = (int)Math.Round((Dgp / sm - 5.88 + (1.96 * (haz + cz))),0);
//                Zc = Zf + 2;// добавлено 19.02.20 вместо комментариев

//                //-------------------Закомментировано 19.02.20---------------------
//                //kf = Math.Round(zf);
//                //n = kf / 2; // финт ушами для получения четного числа
//                // m = Math.Round(n * 2); Закомментировано 19.02.20
//                //    if (kf-m !=0)	kf--;
//                return;

//            }

//            // начало SVOL4 - u ip

//            void svol4()
//            {
//                if (PR <= 1)
//                {
//                    U = 0.5 * Zf;//вместо kf поставлено Zf 19.02.20
//                }
//                else U = 0.5 * Zf;
//                if (U <= 70)
//                {
//                    IP = 1;
//                }
//                else
//                {
//                    if ((300 - U) < 0)
//                    {
//                        IP = 3;
//                    }
//                    else IP = 2;
//                }
//                return;
//            }

//            // svol5 - инциал. az

//            void svol5()
//            {
//                double qw = 1.1; double qf = 0.4;
//                //массивы начинаются с нуля!
//                //kf,kc - везде заменено на Zf и Zc 19.02.20
//                az[0] = (3 + 0.01 * Zf);                          // Xf
//                az[1] = az[1] - 1 + qw * (1 + 0.00005 * qw * Zf); // Xc
//                az[2] = sm * Zf;                                  // df
//                az[3] = az[3] + 2 * (az[1] + qf) * sm;            //daf
//                az[4] = az[3] + 2 * (az[1] - haz - cz) * sm;      //dff
//                az[5] = sm * Zc;                                  //dc
//                az[6] = az[5] + 2 * (az[1] - haz) * sm;           //dac
//                az[7] = az[6] + 2 * (haz + cz + qf) * sm;         //dfc
//                return;
//            }

//            // svol6 - инициал ak

//            void svol6()
//            {
//                //!!!!!!!!массивы начинаются с нуля!
//                //инициализация конструкционных параметров волнового редуктора
//                ak[0] = sm * (0.5 * Zf + az[1] - (haz + cz)) - 0.5 * Dgp;//
//                ak[1] = 0.7 * ak[1];
//                ak[2] = 0.2 * az[3];
//                ak[3] = ak[3] + 3;
//                ak[4] = 0.06 * az[3];
//                ak[5] = 1.0 * az[3];
//                ak[6] = 0.18 * az[6];
//                return;
//            }

           

//            //-----------------------ЭТАП КОНСТРУИРОВАНИЯ ОБЪЕКТА КЛАССА "Wave"-----------------------------------------
//            //
//            //----------------------------------------------------------------------------------------------------------
//            haz = Haz;
//            cz = Cz;

//            double[] am = new double[6];//объявление массива модулей для последующих прогонов
//                am[0] = 0.3;
//                am[1] = 0.4;
//                am[2] = 0.5;
//                am[3] = 0.6;
//                am[4] = 0.8;
//                am[5] = 1.0;//инициализация массива модулей
//            double dfp=15.5*(Math.Pow(Tout,1/3));//локальная переменная диаметра подшипника из прочностных соображений
//            double dfr = 1.1 * Dr;//локальная переменная диаметра подшипника из соображений габаритов
//            if (dfp >= dfr) dfpp = dfp; else dfpp = dfr;//определение проектного значения диаметра гибкого подшипника
//            //double sm;
//           // ngpp = 0;

//            for(int n = No; n < Nk; n++)
//            {
//                //double sm;
//                sm = am[n];
//                Dp = dfpp + 0.99 * sm * (6 - 2*(haz + cz));
//                if (Dp <= 240)
//                {
//                    svol2();
//                    svol3();
//                    svol4();
//                    if (IP == 1)
//                    {
//                        Dp = Dp + 5;
//                    }
//                    if (IP == 2)
//                    {
//                        ngp = nout * U;
//                        if ((ngpp - ngp) >= 0)
//                        {
//                            svol5();
//                            svol6();
//                            modulfc = sm;
//                            break;

//                        }
                        
//                    }
//                    if (IP == 3)
//                    {
//                        svol5();
//                        svol6();
//                        modulfc = 0;//зануление модуля - индикатор того, что надо выводить сообщение о невозможности синтеза механизма
//                        break;
                        
//                    }
//                }
//            }



//        }//конец конструктора

//    }//конец описания класса
//}//конец описания пространства имен
