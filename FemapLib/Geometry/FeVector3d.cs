using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using femap;

namespace FemapLib
{
    /// <summary>
    /// 基于Vector的功能集合
    /// </summary>
    public class FeVector3d
    {
        private double x;
        private double y;
        private double z;
        /// <summary>
        /// X值，当autoRound开启时，会输出圆整后的值
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
        /// Y值，当autoRound开启时，会输出圆整后的值
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
        /// Z值，当autoRound开启时，会输出圆整后的值
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
        public FeVector3d()
        {
            decimals = -1;
            autoRound = false;
            x = 0; y = 0; z = 0;
        }
        /// <summary>
        /// 初始化 CNXVector 类的一个新实例
        /// </summary>
        /// <param name="xI">X标量</param>
        /// <param name="yI">Y标量</param>
        /// <param name="zI">Z标量，该值为可选参数，默认为0</param>
        public FeVector3d(double xI, double yI, double zI = 0) : this()
        {
            x = xI; y = yI; z = zI;
        }
        #endregion

        #region 实例函数
        /// <summary>
        /// 获取向量从原点开始指向的位置
        /// </summary>
        /// <returns>返回指向的位置</returns>
        public FePoint3d ToPosition()
        {
            return new FePoint3d(x, y, z);
        }
        /// <summary>
        /// 获取该向量的单位向量
        /// </summary>
        /// <returns>返回单位向量</returns>
        public FeVector3d ToUnit()
        {
            return GetUnitVector(this);
        }
        #endregion

        #region 静态功能函数
        /// <summary>
        /// 获取单位向量
        /// </summary>
        /// <param name="startP">起始点</param>
        /// <param name="endP">终点</param>
        /// <returns>返回单位向量</returns>
        public static FeVector3d GetUnitVector(FePoint3d startP, FePoint3d endP)
        {
            FeVector3d vector = new FeVector3d(endP.X - startP.X, endP.Y - startP.Y, endP.Z - startP.Z);
            if (vector.X == 0 && vector.Y == 0 && vector.Z == 0)
                return vector;
            else
                return GetUnitVector(vector);
        }
        /// <summary>
        /// 获取单位向量
        /// </summary>
        /// <param name="vector">向量</param>
        /// <returns>返回单位向量</returns>
        public static FeVector3d GetUnitVector(FeVector3d vector)
        {
            double m = Math.Sqrt(Math.Pow(vector.X, 2) + Math.Pow(vector.Y, 2) + Math.Pow(vector.Z, 2));
            return new FeVector3d(vector.X / m, vector.Y / m, vector.Z / m);
        }
        /// <summary>
        /// 点乘
        /// </summary>
        /// <param name="v1">向量1</param>
        /// <param name="v2">向量2</param>
        /// <returns>返回点乘结果</returns>
        public static double DotProduct(FeVector3d v1, FeVector3d v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
        }
        /// <summary>
        /// 向量是否相互垂直
        /// </summary>
        /// <param name="v1">向量1</param>
        /// <param name="v2">向量2</param>
        /// <returns>垂直返回true，否则为false</returns>
        public static bool isVectorsPerpendicular(FeVector3d v1, FeVector3d v2)
        {
            return DotProduct(v1, v2) == 0;
        }
        /// <summary>
        /// 获取两个向量的夹角
        /// </summary>
        /// <param name="v1">向量1</param>
        /// <param name="v2">向量2</param>
        /// <returns>返回夹角</returns>
        public static double GetAngle(FeVector3d v1, FeVector3d v2)
        {
            v1 = GetUnitVector(v1);
            v2 = GetUnitVector(v2);
            return FePoint3d.GetAngle(new FePoint3d(), v1.ToPosition(), v2.ToPosition());
        }
        #endregion

        #region 隐式转换和运算符重载
        /// <summary>
        /// 隐式转换CNXVector->double[]
        /// </summary>
        /// <param name="vec">CNXVector对象</param>
        /// <returns>返回转换后的double[]</returns>
        public static implicit operator double[](FeVector3d vec)
        {
            return new double[] { vec.x, vec.y, vec.z };
        }
        /// <summary>
        /// 隐式转换double[]->CNXVector，double[]的大小没有限制，如果超过3，则只取前3个值。
        /// </summary>
        /// <param name="array">double数组</param>
        /// <returns>返回转换后的CNXVector</returns>
        public static implicit operator FeVector3d(double[] array)
        {
            FeVector3d p = new FeVector3d(0, 0, 0);
            if (array != null)
            {
                if (array.Length > 0)
                {
                    p.X = array[0];
                }
                if (array.Length > 1)
                {
                    p.Y = array[1];
                }
                if (array.Length > 2)
                {
                    p.Z = array[2];
                }
            }
            return p;
        }
        /// <summary>
        /// 运算符重载，使用标量对一个向量进行多倍运算
        /// </summary>
        /// <param name="s">标量（double类型）</param>
        /// <param name="vec">原向量</param>
        /// <returns>返回多倍运算后的向量</returns>
        public static FeVector3d operator *(double s, FeVector3d vec)
        {
            return new FeVector3d(s * vec.x, s * vec.y, s * vec.z);
        }
        /// <summary>
        /// 运算符重载，使用标量对一个向量进行多倍运算
        /// </summary>
        /// <param name="s">标量（int类型）</param>
        /// <param name="vec">原向量</param>
        /// <returns>返回多倍运算后的向量</returns>
        public static FeVector3d operator *(int s, FeVector3d vec)
        {
            return new FeVector3d(s * vec.x, s * vec.y, s * vec.z);
        }
        /// <summary>
        /// 运算符重载，使用标量对一个向量使用除法
        /// </summary>
        /// <param name="vec">原向量</param>
        /// <param name="s">标量</param>
        /// <returns>返回相除后的向量</returns>
        public static FeVector3d operator /(FeVector3d vec, double s)
        {
            return new FeVector3d(vec.x / s, vec.y / s, vec.z / s);
        }
        /// <summary>
        /// 运算符重载“==”，比较向量值是否相等
        /// </summary>
        /// <param name="lvec">“==”左侧的坐向量</param>
        /// <param name="rvec">“==”右侧的向量</param>
        /// <returns>相等返回true，否则返回false</returns>
        public static bool operator ==(FeVector3d lvec, FeVector3d rvec)
        {
            return lvec.x == rvec.x && lvec.y == rvec.y && lvec.z == rvec.z;
        }
        /// <summary>
        /// 运算符重载“!=”，比较向量是否不相等
        /// </summary>
        /// <param name="lvec">“！=”左侧的向量</param>
        /// <param name="rvec">“！=”右侧的向量</param>
        /// <returns>不相等返回true，否则返回false</returns>
        public static bool operator !=(FeVector3d lvec, FeVector3d rvec)
        {
            return lvec.x != rvec.x || lvec.y != rvec.y || lvec.z != rvec.z;
        }
        #endregion





    }
}
