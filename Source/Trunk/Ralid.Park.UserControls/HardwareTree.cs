using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Report;
using Ralid.Park.BusinessModel.Resouce;
using Ralid.GeneralLibrary.LED;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.GeneralLibrary.ExceptionHandling;

namespace Ralid.Park.UserControls
{
    public partial class HardwareTree : TreeView, Ralid.Park.BusinessModel.Interface.IReportHandler
    {
        #region 构造函数
        public HardwareTree()
        {
            InitializeComponent();
        }

        public HardwareTree(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
        }
        #endregion

        #region 私有变量
        List<TreeNode> allEntranceNodes = new List<TreeNode>();
        List<TreeNode> allParkNodes = new List<TreeNode>();
        List<TreeNode> allVideoNodes = new List<TreeNode>();
        List<TreeNode> allDivisions = new List<TreeNode>();
        TreeNode root;

        bool _showEntrance = true;
        #endregion

        #region 私有方法
        private void Node_Checked(object sender, TreeViewEventArgs e)
        {
            this.AfterCheck -= Node_Checked;
            CheckChildren(e.Node);
            CheckParent(e.Node);
            this.AfterCheck += Node_Checked;
        }

        private void CheckChildren(TreeNode curNode)
        {
            foreach (TreeNode nod in curNode.Nodes)
            {
                nod.Checked = curNode.Checked;
                CheckChildren(nod);
            }
        }

        private void CheckParent(TreeNode curNode)
        {
            TreeNode parent = curNode.Parent;
            if (parent != null)
            {
                bool allChecked = true;
                foreach (TreeNode n in parent.Nodes)
                {
                    if (n.Checked == false)
                    {
                        allChecked = false;
                        break;
                    }
                }
                parent.Checked = allChecked;
                CheckParent(parent);
            }
        }
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取根节点
        /// </summary>
        public TreeNode RootNode
        {
            get
            {
                return root;
            }
        }

        /// <summary>
        /// 获取所有的停车场节点
        /// </summary>
        public List<TreeNode> ParkNodes
        {
            get
            {
                return allParkNodes;
            }
        }

        /// <summary>
        /// 获取所有的通道节点
        /// </summary>
        public List<TreeNode> EntranceNodes
        {
            get
            {
                return allEntranceNodes;
            }
        }

        /// <summary>
        /// 获取所有的社频节点
        /// </summary>
        public List<TreeNode> VideoSourceNodes
        {
            get
            {
                return allVideoNodes;
            }
        }


        /// <summary>
        /// 获取或设置是否在树中显示视频节点
        /// </summary>
        public bool ShowVideoSource { get; set; }

        /// <summary>
        /// 获取或设置是否在数中显示控制器节点
        /// </summary>
        public bool ShowEntrance
        {
            get { return _showEntrance; }
            set { _showEntrance = value; }
        }

        /// <summary>
        /// 获取勾选的通道控制器ID字串
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<int> SelectedEntranceIDs
        {
            get
            {
                List<int> entrance = new List<int>();
                foreach (TreeNode node in allEntranceNodes)
                {
                    if (node.Checked)
                    {
                        EntranceInfo info = node.Tag as EntranceInfo;
                        entrance.Add(info.EntranceID);
                    }
                }
                return entrance;
            }
            set
            {
                if (value != null)
                {
                    foreach (TreeNode node in allEntranceNodes)
                    {
                        EntranceInfo info = node.Tag as EntranceInfo;
                        if (value.Any(e => e == info.EntranceID))
                        {
                            node.Checked = true;
                        }
                    }
                }
            }
        }
        #endregion

        #region 公共方法

        /// <summary>
        /// 获取通道节点
        /// </summary>
        /// <param name="entranceID"></param>
        /// <returns></returns>
        public TreeNode GetEntranceNode(int entranceID)
        {
            foreach (TreeNode node in allEntranceNodes)
            {
                EntranceInfo entrance = node.Tag as EntranceInfo;
                if (entrance != null && entrance.EntranceID == entranceID)
                {
                    return node;
                }
            }
            return null;
        }

        /// <summary>
        /// 获取停车场节点
        /// </summary>
        /// <param name="parkID"></param>
        /// <returns></returns>
        public TreeNode GetParkNode(int parkID)
        {
            foreach (TreeNode node in allParkNodes)
            {
                ParkInfo park = node.Tag as ParkInfo;
                if (park != null && park.ParkID == parkID)
                {
                    return node;
                }
            }
            return null;
        }

        /// <summary>
        /// 获取视频节点
        /// </summary>
        /// <param name="videoID"></param>
        /// <returns></returns>
        public TreeNode GetVideoNode(int videoID)
        {
            foreach (TreeNode node in allVideoNodes)
            {
                VideoSourceInfo video = node.Tag as VideoSourceInfo;
                if (video != null && video.VideoID == videoID)
                {
                    return node;
                }
            }
            return null;
        }


        /// <summary>
        /// 初始化树
        /// </summary>
        public void Init()
        {
            Init(0);
        }

        /// <summary>
        /// 初始化树
        /// </summary>
        /// <param name="parkID">停车场ID，为0时显示所有停车场</param>
        public void Init(int parkID)
        {
            this.Nodes.Clear();
            this.allParkNodes.Clear();
            this.allVideoNodes.Clear();
            this.allEntranceNodes.Clear();
            this.allDivisions.Clear();

            this.ImageList = images;
            ParkBll parkbll = new ParkBll(AppSettings.CurrentSetting.ParkConnect);

            root = new TreeNode(Resources.Resource1.HardwareTree_Root);
            this.Nodes.Add(root);

            if (ParkBuffer.Current != null)
            {
                foreach (ParkInfo park in ParkBuffer.Current.Parks)
                {
                    if (park.ParentID == null)
                    {
                        if (parkID == 0 || parkID == park.ParkID)
                        {
                            AddParkNode(root, park);
                        }
                    }
                }
            }
            this.ExpandAll();
            this.AfterCheck += Node_Checked;
        }

        /// <summary>
        /// 添加一个Park节点
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="park"></param>
        /// <returns></returns>
        public TreeNode AddParkNode(TreeNode parent, ParkInfo park)
        {
            TreeNode node = new TreeNode();
            parent.Nodes.Add(node);
            allParkNodes.Add(node);
            RenderPark(node, park);
            foreach (ParkInfo d in park.SubParks)
            {
                AddParkNode(node, d);
            }
            if (ShowEntrance)
            {
                foreach (EntranceInfo entrance in park.Entrances)
                {
                    AddEntranceNode(node, entrance);
                }
            }
            return node;
        }
        /// <summary>
        /// 增加一个通道控制器节点
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="entrance"></param>
        /// <returns></returns>
        public TreeNode AddEntranceNode(TreeNode parent, EntranceInfo entrance)
        {
            TreeNode node = new TreeNode();
            parent.Nodes.Add(node);
            allEntranceNodes.Add(node);
            RenderEntrance(node, entrance);
            if (ShowVideoSource)
            {
                foreach (VideoSourceInfo video in entrance.VideoSources)
                {
                    AddVideoSourceNode(node, video);
                }
            }
            return node;
        }

        /// <summary>
        /// 增加一个视频节点
        /// </summary>
        /// <param name="entrance"></param>
        /// <param name="video"></param>
        /// <returns></returns>
        public TreeNode AddVideoSourceNode(TreeNode entrance, VideoSourceInfo video)
        {
            TreeNode node = new TreeNode();
            entrance.Nodes.Add(node);
            allVideoNodes.Add(node);
            RenderVideoSource(node, video);
            return node;
        }

        /// <summary>
        /// 显示PARK节点
        /// </summary>
        /// <param name="node"></param>
        /// <param name="park"></param>
        public void RenderPark(TreeNode node, ParkInfo park)
        {
            if (park.IsRootPark)
            {
                node.Text = string.Format("{0}[{1}/{2}][{3}]", park.ParkName, park.Vacant, park.TotalPosition, ParkWorkModeDescription.GetDescription(park.WorkMode));
            }
            else
            {
                node.Text = string.Format("{0}[{1}/{2}]", park.ParkName, park.Vacant, park.TotalPosition);
            }
            node.Tag = park;
            node.ForeColor = (park.Status == EntranceStatus.Ok) ? Color.Black : Color.Red;
            node.ImageIndex = (park.Status == EntranceStatus.Ok) ? 1 : 4;
            node.SelectedImageIndex = (park.Status == EntranceStatus.Ok) ? 1 : 4;
        }
        /// <summary>
        /// 显示通道控制器节点
        /// </summary>
        /// <param name="node"></param>
        /// <param name="en"></param>
        public void RenderEntrance(TreeNode node, EntranceInfo en)
        {
            string toolTip = string.Empty;
            node.Tag = en;
            node.Text = en.IsExitDevice ? string.Format("{0}[{1}]", en.EntranceName, en.IPAddress) :
                string.Format("{0}[{1}][{2}]", en.EntranceName, en.IPAddress, en.TempCard);
            switch (en.Status)
            {
                case EntranceStatus.Ok:
                case EntranceStatus.GateDown:
                case EntranceStatus.GateUp:
                case EntranceStatus.LessCard:
                    node.ForeColor = System.Drawing.Color.Black;
                    node.ImageIndex = en.IsMaster ? 6 : 2;
                    node.SelectedImageIndex = en.IsMaster ? 6 : 2;
                    if (!en.IsExitDevice)
                    {
                        if (UserSetting.Current.MinTempCard >= 0 && UserSetting.Current.MinTempCard >= en.TempCard)
                        {
                            if (AppSettings.CurrentSetting.EnableTTS) Ralid.GeneralLibrary.Speech.TTSSpeech.Instance.Speek(string.Format("{0} {1}", en.EntranceName, Resources.Resource1.HardwareTree_TempCardSpeech));
                            toolTip = Resources.Resource1.HardwareTree_TempCardAlarm;
                            node.ForeColor = System.Drawing.Color.Red;
                            node.ImageIndex = 5;
                            node.SelectedImageIndex = 5;
                        }
                    }
                    break;
                case EntranceStatus.OffLine:
                    node.ForeColor = System.Drawing.Color.Red;
                    node.SelectedImageIndex = 4;
                    node.ImageIndex = 4;
                    break;
                case EntranceStatus.NoCard:
                case EntranceStatus.CardJam:
                case EntranceStatus.StorageAlarm:
                case EntranceStatus.StorageFull:
                    toolTip = EntranceStatusDescription.GetDescription(en.Status);
                    node.Text = string.Format("{0}[{1}][{2}]", en.EntranceName, toolTip);
                    node.ForeColor = System.Drawing.Color.Red;
                    node.ImageIndex = 5;
                    node.SelectedImageIndex = 5;
                    break;
                default:
                    node.ForeColor = System.Drawing.Color.Red;
                    node.SelectedImageIndex = 4;
                    node.ImageIndex = 4;
                    break;
            }
            node.ToolTipText = toolTip;
        }

        /// <summary>
        /// 显示视频节点
        /// </summary>
        /// <param name="node"></param>
        /// <param name="info"></param>
        public void RenderVideoSource(TreeNode node, VideoSourceInfo info)
        {
            node.Tag = info;
            node.Text = info.VideoName;
            node.SelectedImageIndex = 3;
            node.ImageIndex = 3;
        }

        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="node"></param>
        public void DeleteNode(TreeNode node)
        {
            if (node.Parent != null)
            {
                node.Parent.Nodes.Remove(node);
            }
            else
            {
                node.TreeView.Nodes.Remove(node);
            }
        }

        /// <summary>
        /// 节点是否是停车场节点
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public bool IsParkNode(TreeNode node)
        {
            return (node.Tag != null && node.Tag is ParkInfo);
        }
        /// <summary>
        /// 节点是否是通道控制器节点
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public bool IsEntranceNode(TreeNode node)
        {
            return (node.Tag != null && node.Tag is EntranceInfo);
        }

        /// <summary>
        /// 节点是否是视频节点
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public bool IsVideoSourceNode(TreeNode node)
        {
            return (node.Tag != null && node.Tag is VideoSourceInfo);
        }

        /// <summary>
        /// 在树中勾选通道控制器节点
        /// </summary>
        /// <param name="entrances"></param>
        public void SelectEntrances(List<EntranceInfo> entrances)
        {
            foreach (TreeNode node in allEntranceNodes)
            {
                EntranceInfo info = node.Tag as EntranceInfo;
                if (entrances.Exists(e => e.EntranceID == info.EntranceID))
                {
                    node.Checked = true;
                }
            }
        }

        /// <summary>
        /// 获取树中所有选中的通道
        /// </summary>
        /// <returns></returns>
        public List<EntranceInfo> GetSelectedEntrances()
        {
            List<EntranceInfo> entrance = new List<EntranceInfo>();
            foreach (TreeNode node in allEntranceNodes)
            {
                if (node.Checked)
                {
                    EntranceInfo info = node.Tag as EntranceInfo;
                    entrance.Add(info);
                }
            }
            return entrance;
        }

        /// <summary>
        /// 获取某停车场下所有没用选中的通道
        /// </summary>
        /// <param name="parkID"></param>
        /// <returns></returns>
        public List<EntranceInfo> GetUnSeclectedEntrances(int parkID)
        {
            List<EntranceInfo> entrance = new List<EntranceInfo>();
            foreach (TreeNode node in allEntranceNodes)
            {
                EntranceInfo info = node.Tag as EntranceInfo;
                if (info.ParkID == parkID && !node.Checked)
                {
                    entrance.Add(info);
                }
            }
            return entrance;
        }

        /// <summary>
        /// 获取树中所有选中的停车场
        /// </summary>
        /// <returns></returns>
        public List<ParkInfo> GetSelectedParks()
        {
            List<ParkInfo> park = new List<ParkInfo>();
            foreach (TreeNode node in allParkNodes)
            {
                if (node.Checked)
                {
                    ParkInfo info = node.Tag as ParkInfo;
                    park.Add(info);
                }
            }
            return park;
        }

        /// <summary>
        /// 获取树中所有选中的停车场ID和通道ID
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, List<int>> GetSelectedParksAndEntrancesID()
        {
            Dictionary<int, List<int>> parksAndentrances = new Dictionary<int, List<int>>();
            foreach (TreeNode node in allEntranceNodes)
            {
                if (node.Checked)
                {
                    EntranceInfo info = node.Tag as EntranceInfo;
                    if (!parksAndentrances.ContainsKey(info.ParkID))
                    {
                        parksAndentrances.Add(info.ParkID, new List<int>());
                    }
                    parksAndentrances[info.ParkID].Add(info.EntranceID);
                }
            }
            return parksAndentrances;
        }

        /// <summary>
        /// 获取树中所有选中的停车场和通道
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, List<EntranceInfo>> GetSelectedParksIDAndEntrances()
        {
            Dictionary<int, List<EntranceInfo>> parksAndentrances = new Dictionary<int, List<EntranceInfo>>();
            foreach (TreeNode node in allEntranceNodes)
            {
                if (node.Checked)
                {
                    EntranceInfo info = node.Tag as EntranceInfo;
                    if (!parksAndentrances.ContainsKey(info.ParkID))
                    {
                        parksAndentrances.Add(info.ParkID, new List<EntranceInfo>());
                    }
                    parksAndentrances[info.ParkID].Add(info);
                }
            }
            return parksAndentrances;
        }

        /// <summary>
        /// 只展开根节点
        /// </summary>
        public void ExpandRootOnly()
        {
            this.CollapseAll();
            if (this.root != null)
            {
                this.root.Expand();
            }
        }
        #endregion

        #region IReportHandler 成员

        public void ProcessReport(ReportBase report)
        {
            Action<ReportBase> action = delegate(ReportBase r)
            {
                if (r is EntranceStatusReport)
                {
                    ProcessReport(r as EntranceStatusReport);
                }
                else if (r is ParkVacantReport)
                {
                    ProcessReport(r as ParkVacantReport);
                }
                else if (r is EntranceRemainTempCardReport)
                {
                    ProcessReport(r as EntranceRemainTempCardReport);
                }
            };

            if (this.InvokeRequired)
            {
                this.BeginInvoke(action, report);
            }
            else
            {
                action(report);
            }
        }

        private void ProcessReport(ParkVacantReport report)
        {
            ParkInfo park = ParkBuffer.Current.GetPark(report.ParkID);
            if (park != null)
            {
                park.Vacant = report.ParkVacant;
                TreeNode parkNode = GetParkNode(park.ParkID);
                RenderPark(parkNode, park);
            }
        }

        private void ProcessReport(EntranceStatusReport report)
        {
            EntranceInfo entrance = ParkBuffer.Current.GetEntrance(report.EntranceID);
            if (entrance != null)
            {
                entrance.Status = report.Status;
                TreeNode node = GetEntranceNode(entrance.EntranceID);
                if (node != null)
                {
                    RenderEntrance(node, entrance);
                }
            }
        }

        private void ProcessReport(EntranceRemainTempCardReport report)
        {
            EntranceInfo entrance = ParkBuffer.Current.GetEntrance(report.EntranceID);
            if (entrance != null)
            {
                entrance.TempCard = report.RemainTempCard;
                TreeNode node = GetEntranceNode(entrance.EntranceID);
                if (node != null)
                {
                    RenderEntrance(node, entrance);
                }
            }
        }
        #endregion
    }
}
