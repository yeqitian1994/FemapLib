using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using femap;
using System.Runtime.InteropServices;

namespace FemapLib.Tutorials
{
    public class Tutorial_EntityOperation
    {
        //关于entity
        //在femap api当中，所有的Entity都继承自5.1 Common Entity，每个不同类型的Entity再额外附带的属性和函数。
        //例如5.63 Point对象，包含了结点特定的x、y、z坐标值。
        //虽然femap当中有类似Point这种对象，但所有对entity的操作仍然是基于ID进行的，我们会传入int类型的ID，而不是Point对象。

        //关于entity的指针对象
        //femap给每种类型的entity提供了一个指针对象，比如model.fePoint，它会返回一个Point对象而不是所有Point
        //当然你得到的Point对象它是空的，你需要使用First()让它指向数据库中的第一个Point，再用Next()遍历下去，直到返回的不再是FE_OK，或是ID号大于等于2147483647。
        //所有通用的entity操作请看 API Reference 中 5.1.2  Common Entity Methods 一节
        //不要试图存下这些Point对象，因为当你使用Next()之后，这个对象就发生改变了，包括你之前存下的Point，最后只能得到一堆一样的对象。
        //所以不要对具体的class太过执着，试图传一个List<Point>进出，也不推荐频繁全局遍历，会非常影响程序的效率
        //用List<int>就好，femap的函数也不会要求你把Point作为输入参数。

        /// <summary>
        /// 创建新的点
        /// </summary>
        public static void CreateAndSavePoint()
        {
            //获取femap model对象
            model feModel = Marshal.GetActiveObject("femap.model") as model;
            if(feModel!=null)
            {            
                //创建一个点对象
                Point point = feModel.fePoint;
                //获得下一个可用的ID
                int nextEmptyID = point.NextEmptyID();
                //设置点的坐标值
                point.x = 1;
                point.y = 1;
                point.z = 1;
                //保存点坐标
                //【注意】所有代码中的对象都是临时对象，如果你创建了一个对象并且想对它编辑，切记不要过早put保存
                //，否则put之后修改的数据不会同步到femap的数据库。（除非你再put一次）
                point.Put(nextEmptyID);
            }
        }

        /// <summary>
        /// 通过ID来获取并编辑点坐标
        /// </summary>
        public static void GetAndEditPointByID()
        {
            //如果你已经知道了某个对象的ID号以及类型，你可以通过以下方式获取，此处以Point举例
            //目标是获取id号为1的点
            int id = 1;
            model feModel = Marshal.GetActiveObject("femap.model") as model;
            if (feModel != null)
            {
                //此处调用上述创建点的函数，保证model中有点
                CreateAndSavePoint();
                //创建一个点对象
                Point point = feModel.fePoint;
                //输入id，通过Get函数获得
                //如果返回的ReturnCode是FE_OK，则说明成功了，此时这个point就是ID为1的point
                zReturnCode rc= point.Get(id);
                if(rc==zReturnCode.FE_OK)
                {
                    //修改点的坐标
                    point.x = 2;
                    //保存点
                    //此处输入点自身的ID，表示覆盖。
                    point.Put(point.ID);
                }
                else
                {
                    //提示错误
                    feModel.feAppMessage(zMessageColor.FCM_NORMAL, "未能获取到id为：" + id + "的Point对象。");
                }
            }
        }
    }
}
