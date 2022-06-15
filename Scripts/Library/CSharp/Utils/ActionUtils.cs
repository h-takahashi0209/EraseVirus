using System;


namespace TakahashiH
{
    /// <summary>
    /// Action 関連 Utils
    /// </summary>
    public static class ActionUtils
    {
        //====================================
        //! 関数（public static）
        //====================================

        /// <summary>
        /// 一度だけ呼び出す
        /// </summary>
        /// <param name="rAction"> 呼び出す Action </param>
        public static void CallOnce(ref Action rAction)
        {
            if (rAction == null) {
                return;
            }

            var tempAction = rAction;
            rAction = null;
            tempAction();
        }

        /// <summary>
        /// 一度だけ呼び出す
        /// </summary>
        /// <param name="rAction">    呼び出す Action    </param>
        /// <param name="arg">        引数               </param>
        public static void CallOnce<T>(ref Action<T> rAction, T arg)
        {
            if (rAction == null) {
                return;
            }

            var tempAction = rAction;
            rAction = null;
            tempAction(arg);
        }

        /// <summary>
        /// 一度だけ呼び出す
        /// </summary>
        /// <param name="rAction">    呼び出す Action    </param>
        /// <param name="arg1">       引数1              </param>
        /// <param name="arg2">       引数2              </param>
        public static void CallOnce<T1, T2>(ref Action<T1, T2> rAction, T1 arg1, T2 arg2)
        {
            if (rAction == null) {
                return;
            }

            var tempAction = rAction;
            rAction = null;
            tempAction(arg1, arg2);
        }

        /// <summary>
        /// 一度だけ呼び出す
        /// </summary>
        /// <param name="rAction">    呼び出す Action    </param>
        /// <param name="arg1">       引数1              </param>
        /// <param name="arg2">       引数2              </param>
        /// <param name="arg3">       引数3              </param>
        public static void CallOnce<T1, T2, T3>(ref Action<T1, T2, T3> rAction, T1 arg1, T2 arg2, T3 arg3)
        {
            if (rAction == null) {
                return;
            }

            var tempAction = rAction;
            rAction = null;
            tempAction(arg1, arg2, arg3);
        }
    }
}
