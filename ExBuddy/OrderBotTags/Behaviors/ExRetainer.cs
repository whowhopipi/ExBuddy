namespace ExBuddy.OrderBotTags.Behaviors
{
    using System.Threading.Tasks;
    using Clio.XmlEngine;
    using Buddy.Coroutines;

    using ff14bot.Managers;
    using ff14bot.RemoteWindows;
    using ff14bot;
    using ff14bot.Objects;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.Linq;
    [XmlElement("Retainer")]
    public class ExRetainer : ExProfileBehavior
    {
        [DefaultValue(3.0f)]
        [XmlAttribute("Radius")]
        public float Radius { get; set; }

        private List<string> Ventures = new List<string>()
        {
            "[探险归来]",
            "[Tâche terminée]",
            "(Venture complete)"
        };

        private GameObject bell;

        protected override void OnStart()
        {
            uint[] RetainerIds = new uint[] { 2000401 , 2000441 };
            foreach (var oneNpc in GameObjectManager.GetObjectsByNPCIds<EventObject>(RetainerIds))
            {
                Log("{0}", oneNpc.ObjectId);
                if (oneNpc.Distance(Core.Me.Location) < Radius)
                {
                    bell = oneNpc;
                    break;
                }
            }

            if(bell == null)
            {
                foreach (var oneNpc in GameObjectManager.GetObjectsByNPCId<HousingEventObject>(2000401))
                {
                    if (oneNpc.Distance(Core.Me.Location) < 3)
                    {
                        bell = oneNpc;
                        break;
                    }
                }
            }
        }

        protected override async Task<bool> Main()
        {
            if (bell == null)
            {
                Log("该区域没有传唤铃或者传唤铃距当前位置过远");
                isDone = true;
                return true;
            }

            bell.Interact();
            if (await Coroutine.Wait(5000, () => SelectString.IsOpen))
            {
                uint count = 0;
                int lineC = SelectString.LineCount;
                uint countLine = (uint)lineC;
                foreach (var retainer in SelectString.Lines())
                {
                    if (retainer.ToString().EndsWith("]") || retainer.ToString().EndsWith(")"))
                    {
                        Log("检查雇员第{0}个雇员" ,(count + 1));
                        string currentRetainer = retainer.ToString();
                        string one = Ventures.FirstOrDefault(v => currentRetainer.Contains(v));
                        if (one != null)
                        {
                            Log("探险成功 !");
                            SelectString.ClickSlot(count);
                            if (await Coroutine.Wait(5000, () => Talk.DialogOpen))
                            {
                                Talk.Next();
                                if (await Coroutine.Wait(5000, () => SelectString.IsOpen))
                                {
                                    // 雇员界面打开，再次判断探险是否完成
                                    //SelectString.Lines().;

                                    var ret = SelectString.ClickLineContains("结束") || SelectString.ClickLineContains("Complete");
                                    if (await Coroutine.Wait(3000, () => RetainerTaskResult.IsOpen))
                                    {
                                        RetainerTaskResult.Reassign();
                                        if (await Coroutine.Wait(5000, () => RetainerTaskAsk.IsOpen))
                                        {
                                            RetainerTaskAsk.Confirm();
                                            if (await Coroutine.Wait(5000, () => Talk.DialogOpen))
                                            {
                                                Talk.Next();
                                                if (await Coroutine.Wait(54000, () => SelectString.IsOpen))
                                                {
                                                    SelectString.ClickSlot(9);
                                                    if (await Coroutine.Wait(5000, () => Talk.DialogOpen))
                                                    {
                                                        Talk.Next();
                                                        await Coroutine.Sleep(3000);
                                                        bell.Interact();
                                                        if (await Coroutine.Wait(5000, () => SelectString.IsOpen))
                                                        {
                                                            count++;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    } else
                                    {
                                        SelectString.ClickSlot(9);
                                        if (await Coroutine.Wait(5000, () => Talk.DialogOpen))
                                        {
                                            Talk.Next();
                                            await Coroutine.Sleep(3000);
                                            bell.Interact();
                                            if (await Coroutine.Wait(5000, () => SelectString.IsOpen))
                                            {
                                                count++;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            Log("探险没有结束 !");
                            count++;
                        }
                    }
                    else
                    {
                        Log("没有更多雇员");
                        SelectString.ClickSlot(countLine - 1);
                    }
                }
                return isDone = true;
            }
            return isDone = true;
        }
    }
}
