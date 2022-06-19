using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using femap;

namespace FemapLib
{
    /// <summary>
    /// 一个表示三维坐标的类,实现功能如下：
    /// 1.实现了多种运算符重载。
    /// 2.自身对NXOpen.Point3d、NXOpen.Point2d、NXOpen.Point以及double[]之间的隐式转换。
    /// 3.带有自动圆整数值的功能
    /// 4.包含了其他三维坐标相关的静态功能函数
    /// </summary>
    public class FePoint3d
    {
        private double x;
        private double y;
        private double z;
        /// <summary>
        /// X坐标值，当autoRound开启时，会输出圆整后的值
        /// </summary>
        public double X
        {
            get
            {
                if (autoRound && IsDecimalsAvailable) { return Math.Round(x, decimals); }
                else
                { return x; }
            }
            set { x = value; }
        }
        /// <summary>
        /// Y坐标值，当autoRound开启时，会输出圆整后的值
        /// </summary>
        public double Y
        {
            get
            {
                if (autoRound && IsDecimalsAvailable) { return Math.Round(y, decimals); }
                else { return y; }
            }
            set { y = value; }
        }
        /// <summary>
        /// Z坐标值，当autoRound开启时，会输出圆整后的值
        /// </summary>
        public double Z
        {
            get
            {
                if (autoRound && IsDecimalsAvailable) { return Math.Round(z, decimals); }
                else { return z; }
            }
            set { z = value; }
        }
        /// <summary>
        /// 当自动圆整开启时，圆整的小数位数，范围在[0,15]之间（包含0和15）
        /// </summary>
        public int decimals;
        /// <summary>
        /// 是否开启自动圆整，如果开启，并且decimals设置合理，X、Y、Z将输出圆整后的坐标值。
        /// </summary>
        public bool autoRound;
        /// <summary>
        /// 判断当前设置的圆整小数位数是否有效
        /// </summary>
        public bool IsDecimalsAvailable
        {
            get { return decimals >= 0 && decimals <= 15; }
        }

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public FePoint3d()
        {
            decimals = -1;
            autoRound = false;
            x = 0; y = 0; z = 0;
        }
        /// <summary>
        /// 初始化 CNXPosition 类的一个新实例
        /// </summary>
        /// <param name="xI">X坐标</param>
        /// <param name="yI">Y坐标</param>
        /// <param name="zI">Z坐标，该值为可选参数，默认为0</param>
        public FePoint3d(double xI, double yI, double zI = 0) : this()
        {
            x = xI; y = yI; z = zI;
        }
        /// <summary>
        /// 初始化 CNXPosition 类的一个新实例
        /// </summary>
        /// <param name="xI">X坐标</param>
        /// <param name="yI">Y坐标</param>
        /// <param name="zI">Z坐标，该值为可选参数，默认为0</param>
        public FePoint3d(double[] values) : this()
        {
            if (values.Length >= 1)
                x = values[0];
            if (values.Length >= 2)
                y = values[1];
            if (values.Length >= 3)
                z = values[2];
        }

        #endregion

        #region 实例函数
        /// <summary>
        /// 拷贝值,生成新的CNXPosition实例及其引用
        /// </summary>
        /// <returns>返回CNXPosition实例及其引用</returns>
        public FePoint3d Copy()
        {
            return new FePoint3d(x, y, z);
        }
        #endregion

        #region 静态功能函数
        /// <summary>
        /// 圆整三维坐标值
        /// </summary>
        /// <param name="posI">允许CNXPosition即所有可以转换为CNXPosition的对象。</param>
        /// <param name="decimalsI">圆整的小数位数，范围[0,15]</param>
        /// <returns>返回圆整后的三维坐标值</returns>
        public static FePoint3d Round(FePoint3d posI, int decimalsI)
        {
            FePoint3d posRe = posI.Copy();
            if (decimalsI >= 0 && decimalsI <= 15)
            {
                posRe.X = Math.Round(posI.X, decimalsI);
                posRe.Y = Math.Round(posI.Y, decimalsI);
                posRe.Z = Math.Round(posI.Z, decimalsI);
            }
            return posRe;
        }
        /// <summary>
        /// 原点坐标位置（0，0，0）
        /// </summary>
        /// <returns>返回值为（0，0，0）的三维坐标</returns>
        public static FePoint3d Zero()
        {
            return new FePoint3d(0, 0, 0);
        }
        /// <summary>
        /// 获取两个三维坐标之间的距离
        /// </summary>
        /// <param name="pos1">第一个三维坐标</param>
        /// <param name="pos2">第二个三维坐标</param>
        /// <returns>返回求得的距离</returns>
        public static double GetDistance(FePoint3d pos1, FePoint3d pos2)
        {
            return Math.Sqrt(Math.Pow(pos2.y - pos1.y, 2) + Math.Pow(pos2.x - pos1.x, 2) + Math.Pow(pos2.z - pos1.z, 2));
        }
        /// <summary>
        /// 获取两点间的中间点坐标
        /// </summary>
        /// <param name="pos1">第一个三维坐标</param>
        /// <param name="pos2">第二个三维坐标</param>
        /// <returns>返回中间点的坐标</returns>
        public static FePoint3d GetCenterpoint3d(FePoint3d pos1, FePoint3d pos2)
        {
            FePoint3d point3d_temp = new FePoint3d();
            point3d_temp.x = (pos1.x + pos2.x) / 2;
            point3d_temp.y = (pos1.y + pos2.y) / 2;
            point3d_temp.z = (pos1.z + pos2.z) / 2;
            return point3d_temp;
        }
        ///// <summary>
        ///// 通过两两比较，获取一系列点中的最大距离
        ///// </summary>
        ///// <param name="poses">所有参与计算的点坐标</param>
        ///// <returns>返回最大距离</returns>
        //public static double GetMaxDistance(FePoint3D[] poses)
        //{
        //    List<double> distance = new List<double>();
        //    for (int i = 0; i < poses.Length; i++)
        //    {
        //        for (int j = i; j < poses.Length; j++)
        //        {
        //            double tempDis = GetDistance(poses[i], poses[j]);
        //            distance.Add(tempDis);
        //        }
        //    }
        //    if (distance.Count > 1)
        //    {
        //        double[] distanceArray = distance.ToArray();
        //        Common.QuickSort<double>(ref distanceArray, 0, distanceArray.Length - 1, true);
        //        return distanceArray[0];
        //    }
        //    else if (distance.Count == 1)
        //        return distance[0];
        //    else
        //        return -1;
        //}
        /// <summary>
        /// 三角形面积：海伦公式（要求三角形在X-Y平面）
        /// </summary>
        /// <param name="poses">三角形的三个点坐标</param>
        /// <returns>返回三角形的面积，如果为0，则表示三点无法构成三角形</returns>
        public double GetTriangleAreaByHeronsFormula(FePoint3d[] poses)
        {
            double x1 = poses[0].x,
                  y1 = poses[0].y,
                  x2 = poses[1].x,
                  y2 = poses[1].y,
                  x3 = poses[2].x,
                  y3 = poses[2].y;
            double a, b, c, p;
            double s;
            a = Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));
            b = Math.Sqrt(Math.Pow((x3 - x1), 2) + Math.Pow((y3 - y1), 2));
            c = Math.Sqrt(Math.Pow((x3 - x2), 2) + Math.Pow((y3 - y2), 2));
            p = (a + b + c) / 2;
            s = Math.Sqrt(p * (p - a) * (p - b) * (p - c));
            return s;
        }

        public static FePoint3d CreatePointWithOffset(FePoint3d baseP, FeVector3d vector, double distance)
        {
            FeVector3d unitV = FeVector3d.GetUnitVector(vector);
            FePoint3d posAbs = new FePoint3d(unitV.X * distance, unitV.Y * distance, unitV.Z * distance);
            return new FePoint3d(posAbs.X + baseP.X, posAbs.Y + baseP.Y, posAbs.Z + baseP.Z);
        }

        public static double GetAngle(FePoint3d cen, FePoint3d first, FePoint3d second)
        {
            const double M_PI = Math.PI;
            double ma_x = first.X - cen.X;
            double ma_y = first.Y - cen.Y;
            double mb_x = second.X - cen.X;
            double mb_y = second.Y - cen.Y;
            double v1 = (ma_x * mb_x) + (ma_y * mb_y);
            double ma_val = Math.Sqrt(ma_x * ma_x + ma_y * ma_y);
            double mb_val = Math.Sqrt(mb_x * mb_x + mb_y * mb_y);
            double cosM = v1 / (ma_val * mb_val);
            return Math.Acos(cosM) * 180 / M_PI;
        }
        #endregion

        #region 隐式转换和运算符重载
        /// <summary>
        /// 隐式转换Position->double[]
        /// </summary>
        /// <param name="pos">CNXPosition对象</param>
        /// <returns>返回转换后的double[]</returns>
        public static implicit operator double[](FePoint3d pos)
        {
            return new double[] { pos.x, pos.y, pos.z };
        }
        /// <summary>
        /// 隐式转换double[]->Position，double[]的大小没有限制，如果超过3，则只取前3个值。
        /// </summary>
        /// <param name="array">double数组</param>
        /// <returns>返回转换后的Position</returns>
        public static implicit operator FePoint3d(double[] array)
        {
            FePoint3d p = new FePoint3d(0, 0, 0);
            if (array != null)
            {
                if (array.Length > 0)
                {
                    p.x = array[0];
                }
                if (array.Length > 1)
                {
                    p.y = array[1];
                }
                if (array.Length > 2)
                {
                    p.z = array[2];
                }
            }
            return p;
        }
        /// <summary>
        /// 运算符重载“+”，两个Position坐标值相加
        /// </summary>
        /// <param name="lpos">“+”左侧的坐标值</param>
        /// <param name="rpos">“+”右侧的坐标值</param>
        /// <returns>返回X、Y、Z分别相加后得到的坐标值</returns>
        public static FePoint3d operator +(FePoint3d lpos, FePoint3d rpos)
        {
            return new FePoint3d(lpos.x + rpos.x, lpos.y + rpos.y, lpos.z + rpos.z);
        }
        /// <summary>
        /// 运算符重载“-”，左侧Pos减去右侧Pos
        /// </summary>
        /// <param name="lpos">“-”左侧的坐标值</param>
        /// <param name="rpos">“-”右侧的坐标值</param>
        /// <returns>返回X、Y、Z分别相减后得到的坐标值</returns>
        public static FePoint3d operator -(FePoint3d lpos, FePoint3d rpos)
        {
            return new FePoint3d(lpos.x - rpos.x, lpos.y - rpos.y, lpos.z - rpos.z);
        }
        /// <summary>
        /// 运算符重载，使用标量对一个位置进行多倍运算
        /// </summary>
        /// <param name="s">标量（double类型）</param>
        /// <param name="pos">原位置</param>
        /// <returns>返回多倍运算后的坐标值</returns>
        public static FePoint3d operator *(double s, FePoint3d pos)
        {
            return new FePoint3d(s * pos.x, s * pos.y, s * pos.z);
        }
        /// <summary>
        /// 运算符重载，使用标量对一个位置进行多倍运算
        /// </summary>
        /// <param name="s">标量（int类型）</param>
        /// <param name="pos">原位置</param>
        /// <returns>返回多倍运算后的坐标值</returns>
        public static FePoint3d operator *(int s, FePoint3d pos)
        {
            return new FePoint3d(s * pos.x, s * pos.y, s * pos.z);
        }
        /// <summary>
        /// 运算符重载，使用标量对一个位置坐标使用除法
        /// </summary>
        /// <param name="pos">原位置</param>
        /// <param name="s">标量</param>
        /// <returns>返回相除后的坐标值</returns>
        public static FePoint3d operator /(FePoint3d pos, double s)
        {
            return new FePoint3d(pos.x / s, pos.y / s, pos.z / s);
        }
        /// <summary>
        /// 运算符重载“==”，比较坐标值是否相等
        /// </summary>
        /// <param name="lpos">“==”左侧的坐标值</param>
        /// <param name="rpos">“==”右侧的坐标值</param>
        /// <returns>相等返回true，否则返回false</returns>
        public static bool operator ==(FePoint3d lpos, FePoint3d rpos)
        {
            return lpos.x == rpos.x && lpos.y == rpos.y && lpos.z == rpos.z;
        }
        /// <summary>
        /// 运算符重载“!=”，比较坐标值是否不相等
        /// </summary>
        /// <param name="lpos">“！=”左侧的坐标值</param>
        /// <param name="rpos">“！=”右侧的坐标值</param>
        /// <returns>不相等返回true，否则为false</returns>
        public static bool operator !=(FePoint3d lpos, FePoint3d rpos)
        {
            return lpos.x != rpos.x || lpos.y != rpos.y || lpos.z != rpos.z;
        }
        #endregion

        #region 覆写
        /// <summary>
        /// 覆写Equals(object obj)
        /// </summary>
        /// <param name="obj">和当前对象比较的对象</param>
        /// <returns>一致（类型和数值都一样）返回true，否则为false</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if ((obj.GetType().Equals(this.GetType())) == false)
            {
                return false;
            }
            FePoint3d temp = (FePoint3d)obj;
            return this == temp;
        }
        /// <summary>
        /// 覆写GetHashCode()
        /// </summary>
        /// <returns>返回当前对象的哈希值</returns>
        public override int GetHashCode()
        {
            return x.GetHashCode() + y.GetHashCode() + z.GetHashCode();
        }
        /// <summary>
        /// 覆写ToString()
        /// </summary>
        /// <returns>返回以“( X , Y , Z )"的格式文本输出坐标值</returns>
        public override string ToString()
        {
            return "( " + X + " , " + Y + " , " + Z + " )";
        }
        #endregion

    }
}
